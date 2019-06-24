using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHub.Models
{
    public class IoTMessage
    {
        public string MACAddress { get; set; }
        public DateTime TimeStamp { get; set; }
        public IoTStatus Status { get; set; }
        public string message { get; set; }
    }

    public enum IoTStatus
    {
        Failure = 0,
        Alive = 1,
        ShuttingDown = 2
    }
}
