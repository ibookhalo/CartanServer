using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartan.Network.CartanClientMessages
{
    [Serializable]
    public abstract class  NetworkClientMessage
    {
        public abstract ushort MaxDataSizeInBytes { get; }
    }
}
