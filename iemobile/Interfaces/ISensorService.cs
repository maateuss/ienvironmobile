using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iemobile.Models;

namespace iemobile.Interfaces
{
    public interface ISensorService
    {
        Task<IEnumerable<Sensor>> BuscarSensores(Ambiente ambiente);
    }
}
