using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Top_lista_vremena.Models;

namespace Top_lista_vremena.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        public readonly string connectionString;
        private static IList<TopTime> TopTimes;

        public HomeController(IConfiguration config)
        {
            this.configuration = config;
            connectionString = configuration.GetConnectionString("TopListConnection");
            GetRecords();
        }
        
        public void GetRecords()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    TopTimes = new List<TopTime>();
                    SqlCommand cmd = new SqlCommand("spGetTopListRecords", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        TopTime record = new TopTime();
                        record.ID = (int)dr[0];
                        record.Name = dr[1].ToString();
                        record.Surname = dr[2].ToString();
                        record.Time = TimeSpan.Parse(dr[3].ToString());

                        TopTimes.Add(record);
                    }
                    connection.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TopListView()
        {
            return PartialView("TopListView", TopTimes.OrderBy(x => x.Time));
        }

        public IActionResult NewRecord()
        {
            return View("NewRecordView");
        }

        public IActionResult AddRecord(TopTime record)
        {
            if (TopTimes == null)
                TopTimes = new List<TopTime>();
            if (TopTimes.Count != 0)
                record.ID = TopTimes.Max(x => x.ID) + 1;
            else
                record.ID = 1;
            
            TopTimes.Add(record);

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAddTopListRecord", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", record.Name);
                    cmd.Parameters.AddWithValue("@surname", record.Surname);
                    cmd.Parameters.AddWithValue("@time", record.Time);
                    connection.Open();
                    cmd.ExecuteReader();
                    connection.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return View("Index");
        }
    }
}
