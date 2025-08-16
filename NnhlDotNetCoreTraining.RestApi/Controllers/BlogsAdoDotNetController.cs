using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NnhlDotNetCoreTraining.Database.Models;
using NnhlDotNetCoreTraining.RestApi.ViewModels;
using System.Data;

namespace NnhlDotNetCoreTraining.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsAdoDotNetController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=DESKTOP-3CLQTFU;Initial Catalog=DotNetTrainingBatch5;Integrated Security=True;TrustServerCertificate=True;";

        // ✅ GET all blogs
        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> lst=new List<BlogViewModel>();
            SqlConnection connection = new SqlConnection();
            connection.Open();
            string selectQuery = @"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                                  ,[CreatedAt]
                                  ,[del_flg]
                              FROM [dbo].[Tbl_Blog]Where del_flg=0";

            SqlCommand command = new SqlCommand(selectQuery,connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) 
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);

                var item = new BlogViewModel
                {
                    Id=Convert.ToInt32(reader["BlogId"]),
                    Title=Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    Del_Flg = Convert.ToBoolean(reader["del_Flg"]),
                };
                lst.Add(item);
            }
            return Ok(lst);
        }

        // ✅ GET a single blog by id
        [HttpGet("{id}")]
        public IActionResult EditBlogs(int id)
        {
            BlogViewModel blog = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string query = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent], [Del_Flg]
                                 FROM [dbo].[Tbl_Blog]
                                 WHERE BlogId = @id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    blog = new BlogViewModel
                    {
                        Id = Convert.ToInt32(reader["BlogId"]),
                        Title = reader["BlogTitle"]?.ToString(),
                        Author = reader["BlogAuthor"]?.ToString(),
                        Content = reader["BlogContent"]?.ToString(),
                        Del_Flg = reader["Del_Flg"] != DBNull.Value && Convert.ToBoolean(reader["Del_Flg"])
                    };
                }
            }

            if (blog == null)
                return NotFound("Blog not found");

            return Ok(blog);
        }

        // ✅ CREATE a new blog
        [HttpPost]
        public IActionResult CreateBlogs(BlogViewModel blogView)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string insertQuery = @"INSERT INTO [dbo].[Tbl_Blog] 
                                        ([BlogTitle], [BlogAuthor], [BlogContent], [Del_Flg]) 
                                        VALUES (@title, @author,
                                                @content, @delFlg)";

                SqlCommand cmd = new SqlCommand(insertQuery, con);
                cmd.Parameters.AddWithValue("@title", blogView.Title ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@author", blogView.Author ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@content", blogView.Content ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@delFlg", blogView.Del_Flg);

                int result = cmd.ExecuteNonQuery();
                return Ok(result > 0 ? "Blog created successfully" : "Failed to create blog");
            }
        }

        // ✅ UPDATE a blog completely
        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, BlogViewModel blogView)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string updateQuery = @"UPDATE [dbo].[Tbl_Blog]
                                       SET [BlogTitle] = @title,
                                           [BlogAuthor] = @author,
                                           [BlogContent] = @content,
                                           [Del_Flg] = @delFlg
                                       WHERE BlogId = @id";

                SqlCommand cmd = new SqlCommand(updateQuery, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@title", blogView.Title ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@author", blogView.Author ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@content", blogView.Content ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@delFlg", blogView.Del_Flg);

                int result = cmd.ExecuteNonQuery();
                return Ok(result > 0 ? "Blog updated successfully" : "Update failed");
            }
        }

        // ✅ PATCH a blog (partial update)
        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, BlogViewModel blogView)
        {
      
            string condition = "";
            if (!string.IsNullOrEmpty(blogView.Title))
            {
                condition += " [BlogTitle] = @title, ";
            }
            if (!string.IsNullOrEmpty(blogView.Author))
            { 
                condition += " [BlogAuthor] = @author, ";
            }
            if (!string.IsNullOrEmpty(blogView.Content))
            {
                condition += " [BlogTitle] = @title, ";
            }
            if (condition.Length == 0)
            {
                return BadRequest("No fields to update");
            }
            condition = condition.Substring(0, condition.Length - 2);

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string updateQuery = $@"UPDATE [dbo].[Tbl_Blog]
                                        SET {condition}
                                        WHERE BlogId = @BlogId";

                SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                updateCmd.Parameters.AddWithValue("@BlogId", id);

                if (!string.IsNullOrEmpty(blogView.Title))
                    updateCmd.Parameters.AddWithValue("@BlogTitle", blogView.Title);
                if (!string.IsNullOrEmpty(blogView.Author))
                    updateCmd.Parameters.AddWithValue("@BlogAuthor", blogView.Author);
                if (!string.IsNullOrEmpty(blogView.Content))
                    updateCmd.Parameters.AddWithValue("@BlogContent", blogView.Content);

                int result = updateCmd.ExecuteNonQuery();
                return Ok(result > 0 ? "Partial update success" : "Update failed");
            }
        }

        // ✅ DELETE a blog
        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string deleteQuery = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @id";
                SqlCommand cmd = new SqlCommand(deleteQuery, con);
                cmd.Parameters.AddWithValue("@id", id);

                int result = cmd.ExecuteNonQuery();
                return Ok(result > 0 ? "Blog deleted successfully" : "Delete failed");
            }
        }
    }
}
