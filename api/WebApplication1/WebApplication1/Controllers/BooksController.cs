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
    public class BooksController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public BooksController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select BookId, BooksName, BookAutor, BookSinopse, BookRelease, BookStatus, Reservations from
                        Books
                ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("BooksAppCon");
            MySqlDataReader myBooks;
            using(MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                using(MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myBooks = myCommand.ExecuteReader();
                    table.Load(myBooks);

                    myBooks.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Books dep)
        {
            string query = @"
                        
                        insert into Books(BookId, BooksName, BookAutor, BookSinopse, BookRelease, BookStatus, Reservations) 
                                    values (@BookId, @BooksName, @BookAutor, @BookSinopse, @BookRelease, @BookStatus, @Reservations);
                        from
                        Books
                ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("BooksAppCon");
            MySqlDataReader myBooks;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@BookId", dep.BookId);
                    myCommand.Parameters.AddWithValue("@BookName", dep.BookName);
                    myCommand.Parameters.AddWithValue("@BookAutor", dep.BookAutor);
                    myCommand.Parameters.AddWithValue("@BookSinopse", dep.BookSinopse);
                    myCommand.Parameters.AddWithValue("@BookRelease", dep.BookRelease);
                    myCommand.Parameters.AddWithValue("@BookStatus", dep.BookStatus);
                    myCommand.Parameters.AddWithValue("@Reservations", dep.Reservations);
                    myBooks = myCommand.ExecuteReader();
                    table.Load(myBooks);

                    myBooks.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Books dep)
        {
            string query = @"
                        
                        update Books set 
                        BookId =@BookId  
                        BookName =@BookName 
                        BookAutor =@BookAutor
                        BookSinopse =@BookSinopse
                        BookRelease =@BookRelease 
                        BookStatus =@BookStatus
                        Reservations =@Reservations
                        where BooksId=@BooksId;
        
                ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("BooksAppCon");
            MySqlDataReader myBooks;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@BookId", dep.BookId);
                    myCommand.Parameters.AddWithValue("@BookName", dep.BookName);
                    myCommand.Parameters.AddWithValue("@BookAutor", dep.BookAutor);
                    myCommand.Parameters.AddWithValue("@BookSinopse", dep.BookSinopse);
                    myCommand.Parameters.AddWithValue("@BookRelease", dep.BookRelease);
                    myCommand.Parameters.AddWithValue("@BookStatus", dep.BookStatus);
                    myCommand.Parameters.AddWithValue("@Reservations", dep.Reservations);
                    myBooks = myCommand.ExecuteReader();
                    table.Load(myBooks);

                    myBooks.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("update Successfully");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                        
                        delete from Books
                        where BookId=@BookId;
        
                ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("BooksAppCon");
            MySqlDataReader myBooks;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@BookId", id);
                    myBooks = myCommand.ExecuteReader();
                    table.Load(myBooks);

                    myBooks.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("deleted Successfully");
        }


    }
}
