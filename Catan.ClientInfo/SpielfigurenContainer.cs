using Catan.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    public class SpielFigurenContainer
    {
        public List<Siedlung> Siedlungen { private set; get; }
        public List<Stadt> Staedte { private set; get; }
        public List<Strasse> Strassen { private set; get; }

        public SpielFigurenContainer()
        {
            this.Siedlungen = new List<Game.Siedlung>();
            this.Staedte = new List<Game.Stadt>();
            this.Strassen = new List<Game.Strasse>();
        }
    }
}
