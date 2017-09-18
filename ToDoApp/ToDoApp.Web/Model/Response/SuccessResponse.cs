using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoApp.Web.Model.Response
{
    public class SuccessResponse : BaseResponse
    {
        public SuccessResponse()
        {
            this.IsSuccessful = true;
        }
    }
}