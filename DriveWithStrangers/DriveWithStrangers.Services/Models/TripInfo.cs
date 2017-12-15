﻿namespace DriveWithStrangers.Services.Models
{
    using System;

    public class TripInfo
    {
        public DateTime StartDate { get; set; }

        public string DriverId { get; set; }

        public bool UserIsSignedIn { get; set; }
    }
}
