using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessComments
{
    public class CommentRepository : ICommentRepository
    {

        private ConfigurationData _connectionString;

        public CommentRepository(ConfigurationData connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection DBConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }


        public List<CommentsME> GetComments()  // <--
        {
            List<CommentsME> comments = new List<CommentsME>();

            try
            {
                SqlConnection dbConnection = DBConnection();
                SqlCommand command = new SqlCommand("GET_POSTS_COMMENTS", dbConnection); 
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandTimeout = 9999;

                dbConnection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CommentsME comment = new CommentsME();
                        PostsME post = new PostsME();

                        comment.postId = (reader.GetValue(0) != DBNull.Value ? reader.GetInt32(0) : 0);
                        comment.id = (reader.GetValue(1) != DBNull.Value ? reader.GetInt32(1) : 0);
                        comment.name = (!reader.IsDBNull(2) ? reader.GetString(2) : string.Empty);
                        comment.email = (!reader.IsDBNull(3) ? reader.GetString(3) : string.Empty);
                        comment.body = (!reader.IsDBNull(4) ? reader.GetString(4) : string.Empty);
                        
                        comments.Add(comment);
                    }

                    reader.Close();
                }

                dbConnection.Close();
                dbConnection.Dispose();

            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return comments;
        }

        
        public List<CommentsME> GetCommentsById(int id)
        {
            List<CommentsME> comments = new List<CommentsME>();

            try
            {
                SqlConnection dbConnection = DBConnection();
                SqlCommand command = new SqlCommand("GET_POSTS_COMMENTS_ID", dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandTimeout = 9999;

                command.Parameters.AddWithValue("@postId", id);

                dbConnection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {                  
                        CommentsME comment = new CommentsME();

                        comment.postId = (reader.GetValue(0) != DBNull.Value ? reader.GetInt32(0) : 0);
                        comment.id = (reader.GetValue(1) != DBNull.Value ? reader.GetInt32(1) : 0);
                        comment.name = (!reader.IsDBNull(2) ? reader.GetString(2) : string.Empty);
                        comment.email = (!reader.IsDBNull(3) ? reader.GetString(3) : string.Empty);
                        comment.body = (!reader.IsDBNull(4) ? reader.GetString(4) : string.Empty);

                        comments.Add(comment);

                    }
                }

                dbConnection.Close();
                dbConnection.Dispose();

            }

            catch (Exception ex)
            {
               ex.Message.ToString();
            }

            return comments;
        }


        public int CreateComment(CommentsME comment)
        {
            int createComment = 0;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("CREATE_COMMENT", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    command.Parameters.AddWithValue("@commentId", comment.id);
                    command.Parameters.AddWithValue("@postId", comment.postId); 
                    command.Parameters.AddWithValue("@name", comment.name);
                    command.Parameters.AddWithValue("@email", comment.email);
                    command.Parameters.AddWithValue("@body", comment.body);

                    dbConnection.Open();

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            createComment = dr.GetInt32(0);
                        }
                    }

                    dbConnection.Close();
                    dbConnection.Dispose();

                }
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return createComment;
        }


        public int ModifyComment(CommentsME comment)
        {
            int updateComment = 0;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("MODIFY_COMMENT", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    command.Parameters.AddWithValue("@postId", comment.postId);
                    command.Parameters.AddWithValue("@commentId", comment.id);
                    command.Parameters.AddWithValue("@name", comment.name);
                    command.Parameters.AddWithValue("@body", comment.body);

                    dbConnection.Open();

                    command.ExecuteNonQuery();

                    updateComment = 1;

                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return updateComment;
        }


        public int DeleteComment(int id)
        {
            int deleteComment = 0;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("DELETE_COMMENT", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    command.Parameters.AddWithValue("@id", id);

                    dbConnection.Open();

                    command.ExecuteNonQuery();

                    deleteComment = 1;

                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return deleteComment;
        }


        public int[] InsertComments(List<CommentsME> comments)
        {
            int[] valiCreate = new int[comments.Count()];
            int index = 0;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    dbConnection.Open();


                    foreach (CommentsME comment in comments)
                    {
                        int isCreate = 0;

                        SqlCommand command = new SqlCommand("CREATE_COMMENT", dbConnection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandTimeout = 1800;

                        command.Parameters.AddWithValue("@commentId", comment.id);
                        command.Parameters.AddWithValue("@postId", comment.postId);
                        command.Parameters.AddWithValue("@name", comment.name);
                        command.Parameters.AddWithValue("@email", comment.email);
                        command.Parameters.AddWithValue("@body", comment.body);

                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                isCreate = dr.GetInt32(0);
                            }
                        }

                        valiCreate[index] = isCreate;
                        index = index + 1;
                    }

                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return valiCreate;
        }

    }
}
