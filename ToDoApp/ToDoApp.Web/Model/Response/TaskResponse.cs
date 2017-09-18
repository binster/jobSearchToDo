using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoApp.Web.Model.Response
{
    public class TaskResponse<T> : SuccessResponse
    {
        public T Task { get; set; }
    }
}