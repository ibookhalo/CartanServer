using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan.Network;

namespace Catan.Network.EventArgs
{
    public class CatanClientConnectedEventArgs: System.EventArgs
    {
        public CatanClient CatanClient { private set; get; }

        public CatanClientConnectedEventArgs(CatanClient catanClient)
        {
            this.CatanClient = catanClient;
        }
    }
}
