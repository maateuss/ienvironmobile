using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iemobile.Models;

namespace iemobile.Interfaces
{
    public interface IEventoService
    {
        Task<IEnumerable<Evento>> BuscarEventos(Ambiente ambiente);
        Task<bool> AcionarEventoManual(Evento evento);
    }
}
