using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DecryptService.model
{
    public class FileResponse
    {
        [JsonProperty(PropertyName = "filepath")]
        public string FilePath { get; set; }

        [JsonProperty(PropertyName = "sucess")]
        public bool Sucess { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
