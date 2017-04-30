using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Network;

namespace Catan.Network.Events
{
    public class CatanClientConnectedEventArgs:EventArgs
    {
        public CatanClient CatanClient { private set; get; }

        public CatanClientConnectedEventArgs(CatanClient catanClient)
        {
            this.CatanClient = catanClient;
        }
    }
}
