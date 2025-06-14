﻿using EventHubWebsite.Data;

namespace EventHubWebsite.Entities
{
    public class UserEvent
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public AppUser? User { get; set; } 

        public int EventId { get; set; }
        public Event? Event { get; set; } 

    }
}
