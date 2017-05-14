using Catan.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catan.Game
{
    [Serializable]
    public class SpielFigurenContainer
    {
        public List<Siedlung> Siedlungen { private set; get; }
        public List<Stadt> Staedte { private set; get; }
        public List<Strasse> Strassen { private set; get; }

        public SpielFigurenContainer()
        {
            this.Siedlungen = new List<Game.Siedlung>(capacity: 5);
            this.Staedte = new List<Game.Stadt>(capacity: 4);
            this.Strassen = new List<Game.Strasse>(capacity: 15);
        }
    }
}
