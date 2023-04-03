using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessComments
{
    public interface ICommentRepository
    {
        List<CommentsME> GetComments();    // <--

        List<CommentsME> GetCommentsById(int id);

        int CreateComment(CommentsME comment);

        int ModifyComment(CommentsME comment);

        int DeleteComment(int id);

        int[] InsertComments(List<CommentsME> comments);
    }
}
