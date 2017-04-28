
using Catan.Network.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Network.Messaging
{
    public class NetworkMessageFormatter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Liefert NULL, wenn die Deserialisierung fehlschlägt.</returns>
        public NetworkMessage Deserialize(byte[] data)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream(data))
                {
                   return bf.Deserialize(ms) as NetworkMessage;
                }
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Liefert NULL, wenn die Deserialisierung fehlschlägt.</returns>
        public byte[] Serialize(NetworkMessage message)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    bf.Serialize(ms, message);
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
