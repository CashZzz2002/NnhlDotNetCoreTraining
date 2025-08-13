//using System;
//using System.Data;
//using System.Data.SqlClient;

//namespace NnhlDotNetCoreTraining.ConsoleApp
//{
//    public class AdoDotNetExample
//    {
//        private readonly string _connectionString = "Data Source=DESKTOP-3CLQTFU;Initial Catalog=DotNetTrainingBatch5;Integrated Security=True;TrustServerCertificate=True;";

//        public void Read()
//        {
//            using (SqlConnection con = new SqlConnection(_connectionString))
//            {
//                con.Open();
//                string query = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent]
//                                 FROM [dbo].[Tbl_Blog]";

//                SqlCommand cmd = new SqlCommand(query, con);
//                DataTable dt = new DataTable();
//                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//                adapter.Fill(dt);
//            }
//            Console.WriteLine("connection is closed");
//        }

//        public void Insert()
//        {
//            using (SqlConnection connection = new SqlConnection(_connectionString))
//            {
//                Console.WriteLine("Blog title : ");
//                string title = Console.ReadLine();

//                Console.WriteLine("Author Name : ");
//                string author = Console.ReadLine();

//                Console.WriteLine("Blog Content : ");
//                string content = Console.ReadLine();

//                connection.Open();

//                string queryInsert = @"INSERT INTO [dbo].[Tbl_Blog] 
//                                       ([BlogTitle], [BlogAuthor], [BlogContent], [del_flg])
//                                       VALUES (@title, @author, @content, 0)";

//                SqlCommand sqlCommand = new SqlCommand(queryInsert, connection);
//                sqlCommand.Parameters.AddWithValue("@title", title);
//                sqlCommand.Parameters.AddWithValue("@author", author);
//                sqlCommand.Parameters.AddWithValue("@content", content);

//                int result = sqlCommand.ExecuteNonQuery();

//                if (result == 0)
//                    Console.WriteLine("Saving Failed");
//                else
//                    Console.WriteLine("Saving Success");
//            }
//        }

//        public void Update()
//        {
//            Console.Write("Blog id : ");
//            string id = Console.ReadLine();

//            using (SqlConnection connection = new SqlConnection(_connectionString))
//            {
//                connection.Open();

//                string selectQuery = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent], [del_flg]
//                                       FROM [dbo].[Tbl_Blog] 
//                                       WHERE BlogId=@BlogId;";

//                SqlCommand cmd = new SqlCommand(selectQuery, connection);
//                cmd.Parameters.AddWithValue("@BlogId", id);

//                DataTable dt = new DataTable();
//                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//                adapter.Fill(dt);

//                if (dt.Rows.Count == 0)
//                {
//                    Console.WriteLine("Data Not Found");
//                    return;
//                }

//                DataRow dr = dt.Rows[0];
//                Console.WriteLine($"ID: {dr["BlogId"]}");
//                Console.WriteLine($"Title: {dr["BlogTitle"]}");
//                Console.WriteLine($"Author: {dr["BlogAuthor"]}");
//                Console.WriteLine($"Content: {dr["BlogContent"]}");

//                Console.Write("New Title: ");
//                string newTitle = Console.ReadLine();

//                Console.Write("New Author: ");
//                string newAuthor = Console.ReadLine();

//                Console.Write("New Content: ");
//                string newContent = Console.ReadLine();

//                string updateQuery = @"UPDATE [dbo].[Tbl_Blog]
//                                       SET BlogTitle=@title,
//                                           BlogAuthor=@author,
//                                           BlogContent=@content
//                                       WHERE BlogId=@BlogId";

//                SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
//                updateCmd.Parameters.AddWithValue("@title", newTitle);
//                updateCmd.Parameters.AddWithValue("@author", newAuthor);
//                updateCmd.Parameters.AddWithValue("@content", newContent);
//                updateCmd.Parameters.AddWithValue("@BlogId", id);

//                int result = updateCmd.ExecuteNonQuery();

//                if (result > 0)
//                    Console.WriteLine("Update Successful");
//                else
//                    Console.WriteLine("Update Failed");
//            }
//        }
//        public void Delete()
//        {
//            Console.Write("Enter Blog Id: ");
//            string id = Console.ReadLine();

//            using (SqlConnection connection = new SqlConnection(_connectionString))
//            {
//                connection.Open();

//                // Step 1: Check if Blog exists
//                string selectQuery = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent], [del_flg]
//                               FROM [dbo].[Tbl_Blog] 
//                               WHERE BlogId=@BlogId;";

//                SqlCommand cmd = new SqlCommand(selectQuery, connection);
//                cmd.Parameters.AddWithValue("@BlogId", id);

//                DataTable dt = new DataTable();
//                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//                adapter.Fill(dt);

//                if (dt.Rows.Count == 0)
//                {
//                    Console.WriteLine("Data not found");
//                    return;
//                }

//                // Show blog info
//                DataRow dr = dt.Rows[0];
//                Console.WriteLine($"ID: {dr["BlogId"]}");
//                Console.WriteLine($"Title: {dr["BlogTitle"]}");
//                Console.WriteLine($"Author: {dr["BlogAuthor"]}");

//                // Step 2: Ask for confirmation
//                Console.Write("Are you sure you want to delete it? (y/n): ");
//                string confirm = Console.ReadLine();

//                if (confirm?.ToLower() != "y")
//                {
//                    Console.WriteLine("Delete cancelled.");
//                    return;
//                }

//                // Step 3: Delete
//                string deleteQuery = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId=@BlogId";
//                SqlCommand sql = new SqlCommand(deleteQuery, connection);
//                sql.Parameters.AddWithValue("@BlogId", id);

//                int result = sql.ExecuteNonQuery();

//                if (result > 0)
//                    Console.WriteLine("Delete successful");
//                else
//                    Console.WriteLine("Delete failed");
//            }
//        }
//    }

//        // Main Program Entry
//        class Program
//    {
//        static void Main(string[] args)
//        {
//            AdoDotNetExample adoDotNetExample = new AdoDotNetExample();

//            // Call Update
//            adoDotNetExample.Update();

//            // Call Insert
//            adoDotNetExample.Insert();

//            Console.ReadLine();
//        }
//    }
//}
using System.Data;
using System.Data.SqlClient;

namespace NnhlDotNetCoreTraining.ConsoleApp
{
    public class AdoDotNetExample
    {
        private readonly string _connectionString = "Data Source=DESKTOP-3CLQTFU;Initial Catalog=DotNetTrainingBatch5;Integrated Security=True;TrustServerCertificate=True;";
        public void Delete()
        {
            Console.WriteLine("Enter Blog Id :");
            string id = Console.ReadLine();
            
            SqlConnection sqlConnection=new SqlConnection(_connectionString);
            sqlConnection.Open();

            string selectQuery = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent], [del_flg]
                                   FROM [dbo].[Tbl_Blog] 
                                   WHERE BlogId=@BlogId;";

            SqlCommand cmd = new SqlCommand(selectQuery, sqlConnection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            DataTable dt=new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
              if (dt.Rows.Count == 0)
                {
                               Console.WriteLine("Data not found");
                                return;
                 }
            DataRow dr = dt.Rows[0];
            Console.WriteLine($"ID :{dr["BlogId"]}");
            Console.WriteLine($"Title :{ dr["BlogTitle"]}");
            Console.WriteLine($"Author :{dr ["BlogAuthor"]}");
            Console.WriteLine($"Content :{ dr["BlogContent"]}");

            Console.WriteLine("Are you sure you want to delete it? (y/n): ");
            string confirm=Console.ReadLine();
            if (confirm?.ToLower() != "y")
            {
                Console.WriteLine("Delete is Cancelled");
            }
        
            string deleteQuery = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId=@BlogId";
            SqlCommand cm=new SqlCommand(deleteQuery, sqlConnection);
            cm.Parameters.AddWithValue("@BlogId", id);
            int result=cm.ExecuteNonQuery();

            if (result==0)
            {
                Console.WriteLine("Delete is failed ");
                return;
            }
            else
            {
                Console.WriteLine("Delete is Successfully!");
                return;
            }
            sqlConnection.Close();
            
        }
    }
}
