using DataAccess.DataAccessPosts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjPostApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IPostRepository _IPostRepository;

        public PostsController(IPostRepository postRepository)
        {
            _IPostRepository = postRepository;
        }

        [HttpGet]
        //[Route("api/[controller]")]
        //[HttpGet("api/[controller]")]
        public ActionResult GetPosts()
        {
            try
            {
                return Ok(_IPostRepository.GetPosts());
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        //[Route("api/[controller]/{id}")]
        //[HttpGet("api/[controller]/{id}")]
        public ActionResult GetPostById(int postId)
        {
            try
            {
                return Ok(_IPostRepository.GetPostById(postId));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        //[Route("api/[controller]")]
        //[HttpPost("api/[controller]")]
        public ActionResult CreatePost([FromBody] PostsME post)
        {
            try
            {
                return Ok(_IPostRepository.CreatePost(post));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        //[Route("api/[controller]/{id}")]
        //[HttpPut("api/[controller]/{id}")]
        public ActionResult ModifyPost([FromBody] PostsME post)
        {
            try
            {
                return Ok(_IPostRepository.ModifyPost(post));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete("{id}")]
        //[Route("api/[controller]/{id}")]
        //[HttpDelete("api/[controller]/{id}")]
        public ActionResult DeletePost(int id)
        {
            try
            {
                return Ok(_IPostRepository.DeletePost(id));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
