using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NnhlDotNetCoreTraining.Database.Models;

namespace NnhlDotNetCoreTraining.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsAdoDotNetController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();

        [HttpGet]
        public IActionResult GetBlogs()
        {
            
            return Ok();
        }


        [HttpGet("{id}")]
        public IActionResult EditBlogs(int id)
        {
            

            return Ok();
        }

        [HttpPost]
        public IActionResult CreateBlogs(TblBlog tblBlog)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, TblBlog tblBlog)
        {
            

            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, TblBlog tbl)

        {
            
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBlogs(int id)
        {

            return Ok();
        }

    }
}
