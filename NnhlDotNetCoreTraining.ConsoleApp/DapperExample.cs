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
                var lst = db.Query<BlogDataModel>(query).ToList();//query ko excute lok tr
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

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content,
                });
                //if (result == 0)
                //{
                //    Console.WriteLine("Insert is failed");

                //}
                //else {
                //    Console.WriteLine("Insert is Successful");
                //}
                Console.WriteLine(result == 1 ? "Saving is Successful" : "Saving is failed");
            }
        }

        public void Update()
        {
            Console.WriteLine("Enter the Title ID : ");
            int id = Convert.ToInt32(Console.ReadLine());

            string selectQuery = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent], [del_flg]
                           FROM [dbo].[Tbl_Blog] 
                           WHERE BlogId=@BlogId;";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();

                // Get current blog data
                var blog = db.QueryFirstOrDefault<BlogDataModel>(selectQuery, new { BlogId = id });
                if (blog is null)
                {
                    Console.WriteLine("Data not found.");
                    return;
                }

                //Show existing data
                Console.WriteLine($"Title  Name : {blog.BlogTitle}");
                Console.WriteLine($"Author Name : {blog.BlogAuthor}");
                Console.WriteLine($"Content     : {blog.BlogContent}");

                //Ask for new values
                Console.WriteLine("Enter new Title :");
                blog.BlogTitle = Console.ReadLine();

                Console.WriteLine("Enter new Author :");
                blog.BlogAuthor = Console.ReadLine();

                Console.WriteLine("Enter new Content :");
                blog.BlogContent = Console.ReadLine();

                // Update in DB (property names match SQL params)
                string updateQuery = @"UPDATE [dbo].[Tbl_Blog]
                               SET BlogTitle=@BlogTitle,
                                   BlogAuthor=@BlogAuthor,
                                   BlogContent=@BlogContent
                               WHERE BlogId=@BlogId";

                int result = db.Execute(updateQuery, blog);

                Console.WriteLine(result != 1 ? "Update failed..." : "Update successful.");
                db.Close();
            }
        }
        public void Delete() {
            Console.WriteLine("Enter the id of Blog :");
            int id= Convert.ToInt32(Console.ReadLine());
            string selectQuery = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent], [del_flg]
                           FROM [dbo].[Tbl_Blog] 
                           WHERE BlogId=@BlogId;";



            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                var blog = db.QueryFirst<BlogDataModel>(selectQuery, new
                {
                    BlogId = id
                });
                if (blog is null)
                {
                    Console.WriteLine("data is not found ");
                    return;
                }
                Console.WriteLine($"Title name :{blog.BlogTitle}");
                Console.WriteLine($"Title Author :{blog.BlogAuthor}");
                Console.WriteLine($"Title content :{blog.BlogTitle}");

                Console.WriteLine("Are you sure delete it ? (y/n)");
                string confirm=Console.ReadLine();
                if ( confirm?.ToLower() != "y") {

                    Console.WriteLine("Delete is cancelled ");
                    return;
                }

                string deleteQuery = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId=@BlogId";

                int result =db.Execute(deleteQuery, new { BlogId = id });
                if (result is 0) { 
                Console.WriteLine("Delete is failed ...");
                }
                else
                {
                    Console.WriteLine("Delete is Success");
                }
                db.Close();
            }

        }
    }
}