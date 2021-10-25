using System;
using Newtonsoft.Json;

namespace iemobile.Models
{
    public class Evento
    {
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Nome { get; set; }
        public string Description { get; set; }
        public string Icone { get; set; }
        public bool IsManual { get; set; }
    }
}
