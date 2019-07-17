using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PoopingIO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Unosquare.Swan.Networking;

namespace PoopingIO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BathroomController : ControllerBase
    {
        private readonly BathroomManager _manager;
        private readonly AppSettings _options;

        public BathroomController(IOptions<AppSettings> options, BathroomManager manager)
        {
            _manager = manager;
            _options = options.Value;
        }

        [HttpPost]
        public void Post([FromBody] EventDTO request)
        {
            var content = new ResponseDTO();
            if (request.Event.Type == "app_mention")
            {
                content.Token = _options.SlackBotToken;
                content.Channel = request.Event.User;
                content.As_User = true;
                if (request.Event.Text.Contains("hombre"))
                {
                    content.Text = $"En el baño {_manager.Piso7AmericasHombres.Name} hay {_manager.Piso7AmericasHombres.OpenSpaces} disponibles.";
                    JsonClient.Post(_options.SlackUrl, content, $"{_options.SlackBotToken}");
                }
            }
        }
    }
}
