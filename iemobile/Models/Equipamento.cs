using System;
using Newtonsoft.Json;

namespace iemobile.Models
{
    public class Equipamento
    {
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Nome { get; set; }
        public string Description { get; set; }
        public string Topic { get; set; }
        public string Icone { get; set; }
        [JsonProperty("currentValue")]
        public string Valor { get; set; }
    }
}
