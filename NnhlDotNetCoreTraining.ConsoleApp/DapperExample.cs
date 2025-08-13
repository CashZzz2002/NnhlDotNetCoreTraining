using Dapper;
using NnhlDotNetCoreTraining.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NnhlDotNetCoreTraining.ConsoleApp
{
    public class DapperExample
    {
        private readonly string _connectionString = "Data Source=DESKTOP-3CLQTFU;Initial Catalog=DotNetTrainingBatch5;Integrated Security=True;TrustServerCertificate=True;";
        public void Read()
        {

            //using (IDbConnection db = new SqlConnection(_connectionString))
            //{
            //    string query = "Select * from tbl_blog where del_flg=0;";
            //    var lst=db.Query(query).ToList();//query kp excute lok tr
            //    foreach (var item in lst) {
            //    Console.WriteLine(item.BlogId);
            //    Console.WriteLine(item.BlogTitle);
            //    Console.WriteLine(item.BlogAuthor);
            //    Console.WriteLine(item.BlogContent);


            //    }

            //}

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "Select * from tbl_blog where del_flg=0;";
                var lst = db.Query<BlogDataModel>(query).ToList();//query kp excute lok tr
                foreach (var item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);


                }

            }
        }
        public void Create(string title, string author, string content)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_Blog] 
                                    ([BlogTitle], 
                                    [BlogAuthor],
                                    [BlogContent],
                                    [del_flg])
                             VALUES (@BlogTitle, @BlogAuthor, @BlogContent, 0)";

            using (IDbConnection db = new SqlConnection(_connectionString)){
               int result= db.Execute(query,new BlogDataModel{
                   BlogTitle=title,
                   BlogAuthor=author,
                   BlogContent=content,
                });
                //if (result == 0)
                //{
                //    Console.WriteLine("Insert is failed");

                //}
                //else {
                //    Console.WriteLine("Insert is Successful");
                //}
               Console.WriteLine(result==1? "Saving is Successful": "Saving is failed");
            }
        }
    }
}
