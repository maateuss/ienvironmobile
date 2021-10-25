using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iemobile.Interfaces;
using iemobile.Models;

namespace iemobile.Services
{
    public class AtuadorService : BaseService, IAtuadorService
    {
        public AtuadorService() : base("Actuator")
        {

        }

        public async Task<IEnumerable<Atuador>> BuscarAtuadores(Ambiente ambiente)
        {
            try
            {
                var result = await GetFromWebApi<IEnumerable<Atuador>>($"GetByEnvironmentId/{ambiente.Id}");

                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
