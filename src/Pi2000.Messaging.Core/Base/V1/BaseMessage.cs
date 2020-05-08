using System;

namespace Pi2000.Messaging.MessageDefinitions.Base.V1
{
    [Serializable]
    public struct BaseMessage<T>
    {
        public T Body { get; set; }
    }
}
