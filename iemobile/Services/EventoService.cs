using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iemobile.Interfaces;
using iemobile.Models;

namespace iemobile.Services
{
    public class EventoService : IEventoService
    {

        public async Task<IEnumerable<Evento>> BuscarEventos(Ambiente ambiente)
        {
            await Task.Delay(15);

            return new List<Evento>
            {
                new Evento
                {
                    Icone = "",
                    Nome = "Ligar luz",
                },
                new Evento
                {
                    Icone = "",
                    Nome = "Desligar luz",
                },
                new Evento
                {
                    Icone = "",
                    Nome = "Sair"
                },
                new Evento
                {
                    Icone = "",
                    Nome = "Reunião"
                },
            };
        }
    }
}
