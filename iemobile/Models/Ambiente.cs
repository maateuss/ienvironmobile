using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace iemobile.Models
{
    public class Ambiente
    {
        public string Id { get; set; }
        public string ImageSource { get; set; } = "https://ienvironment.s3-sa-east-1.amazonaws.com/2BBDB2D9073382FB18641B8071AAC3CB8E7FC1B04E3753872CAFFA0DC4434EED.jpg";
        [JsonProperty("name")]
        public string NomeAmbiente { get; set; }
        [JsonProperty("description")]
        public string DescricaoAmbiente { get; set; }
        public List<string> Equipments { get; set; }
        public List<string> Events { get;set; }
    }
}
