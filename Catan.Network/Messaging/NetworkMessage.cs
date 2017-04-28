using System;

namespace Catan.Network.Messaging
{
    [Serializable]
    public class  NetworkMessage
    {
        public static int MAX_DATA_SIZE_IN_BYTES = 5 * 1024;

        public static bool TryParse(byte[] data, out NetworkMessage networkMessage)
        {
            networkMessage = new NetworkMessageFormatter().Deserialize(data);
            return networkMessage != null;
        }
    }
}
