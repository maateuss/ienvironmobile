using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iemobile.Models;

namespace iemobile.Interfaces
{
    public interface IAmbienteService
    {
        Task<IEnumerable<Ambiente>> BuscarAmbientes();
    }
}
