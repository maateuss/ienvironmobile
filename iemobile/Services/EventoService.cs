using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iemobile.Interfaces;
using iemobile.Models;

namespace iemobile.Services
{
    public class EventoService : BaseService, IEventoService
    {
        public EventoService() : base("Event")
        {

        }

        public async Task<bool> AcionarEventoManual(Evento evento)
        {
            try
            {
                if (evento.IsManual)
                {
                    var result = await GetFromWebApi($"RaiseManualEvent/{evento.Id}");

                    return result.IsSuccessStatusCode;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Evento>> BuscarEventos(Ambiente ambiente)
        {
            try
            {
                var result = await GetFromWebApi<IEnumerable<Evento>>($"GetByEnvironmentId/{ambiente.Id}");

                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
