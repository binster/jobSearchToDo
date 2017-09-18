using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoApp.Web.Model.Response
{
    public class TasksResponse<T>:SuccessResponse
    {
        public List<T> Tasks { get; set; }
    }
}