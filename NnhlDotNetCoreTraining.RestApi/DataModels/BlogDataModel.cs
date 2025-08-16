namespace NnhlDotNetCoreTraining.RestApi.DataModels
{
    public class BlogDataModel
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set; }

        public string BlogContent { get; set; }
        public bool del_Flg { get; set; }
    }
}