using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Cartan.Utils
{
    public class NetworkInterface
    {
        public System.Net.NetworkInformation.NetworkInterface Networkinterface { private set; get; }
        public UnicastIPAddressInformation IP { private set; get; }
        public NetworkInterface(System.Net.NetworkInformation.NetworkInterface nic, UnicastIPAddressInformation unicasIp)
        {
            this.Networkinterface = nic;
            this.IP = unicasIp;
        }
    }
}
