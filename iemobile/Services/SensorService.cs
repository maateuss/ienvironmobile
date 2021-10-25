using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iemobile.Interfaces;
using iemobile.Models;

namespace iemobile.Services
{
    public class SensorService : BaseService, ISensorService
    {

        public SensorService() : base("Sensor")
        {

        }
        public async Task<IEnumerable<Sensor>> BuscarSensores(Ambiente ambiente)
        {
            try
            {
                var result = await GetFromWebApi<IEnumerable<Sensor>>($"GetByEnvironmentId/{ambiente.Id}");

                return result;
            }
            catch
            {
                return null;
            }

        }
    }
}
