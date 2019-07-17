using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoopingIO.Models
{
    public class ResponseDTO
    {
        public string Text { get; set; }
        public string Token { get; set; }
        public string Channel { get; set; }
        public bool As_User { get; set; }
    }
}
