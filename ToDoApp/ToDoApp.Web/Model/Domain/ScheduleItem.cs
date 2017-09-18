using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoApp.Web.Model.Domain
{
    public class ScheduleItem
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public string Reason { get; set; }
        public string Resources { get; set; }
        public int Priority { get; set; }
        public DateTime? DueBy { get; set; }
    }
}