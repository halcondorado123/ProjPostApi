using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessPosts
{
    public interface IPostRepository
    {
        List<PostsME> GetPosts();

        PostsME GetPostById(int id);

        int CreatePost(PostsME post);

        int ModifyPost(PostsME post);

        int DeletePost(int id);

        int[] InsertPosts(List<PostsME> posts);
    }
}
