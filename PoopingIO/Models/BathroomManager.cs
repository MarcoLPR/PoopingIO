using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoopingIO.Models
{
    public class BathroomManager
    {
        private static readonly BathroomManager _bathroomManager;

        static BathroomManager()
        {
            _bathroomManager = new BathroomManager();
        }

        public static BathroomManager Manager
        {
            get { return _bathroomManager; }
        }

        public Bathroom[] bathrooms = new Bathroom[] {
                new Bathroom { Id = 1, Name = "Piso 7 - Hombres", Genre = 1, OpenSpaces = 2 },
                new Bathroom { Id = 2, Name = "Piso 7 - Mujeres", Genre = 2, OpenSpaces = 3 }
        };
    }
}
