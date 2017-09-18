using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoApp.Web.Model.Request
{
    public class ScheduleTaskWithId : ScheduleTask
    {
        [Required]
        public int Id { get; set; }
    }
}