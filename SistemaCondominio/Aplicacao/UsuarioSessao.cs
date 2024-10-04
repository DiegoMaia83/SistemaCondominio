using SistemaCondominio.Models;
using SistemaCondominio.Repositorio;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SistemaCondominio.Aplicacao
{
    public class UsuarioSessao
    {
        public static UsuarioLogin Logado
        {
            get
            {
                return GetCookie();
            }
        }

        public static UsuarioLogin GetCookie()
        {
            var adminLogin = new UsuarioLogin();

            try
            {
                if(HttpContext.Current.Request.Cookies["c-sgc"] != null)
                {
                    adminLogin.Token = HttpContext.Current.Request.Cookies["c-sgc"]["key"];
                    adminLogin.UsuarioId = Convert.ToInt32(HttpContext.Current.Request.Cookies["c-sgc"]["id"]);
                    adminLogin.Nome = HttpContext.Current.Request.Cookies["c-sgc"]["usr"];
                    adminLogin.Login = HttpContext.Current.Request.Cookies["c-sgc"]["ref"];
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return adminLogin;
        }

        public static void SetCookie(UsuarioLogin value)
        {
            HttpContext.Current.Response.Cookies["c-sgc"]["key"] = value.Token;
            HttpContext.Current.Response.Cookies["c-sgc"]["id"] = value.UsuarioId.ToString();
            HttpContext.Current.Response.Cookies["c-sgc"]["usr"] = value.Nome;
            HttpContext.Current.Response.Cookies["c-sgc"]["ref"] = value.Login;
        }

        public static bool Login(UsuarioLogin login)
        {
            try
            {
                if(login.Login != "" && login.Password != "")
                {
                    using(var usuarios = new UsuarioRepositorio())
                    {
                        var senhaHash = GerarHashMD5(login.Password);

                        // Busca o usuário
                        var usuario = usuarios.GetAll().Where(x => x.Login == login.Login && x.Password == senhaHash).FirstOrDefault();

                        // Não localizou o usuário
                        if (usuario == null) return false;

                        // Localizou. Armazena os cookies da sessão
                        SetCookie(new UsuarioLogin()
                        {
                            UsuarioId = usuario.UsuarioId,
                            Login = usuario.Login,
                            Nome = usuario.Nome,
                            Token = EncodeString(usuario.Login + ":" + usuario.Password)
                        });

                        return usuario.UsuarioId > 0;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidaToken()
        {
            try
            {
                if(Logado.Token != "")
                {
                    var uncodedToken = DecodeString(Logado.Token);
                    var login = uncodedToken.Split(':')[0].ToString();
                    var passwordHash = uncodedToken.Split(':')[1].ToString();

                    using(var usuarios = new UsuarioRepositorio())
                    {
                        var usuario = usuarios.GetAll().Where(x => x.Login == login && x.Password == passwordHash).FirstOrDefault();

                        // Não localizou
                        if (usuario == null) return false;

                        // Localizou. Retorna verdadeiro
                        return usuario.UsuarioId > 0;                    
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static void Logoff()
        {
            if((HttpContext.Current.Request.Cookies["c-sgc"] != null))
            {
                HttpContext.Current.Response.Cookies["c-sgc"].Expires = DateTime.Now.AddDays(-1);
            }
        }

        private static string EncodeString(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        private static string DecodeString(string str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }

        
        public static string GerarHashMD5(string senha)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(senha);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convertendo o hash para uma string hexadecimal
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}