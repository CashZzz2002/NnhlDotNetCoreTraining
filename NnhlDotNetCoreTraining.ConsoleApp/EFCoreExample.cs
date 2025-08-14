using Microsoft.EntityFrameworkCore;
using NnhlDotNetCoreTraining.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NnhlDotNetCoreTraining.ConsoleApp
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst=db.Blogs.Where(x=>x.del_flg==false).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);

            }
        }

        public void Create(string title, string author, string content)
        {
            BlogDataModel blogDataModel = new BlogDataModel
            {
                BlogTitle= title,
                BlogAuthor= author,
                BlogContent= content,

            };
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blogDataModel);
            var result =db.SaveChanges();
              
                Console.WriteLine(result == 1 ? "Saving is Successful" : "Saving is failed");
            }
        public void edit(int id)
        {
            AppDbContext dbContext=new AppDbContext();
            var item=dbContext.Blogs.FirstOrDefault(x=>x.BlogId==id);
            if (item == null) {
                Console.WriteLine("");
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);    
        }


        public void Update(int id,string title,string author,string content)
        {
            AppDbContext db=new AppDbContext();
            var item=db.Blogs
                .AsNoTracking()//database hte ka data ko track ma lok pl update process ko lok tr//ma pr vuu so one data one data 2 times so a sin ma pyay
                .FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("Data is not exists");
                return;
            }
            if (string.IsNullOrEmpty(title))
            {
                item.BlogTitle = title;
            }
            if (string.IsNullOrEmpty(author))
            {
                item.BlogAuthor = author;
            }

            if (string.IsNullOrEmpty(content))
            {
                item.BlogContent = content;
            }

            db.Entry(item).State= EntityState.Modified;

            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");
        }

        public void Delete(int id, string title, string author, string content)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs
                .AsNoTracking()//database hte ka data ko track ma lok pl update process ko lok tr//ma pr vuu so one data one data 2 times so a sin ma pyay
                .FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("Data is not exists");
                return;
            }
            db.Entry(item).State = EntityState.Deleted;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Delete Successful" : "Delete Failed");
        }

        }
    }
