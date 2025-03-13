using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RailwayManagementSystem.Models
{
    public class BookingInfo
    {
        [Key]
        public int Pnr { get; set; }
        [BindProperty]
        public DateTime BookingDate { get; set; }
        public string SourceStation { get; set; }
        public string DestinationStation { get; set; }
        public int TicketFare { get; set; }
        [BindProperty]
        public int UserId { get; set; }
        public bool BookingStatus { get; set; }

    }
}