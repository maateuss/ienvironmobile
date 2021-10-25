using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iemobile.Interfaces;
using iemobile.Models;

namespace iemobile.Services
{
    public class AmbienteService : BaseService, IAmbienteService
    {
        public AmbienteService() : base("Environment")
        {

        }
        public async Task<IEnumerable<Ambiente>> BuscarAmbientes()
        {
            try
            {
                var result =  await GetFromWebApi<IEnumerable<Ambiente>>("GetAll");
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
