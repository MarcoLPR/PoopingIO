using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoopingIO.Models
{
    public class EventDTO
    {
        public string Token { get; set; }
        public string Team_Id { get; set; }
        public string Api_App_Id { get; set; }
        public EventTypeDTO Event { get; set; }
        public string Type { get; set; }
        public string[] Authed_Users { get; set; }
        public string Event_Id { get; set; }
        public string Event_Time { get; set; }
    }
}
