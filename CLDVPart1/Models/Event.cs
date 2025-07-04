﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics;



namespace CLDVPart1.Models

{
    public class Event
    {
        public int EventID { get; set; }

        [Required]

        public string? EventName { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        public string? Description { get; set; }
        public int? VenueID { get; set; }
        public Venue? Venue { get; set; }
        public int? EventTypeID { get; set; }
        public EventType? EventType { get; set; }
    

    }


}
