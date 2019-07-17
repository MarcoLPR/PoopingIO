using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoopingIO.Models
{
    public class AppSettings
    {
        public string SlackUrl { get; set; } = "https://slack.com/api/chat.postMessage";
        public string SlackBotToken { get; set; } = "xoxb-103199614660-691453304065-5IbUemk9M9fKg4qM4WZKULwY";
    }
}
