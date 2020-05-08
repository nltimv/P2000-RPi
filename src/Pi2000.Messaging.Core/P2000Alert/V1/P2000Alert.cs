using System;
using System.Collections.Generic;
using System.Text;
using Pi2000.Messaging.MessageDefinitions.Base.V1;

namespace Pi2000.Messaging.MessageDefinitions.P2000Alert.V1
{
    public class P2000Alert
    {
        public DateTime Timestamp { get; set; }

        public string AlertText { get; set; }

        public ICollection<CapCode> CapCodeCollection { get; set; }

        public AlertPriority? AlertPriority { get; set; }

        public EmergencyService? EmergencyService { get; set; }

        public RegionType? RegionType { get; set; }
    }
}
