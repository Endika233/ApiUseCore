using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Use_Core.Models
{
    public class RootObject
{
        public int Count { get; set; }
        public string Next { get; set; }
        public object Previous { get; set; }
        [JsonProperty(PropertyName ="results")]//Cogeme el atributo que se llama results de la API
        public List<Character> Characters { get; set; }//Y luego haces esto y se le puede cambiar el nombre
    }
}
