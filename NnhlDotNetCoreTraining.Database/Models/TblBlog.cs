using System;
using System.Collections.Generic;

namespace NnhlDotNetCoreTraining.Database.Models;

public partial class TblBlog
{
    public int BlogId { get; set; }

    public string BlogTitle { get; set; } = null!;

    public string? BlogAuthor { get; set; }

    public string? BlogContent { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? DelFlg { get; set; }
}
