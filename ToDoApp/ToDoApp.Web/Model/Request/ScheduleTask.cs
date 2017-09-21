using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoApp.Web.Model.Request
{
    public class ScheduleTask
    {
        [Required]
        public string Task { get; set; }
        public string Reason { get; set; }
        public string Resources { get; set; }
        [Range(1,3)]
        public int Priority { get; set; }
        public DateTime? DueBy { get; set; } //should i set defaults here?
    }
}