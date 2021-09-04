using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iemobile.Interfaces;
using iemobile.Models;

namespace iemobile.Services
{
    public class SensorService : ISensorService
    {


        public async Task<IEnumerable<Sensor>> BuscarSensores(Ambiente ambiente)
        {
            await Task.Delay(15);

            return new List<Sensor>
            {
                new Sensor
                {
                    Icone = "",
                    Nome = "Temperatura",
                    Valor = "28",
                    UnidadeMedida = "ºC"
                },
                new Sensor
                {
                    Icone = "",
                    Nome = "Umidade",
                    Valor = "30",
                    UnidadeMedida = "%"
                },
                new Sensor
                {
                    Icone = "",
                    Nome = "Presença",
                    Valor = "1",
                    UnidadeMedida = "Não|Sim"
                },
                new Sensor
                {
                    Icone = "",
                    Nome = "Luminosidade",
                    Valor = "86",
                    UnidadeMedida = "%"
                },
            };

        }
    }
}
