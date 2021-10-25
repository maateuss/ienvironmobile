using System;
namespace iemobile.Models
{
    public class Atuador : Equipamento
    {
        public string UnidadeMedida { get; set; }
        public string ValorFormatado { get => RetornarValorFormatado(); }

        internal string RetornarValorFormatado()
        {
            return Valor;
        }


        public string LastSignalReceivedTime { get; set; }
    }
}
