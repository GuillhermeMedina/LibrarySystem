using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserTypeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public UserTypeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select ReaderId, ReaderName, UserType, ReaderPassword, Reservations from
                        Reader
                ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ReaderAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Readers dep)
        {
            string query = @"
                        
                        insert into Reader(ReaderId, ReaderName, UserType, ReaderPassword, Reservations) 
                                    values (@ReaderId, @ReaderName, @UserType, @ReaderPassword, @Reservations);
                        from
                        Reader
                ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ReaderAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ReaderId", dep.ReaderId);
                    myCommand.Parameters.AddWithValue("@ReaderName", dep.ReaderName);
                    myCommand.Parameters.AddWithValue("@UserType", dep.UserType);
                    myCommand.Parameters.AddWithValue("@ReaderPassword", dep.ReaderPassword);
                    myCommand.Parameters.AddWithValue("@Reservations", dep.Reservations);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Readers dep)
        {
            string query = @"
                        
                        update Reader set 
                        ReaderName =@ReaderName 
                        UserType =@UserType 
                        ReaderPassword =@ReaderPassword
                        Reservation =@Reservation
                        where ReaderId=@ReaderId;
        
                ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ReaderAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ReaderName", dep.ReaderName);
                    myCommand.Parameters.AddWithValue("@UserType", dep.UserType);
                    myCommand.Parameters.AddWithValue("@ReaderPassword", dep.ReaderPassword);
                    myCommand.Parameters.AddWithValue("@Reservations", dep.Reservations);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("update Successfully");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                        
                        delete from Reader
                        where ReaderId=@ReaderId;
        
                ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ReaderAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ReaderId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("deleted Successfully");
        }
    }
}
