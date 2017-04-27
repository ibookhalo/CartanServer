using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cartan.Utils
{
    public static class NetworkInfo
    {
        public static List<NetworkInterface> GetAvailableNetworkInterfaces()
        {
            System.Net.NetworkInformation.NetworkInterface[] adapters = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().
                Where(nic => nic.OperationalStatus == OperationalStatus.Up).ToArray();
            List<NetworkInterface> nics = new List<Utils.NetworkInterface>();

            foreach (System.Net.NetworkInformation.NetworkInterface nic in adapters)
            {
                var unicastIps = nic.GetIPProperties().UnicastAddresses.Where(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork).ToList();
                foreach (var unicasIp in unicastIps)
                {
                    nics.Add(new NetworkInterface(nic,unicasIp));
                }

            }
            return nics;
        }
    }
}
