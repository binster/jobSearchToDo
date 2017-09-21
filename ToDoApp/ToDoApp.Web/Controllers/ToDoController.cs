using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ToDoApp.Web.Model.Domain;
using ToDoApp.Web.Model.Request;
using ToDoApp.Web.Model.Response;
using ToDoApp.Web.Services.Interfaces;

namespace ToDoApp.Web.Controllers
{
    [RoutePrefix("api/toDo")]
    public class ToDoController : ApiController
    {
        //inject ToDoService interface
        readonly IToDoService toDoService;
        public ToDoController(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }

        //Get All Tasks, returns List of Tasks
        [Route, HttpGet]
        public HttpResponseMessage GetAllTasks()
        {
            TasksResponse<ScheduleItem> response = new TasksResponse<ScheduleItem>();
            response.Tasks = toDoService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //Get Task By Id, Returns Task
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetTaskById(int id)
        {

            TaskResponse<ScheduleItem> response = new TaskResponse<ScheduleItem>
            {
                Task = toDoService.GetTaskById(id)
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);       
        }

        //Schedule a New Task, returns Task Id
        [Route, HttpPost]
        public HttpResponseMessage ScheduleNewTask(ScheduleTask model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            TaskResponse<int> response = new TaskResponse<int>
            {
                Task = toDoService.ScheduleNewTask(model)
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //Update a Task by Task Id, returns Success Message
        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateTask(int id, ScheduleTaskWithId model)
        {
            if(id != model.Id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The id parameter does not exist in this context");
            }
            SuccessResponse response = new SuccessResponse();
            toDoService.UpdateTask(model);

            return Request.CreateResponse(HttpStatusCode.OK, response);
            
        }

        //Delete a Task by Task Id, returns Success Message
        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteTask(int id)
        {
            toDoService.DeleteTask(id);
            SuccessResponse response = new SuccessResponse();
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("motivation"), HttpGet]
        public async Task<HttpResponseMessage> GetMotivationLinks()
        {
            TasksResponse<MotivationLink> response = new TasksResponse<MotivationLink>();
            response.Tasks = await toDoService.GetRedditPosts();
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

    }
}