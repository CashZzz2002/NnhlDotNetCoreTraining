using NnhlDotNetCoreTraining.ConsoleApp.Models;
using NnhlDotNetCoreTraining.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NnhlDotNetCoreTraining.ConsoleApp
{
    public class DapperExample2
    {
        private readonly DapperService _dapperService;
        private readonly string _connectionString = "Data Source=DESKTOP-3CLQTFU;Initial Catalog=DotNetTrainingBatch5;Integrated Security=True;TrustServerCertificate=True;";


        public DapperExample2()
        {
            _dapperService=new DapperService(_connectionString);
        }
        public void Read()
        {
            string query = "Select * from tbl_blog where del_flg=0;";
            var lst = _dapperService.Query<BlogDataModel>(query).ToList();//query ko execute lok tr
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);

            }
        }
        public void Edit(int id)
        {

            string query = "Select * from tbl_blog where del_flg = 0 and BlogId =@BlogId";
            var item = _dapperService.QueryFirstOrDefault<BlogDapperDataModel>(query, new BlogDapperDataModel
            {
                BlogId = id
            });

            if (item is null)
            {
                Console.WriteLine("data not found ...");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);



        }
        public void Create(string title, string author, string content)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_Blog] 
                                    ([BlogTitle], 
                                    [BlogAuthor],
                                    [BlogContent],
                                    [del_flg])
                             VALUES (@BlogTitle, @BlogAuthor, @BlogContent, 0)";

            
            
                int result = _dapperService.ExecuteQuery(query, new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content,
                });
                Console.WriteLine(result == 1 ? "Saving is Successful" : "Saving is failed");
            
        }
    }
} 