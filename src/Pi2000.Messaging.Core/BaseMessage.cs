using System;
using System.Collections.Generic;
using System.Text;

namespace Pi2000.Messaging.Core
{
    [Serializable]
    public class BaseMessage<T>
    {
        public T Body { get; set; }
    }
}
