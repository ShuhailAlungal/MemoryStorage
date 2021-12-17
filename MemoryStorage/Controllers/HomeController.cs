using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MemoryStorage.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace MemoryStorage.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration config;

        public HomeController(IConfiguration configuration)
        {
            this.config = configuration;
        }
        public IActionResult Index()
        {
            string cnString = config.GetConnectionString("DefaultConnectionString");
            SqlConnection conn = new SqlConnection(cnString);
            conn.Open();
            SqlCommand com = new SqlCommand("select * from tblMemory where IsActive = 1", conn);
            var data = com.ExecuteReader();
            List<MemoryModel> obj = new List<MemoryModel>();
            while(data.Read())
            {
                MemoryModel model = new MemoryModel();
                int id = (int)data["Id"];
                model.Id = id;
                string size = (string)data["Size"];
                model.Size = size;
                string type = (string)data["Type"];
                model.Type = type;
                string noOfItems = (string)data["NoOfItems"];
                model.NoOfItems = noOfItems;
                string make = (string)data["Make"];
                model.Make = make;
                string speed = (string)data["Speed"];
                model.Speed = speed;
                string processor = (string)data["Processor"];
                model.Processor = processor;
                obj.Add(model);
            }
            conn.Close();
            return View(obj);
        }

        [HttpGet]
        public IActionResult Create()
        {          
            return View();
        }

        [HttpPost]
        public IActionResult Create(MemoryModel model1)
        {
            string cnString = config.GetConnectionString("DefaultConnectionString");
            SqlConnection conn = new SqlConnection(cnString);
            conn.Open();
            SqlCommand com = new SqlCommand("insert into tblMemory (Size, Type, NoOfItems, Make, Speed, Processor, IsActive) values(@param1, @param2, @param3, @param4, @param5, @param6, 1)", conn);
            com.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = model1.Size;
            com.Parameters.Add("@param2", SqlDbType.VarChar, 50).Value = model1.Type;
            com.Parameters.Add("@param3", SqlDbType.VarChar, 50).Value = model1.NoOfItems;
            com.Parameters.Add("@param4", SqlDbType.VarChar, 50).Value = model1.Make;
            com.Parameters.Add("@param5", SqlDbType.VarChar, 50).Value = model1.Speed;
            com.Parameters.Add("@param6", SqlDbType.VarChar, 50).Value = model1.Processor;
            com.ExecuteNonQuery();
            conn.Close();
            return Redirect("Index");
        }


        [HttpPut]
        public IActionResult Update(MemoryModel mod)
        {
            string cnString = config.GetConnectionString("DefaultConnectionString");
            SqlConnection conn = new SqlConnection(cnString);
            conn.Open();
            SqlCommand com = new SqlCommand("update tblMemory set Size = @param1, Type = @param2, NoOfItems = @param3, Make = @param4, Speed = @param5, Processor = @param6 where Id = @Id", conn);
            com.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = mod.Size;
            com.Parameters.Add("@param2", SqlDbType.VarChar, 50).Value = mod.Type;
            com.Parameters.Add("@param3", SqlDbType.VarChar, 50).Value = mod.NoOfItems;
            com.Parameters.Add("@param4", SqlDbType.VarChar, 50).Value = mod.Make;
            com.Parameters.Add("@param5", SqlDbType.VarChar, 50).Value = mod.Speed;
            com.Parameters.Add("@param6", SqlDbType.VarChar, 50).Value = mod.Processor;
            com.Parameters.Add("@Id", SqlDbType.Int).Value = mod.Id;
            com.ExecuteNonQuery();
            conn.Close();
            return View();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            string cnString = config.GetConnectionString("DefaultConnectionString");
            SqlConnection conn = new SqlConnection(cnString);
            conn.Open();
            SqlCommand com = new SqlCommand("update tblMemory set IsActive = 0 where Id = "+id+"", conn);
            com.ExecuteNonQuery();
            conn.Close();
            return Redirect("Index");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
