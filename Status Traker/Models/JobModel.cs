using System;
using System.Collections.Generic;
using System.Text;

namespace Status_Traker.Models
{
    public class JobModel
    {
        public enum RepairStatus
        {
            Received,
            InProgress,
            WaitingOnParts,
            QualityCheck,
            ReadyForPickup,
            Completed
            
        }

        public class VehicleRepairJob
        {
            public int Id { get; set; }
            public string CustomerName { get; set; } = string.Empty;
            public string Year { get; set; } = string.Empty;
            public string Make { get; set; } = string.Empty;
            public string Model { get; set; } = string.Empty;
            public string RepairCenter { get; set; } = string.Empty;

            public RepairStatus Status { get; set; } = RepairStatus.Received;
            
        }
    }
}
