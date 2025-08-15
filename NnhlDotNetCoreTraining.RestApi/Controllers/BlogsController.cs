using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NnhlDotNetCoreTraining.Database.Models;

namespace NnhlDotNetCoreTraining.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();

        [HttpGet]
        public IActionResult GetBlogs() {
            var lst = _db.TblBlogs.AsNoTracking()
                 .ToList();
            return Ok(lst);
        }


        [HttpGet("{id}")]
        public IActionResult EditBlogs(int id)
        {
            var item    = _db.TblBlogs.AsNoTracking().FirstOrDefault(x=>x.BlogId==id);
                 
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlogs(TblBlog tblBlog)
        {
            _db.TblBlogs.Add(tblBlog);
            _db.SaveChanges();

            return Ok(tblBlog);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id,TblBlog tblBlog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x=>x.BlogId==id);
            if (item == null) {
                return NotFound();
            }

            item.BlogTitle=tblBlog.BlogTitle;
            item.BlogAuthor=tblBlog.BlogAuthor;
            item.BlogContent = tblBlog.BlogContent;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(item);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id,TblBlog tbl)

        {
            var item = _db.TblBlogs.AsNoTracking()
                .FirstOrDefault(x =>x.BlogId==id);
            if (item == null) {
                return NotFound();
            }
            if (string.IsNullOrEmpty(tbl.BlogTitle)) { 
                item.BlogTitle = tbl.BlogTitle; 
            }
            if (string.IsNullOrEmpty(tbl.BlogAuthor))
            {
                item.BlogAuthor = tbl.BlogAuthor;
            }
            if (string.IsNullOrEmpty(tbl.BlogContent))
            {
                item.BlogContent= tbl.BlogContent;
            }

            _db.Entry(item).State= EntityState.Modified;
            _db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBlogs(int id)
        {

            return Ok();
        }

    }
}
