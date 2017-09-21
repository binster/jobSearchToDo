using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ToDoApp.Web.Data.Extension;
using ToDoApp.Web.Model.Domain;
using ToDoApp.Web.Model.Request;
using ToDoApp.Web.Services.Interfaces;
using AngleSharp;
using System.Threading.Tasks;

namespace ToDoApp.Web.Services
{
    public class ToDoService : IToDoService
    {
 
        const string connectionString = "Server=.;Database=ToDo;Trusted_Connection=true";

        //Get All Scheduled Items
        public List<ScheduleItem> GetAll()
        {
            List<ScheduleItem> scheduleList = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "dbo.ToDo_SelectAll";
                cmd.CommandType = CommandType.StoredProcedure;

                using(IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int index = 0;
                        ScheduleItem singleTask = new ScheduleItem();
                        singleTask.Id = reader.GetInt32(index++);
                        singleTask.Task = reader.GetString(index++);
                        singleTask.Reason = reader.GetSQLString(index++);
                        singleTask.Resources = reader.GetSQLString(index++);
                        singleTask.Priority = reader.GetInt32(index++);
                        singleTask.DueBy = reader.GetSQLDateTimeNullable(index++);

                        if(scheduleList == null)
                        {
                            scheduleList = new List<ScheduleItem>();
                        }

                        scheduleList.Add(singleTask);
                    }
                    return scheduleList;
                }
            }         
        }

        //Select Task by Id
        public ScheduleItem GetTaskById(int id)
        {
            ScheduleItem singleTask = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "dbo.ToDo_SelectById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                using(IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int index = 0;
                        if(singleTask == null)
                        {
                            singleTask = new ScheduleItem();
                        }
                        singleTask.Id = reader.GetInt32(index++);
                        singleTask.Task = reader.GetString(index++);
                        singleTask.Reason = reader.GetSQLString(index++);
                        singleTask.Resources = reader.GetSQLString(index++);
                        singleTask.Priority = reader.GetInt32(index++);
                        singleTask.DueBy = reader.GetSQLDateTimeNullable(index++);
                    }
                    return singleTask;
                }
            }
            
        }

        //Schedule New Task
        public int ScheduleNewTask(ScheduleTask model)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "dbo.ToDo_Insert";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Task", model.Task);
                cmd.Parameters.AddWithValue("@Reason", model.Reason);
                cmd.Parameters.AddWithValue("@Resources", model.Resources);
                cmd.Parameters.AddWithValue("@Priority", model.Priority);
                cmd.Parameters.AddWithValue("@DueBy", model.DueBy);

                SqlParameter outputParam = cmd.Parameters.Add("@Id", SqlDbType.Int);
                outputParam.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                return (int)outputParam.Value;
            }
        }

        //Update Task
        public void UpdateTask(ScheduleTaskWithId model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "dbo.ToDo_Update";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@Task", model.Task);
                cmd.Parameters.AddWithValue("@Reason", model.Reason);
                cmd.Parameters.AddWithValue("@Resources", model.Resources);
                cmd.Parameters.AddWithValue("@Priority", model.Priority);
                cmd.Parameters.AddWithValue("@DueBy", model.DueBy);

                cmd.ExecuteNonQuery();
            }
        }

        //Delete Task
        public void DeleteTask(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "dbo.ToDo_Delete";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }

        //web scrape reddit's /r/getmotivated
        public async Task<List<MotivationLink>> GetRedditPosts()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://www.reddit.com/r/GetMotivated/";
            var document = await BrowsingContext.New(config).OpenAsync(address);
            var cellSelector = "a";
            var cells = document.QuerySelectorAll(cellSelector);
            var redditLinks = cells
                .Where(m => m.ClassList.Contains("title"))
                .Select(m => new MotivationLink
                {
                    LinkTitle = m.TextContent,
                    LinkUrl = m.GetAttribute("href")
                })
                .ToList();
            return redditLinks;

        }
    }
}