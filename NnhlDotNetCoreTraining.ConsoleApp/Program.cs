// See https://aka.ms/new-console-template for more information


using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
//Console.ReadLine();
//Console.ReadKey();
//Console.ReadLine();
//C#  <=> database
//ADO.Net
//Dapper
//EfCore(Entity framework)
//string ConnectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=dO95t:7NdGfU";
//Console.WriteLine("Connection String"+ConnectionString);
//SqlConnection connection = new SqlConnection(ConnectionString);
//Console.WriteLine("Connection is opening");
//connection.Open();
//Console.WriteLine("Connection is opened");

//string query = @"SELECT [BlogId]
//      ,[BlogTitle]
//      ,[BlogAuthor]
//      ,[BlogContent]
//  FROM [dbo].[Tbl_Blog]";

//SqlCommand cmd =new SqlCommand(query, connection);
//SqlDataAdapter adapter = new SqlDataAdapter();

//DataTable dataTable=new DataTable();
//adapter.Fill(dataTable);

//Console.WriteLine("Connection is closing");
//connection.Close();
//Console.WriteLine("Connection is closed");
//Console.ReadKey();
string ConnectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=dO95t:7NdGfU";
SqlConnection con =new SqlConnection(ConnectionString);
con.Open();
string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Tbl_Blog]";

SqlCommand cmd = new SqlCommand(query, con);
DataTable dt = new DataTable();
SqlDataAdapter adapter = new SqlDataAdapter(cmd);
adapter.Fill(dt);
con.Close();
Console.WriteLine("connection is closed");