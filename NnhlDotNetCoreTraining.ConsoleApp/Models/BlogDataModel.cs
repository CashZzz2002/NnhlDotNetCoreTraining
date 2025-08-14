using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NnhlDotNetCoreTraining.ConsoleApp.Models
{
    public class BlogDapperDataModel
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set;}
        public string BlogAuthor { get; set;}
        public string BlogContent { get; set;}

        public int del_flg { get; set;}
       }

    [Table("Tbl_Blog")]
    public class BlogDataModel
    {
        [Key]
        [Column ("BlogId")]
        public int BlogId { get; set; }

        [Column("BlogTitle")]
        public string BlogTitle { get; set; }

        [Column("BlogAuthor")]
        public string BlogAuthor { get; set; }

        [Column("BlogContent")]
        public string BlogContent { get; set; }

        [Column("del_flg")]
        public bool del_flg { get; set; }
    }
}