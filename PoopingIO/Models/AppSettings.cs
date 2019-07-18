using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoopingIO.Models
{
    public class AppSettings
    {
        public string SlackUrl { get; set; } = "https://slack.com/api/chat.postMessage";
        public string SlackBotToken { get; set; } = "xoxb-687329265779-700818904423-SGwpMX40hq87fTVaf0zfZEJD";
    }
}
