﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Network.Messaging
{
    [Serializable]
    public abstract class  NetworkMessage
    {
        public static uint MAX_DATA_SIZE_IN_BYTES = 5 * 1024;
    }
}
