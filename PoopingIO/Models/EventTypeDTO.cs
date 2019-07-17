using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoopingIO.Models
{
    public class EventTypeDTO
    {
        public string Type { get; set; }
        public string Event_Ts { get; set; }
        public string User { get; set; }
        public ItemDTO Item { get; set; }
        public string Item_User { get; set; }
        public string Text { get; set; }
    }
}
