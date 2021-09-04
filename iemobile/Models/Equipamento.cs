﻿using System;
namespace iemobile.Models
{
    public class Equipamento
    {
        public string Nome { get; set; }
        public string Icone { get; set; }
        public string Valor { get; set; }
        public string UnidadeMedida { get; set; }
        public string ValorFormatado { get => RetornarValorFormatado(); }

        internal string RetornarValorFormatado()
        {
            if (UnidadeMedida?.Contains("|") ?? false)
            {
                if (int.TryParse(Valor, out int valorInt))
                {
                    var valoresUnidadeMedidaSplitado = UnidadeMedida.Split('|');
                    if (valorInt >= valoresUnidadeMedidaSplitado.Length)
                    {
                        return Valor;
                    }
                    else
                    {
                        return string.Format("{0}", valoresUnidadeMedidaSplitado[valorInt]);
                    }

                }
                else
                {
                    return Valor;
                }
            }



            return $"{Valor}{UnidadeMedida}";
        }
    }
}
