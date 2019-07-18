using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoopingIO.Models
{
    public class ResponseDTO
    {
        public string text { get; set; }
        public string token { get; set; }
        public string channel { get; set; }
        public bool as_user { get; set; }
    }
}
