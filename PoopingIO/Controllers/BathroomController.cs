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
                content.token = _options.SlackBotToken;
                content.channel = "#general";
                content.as_user = true;
                if (request.Event.Text.Contains("hombre"))
                {
                    content.text = $"En el baño {_manager.bathrooms[0].Name} hay {_manager.bathrooms[0].OpenSpaces} disponibles. ¡Suerte!";

                    JsonClient.Post(_options.SlackUrl, content, _options.SlackBotToken);
                }
                if (request.Event.Text.Contains("mujer"))
                {
                    content.text = $"En el baño {_manager.bathrooms[1].Name} hay {_manager.bathrooms[1].OpenSpaces} disponibles. ¡Suerte!";

                    JsonClient.Post(_options.SlackUrl, content, _options.SlackBotToken);
                }
                if (request.Event.Text.Contains("todo"))
                {
                    content.text = $"En el baño {_manager.bathrooms[1].Name} hay {_manager.bathrooms[1].OpenSpaces} disponibles.\r\nEn el baño {_manager.bathrooms[0].Name} hay {_manager.bathrooms[0].OpenSpaces} disponibles.";

                    JsonClient.Post(_options.SlackUrl, content, _options.SlackBotToken);
                }
            }
        }

        [HttpGet]
        [Route("update/{id}/{spaces}")]
        public void UpdateA(int id, int spaces)
        {
            var bathroom = _manager.bathrooms.FirstOrDefault(x => x.Id == id);
            bathroom.OpenSpaces = spaces;
        }

        [HttpPost]
        [Route("update/{id}/{spaces}")]
        public void UpdateB(int id, int spaces)
        {
            var bathroom = _manager.bathrooms.FirstOrDefault(x => x.Id == id);
            bathroom.OpenSpaces = spaces;
        }
    }
}
