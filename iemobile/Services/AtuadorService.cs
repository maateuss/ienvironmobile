using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iemobile.Interfaces;
using iemobile.Models;

namespace iemobile.Services
{
    public class AtuadorService : IAtuadorService
    {
        public async Task<IEnumerable<Atuador>> BuscarAtuadores(Ambiente ambiente)
        {
            await Task.Delay(15);

            return new List<Atuador>
            {
                new Atuador
                {
                    Icone = "",
                    Nome = "Lâmpada 1",
                    Valor = "1",
                    UnidadeMedida = "Desligada|Ligada"
                },
                new Atuador
                {
                    Icone = "",
                    Nome = "Projetor",
                    Valor = "0",
                    UnidadeMedida = "Desligado|Ligado"
                },
                new Atuador
                {
                    Icone = "",
                    Nome = "Ar Condicionado",
                    Valor = "21",
                    UnidadeMedida = "Cº"
                }
            };
        }
    }
}
