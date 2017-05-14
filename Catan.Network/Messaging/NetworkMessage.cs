using System;

namespace Catan.Network.Messaging
{
    [Serializable]
    public abstract class  NetworkMessage
    {
        public static int MAX_DATA_SIZE_IN_BYTES = 5 * 1024;
    }
}
