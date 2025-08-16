using Dapper;
using NnhlDotNetCoreTraining.ConsoleApp.Models;
using NnhlDotNetCoreTraining.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NnhlDotNetCoreTraining.ConsoleApp
{
    public class AdoDotNetExample2

    {
        private readonly string _connectionString = "Data Source=DESKTOP-3CLQTFU;Initial Catalog=DotNetTrainingBatch5;Integrated Security=True;TrustServerCertificate=True;";

        private readonly AdoDotNetService _adoDotNetService;

        public AdoDotNetExample2()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }
        public void Read()
        {
            string query = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent]
                                 FROM [dbo].[Tbl_Blog]";
            var dt = _adoDotNetService.Query(query);

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);

            }
        }

        public void Edit()
        {
            Console.WriteLine("Enter the Blog Id :");

            string id = Console.ReadLine();

            string selectQuery = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent], [del_flg]
                                       FROM [dbo].[Tbl_Blog] 
                                       WHERE BlogId=@BlogId;";
            //SqlParameterModel[] sqlParameters = new SqlParameterModel[1];
            //sqlParameters[0] = new SqlParameterModel
            //{
            //    Name = "@BlogId",
            //    Value = id
            //};
            //var dt=_adoDotNetService.Query(selectQuery,sqlParameters);

            var dt = _adoDotNetService.Query(selectQuery, new SqlParameterModel("BlogId",id));
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
            }

        }
        public void Create()
        {
            Console.WriteLine("Blog title : ");
            string title = Console.ReadLine();

            Console.WriteLine("Author Name : ");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content : ");
            string content = Console.ReadLine();

            string queryInsert = @"INSERT INTO [dbo].[Tbl_Blog] 
                                       ([BlogTitle], [BlogAuthor], [BlogContent], [del_flg])
                                       VALUES (@title, @author, @content, 0)";

            int result = _adoDotNetService.Execute(queryInsert, new SqlParameterModel("@BlogTitle", title),
                            new SqlParameterModel("@BlogAuthor", author),new SqlParameterModel("@BlogContent", content));
            Console.WriteLine(result==1? "Success":"Failed");
        }
        
    }
}
