﻿using SistemaCondominio.Models;
using SistemaCondominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCondominio.Aplicacao
{
    public class LogAplicacao
    {
        public int GravarLog(Log log)
        {
            try
            {
                using(var logs = new LogRepositorio())
                {
                    logs.Adicionar(log);
                    logs.SalvarTodos();

                    return log.LogId;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}