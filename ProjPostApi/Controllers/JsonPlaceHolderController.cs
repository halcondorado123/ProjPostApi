using ApiConnections;
using DataAccess.DataAccessComments;
using DataAccess.DataAccessPosts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ApiConnections;
using System.Threading.Tasks;

namespace ProjPostApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class JsonPlaceHolderController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public JsonPlaceHolderController(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }


        [HttpGet]
        public ActionResult GetAndInsertPosts()
        {
            int[] ids = new int[] { };

            try
            {
                List<PostsME> posts = new List<PostsME>();
                List<CommentsME> comments = new List<CommentsME>();
                JsonPlaceHolder placeHolder = new JsonPlaceHolder();

                posts = placeHolder.GetApiPosts();

                if (posts != null && posts.Count() > 0)
                {
                    ids = _postRepository.InsertPosts(posts);

                    int idsPostsCount = ids.Where(d => d > 0).Count();

                    if (ids != null && ids.Count() > 0)
                    {
                        comments = placeHolder.GetApiComments();

                        ids = _commentRepository.InsertComments(comments);

                        int idsCommentsCount = ids.Where(d => d > 0).Count();

                        return Ok("Datos insertados correctamente. Posts insertados: " + idsPostsCount + "/" + posts.Count() + 
                            ", Comentarios insertados: " + idsCommentsCount + "/" + comments.Count());
                    }

                    throw new InvalidOperationException("No se encontró data");
                }
                else
                {
                    throw new InvalidOperationException("No se encontró data");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
