using DataAccess.DataAccessComments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjPostApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CommentsController : Controller
    {

        private readonly ICommentRepository _ICommentRepository;

        public CommentsController(ICommentRepository commentRepository)
        {
            
            _ICommentRepository = commentRepository;
        }

        [HttpGet]
        //[Route("api/[controller]")]
        public ActionResult GetComments()
        {
            try
            {
                return Ok(_ICommentRepository.GetComments());     // <--
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
                

        [HttpGet("{id}")]
        //[Route("api/[controller]/{id}")]
        public ActionResult GetCommentsById(int id)
        {
            try
            {
                return Ok(_ICommentRepository.GetCommentsById(id));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        //[Route("api/[controller]")]
        public ActionResult CreateComment([FromBody] CommentsME comment)
        {
            try
            {
                return Ok(_ICommentRepository.CreateComment(comment));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        //[Route("api/[controller]/{id}")]
        public ActionResult ModifyComment([FromBody] CommentsME id)
        {
            try
            {
                return Ok(_ICommentRepository.ModifyComment(id));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete("{id}")]
        //[Route("api/[controller]/{id}")]
        public ActionResult DeleteComment(int id)
        {
            try
            {
                return Ok(_ICommentRepository.DeleteComment(id));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
