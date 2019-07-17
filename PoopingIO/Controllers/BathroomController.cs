using Microsoft.AspNetCore.Mvc;
using PoopingIO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoopingIO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BathroomController : ControllerBase
    {
        private readonly BathroomManager _manager;

        public BathroomController(BathroomManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public object RetrieveBathrooms()
        {
            var response =
                $"{_manager.Piso7AmericasHombres.Name}: {_manager.Piso7AmericasHombres.OpenSpaces}.\r\n {_manager.Piso7AmericasMujeres.Name}: {_manager.Piso7AmericasMujeres.OpenSpaces}.";
            return response;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }
    }
}
