using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessPosts
{
    public class PostRepository : IPostRepository
    {

        private ConfigurationData _connectionString;

        public PostRepository(ConfigurationData connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection DBConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }


        public List<PostsME> GetPosts()
        {
            List<PostsME> posts = new List<PostsME>();

            try
            {
                SqlConnection dbConnection = DBConnection();
                SqlCommand command = new SqlCommand("GET_POSTS", dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandTimeout = 9999;

                dbConnection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PostsME post = new PostsME();

                        post.userId = (reader.GetValue(0) != DBNull.Value ? reader.GetInt32(0) : 0);
                        post.id = (reader.GetValue(1) != DBNull.Value ? reader.GetInt32(1) : 0);
                        post.title = (!reader.IsDBNull(2) ? reader.GetString(2) : string.Empty);
                        post.body = (!reader.IsDBNull(3) ? reader.GetString(3) : string.Empty);
                                  
                        posts.Add(post);
                    }
                }

                dbConnection.Close();
                dbConnection.Dispose();

            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return posts;
        }


        public PostsME GetPostById(int id)
        {
            PostsME post = new PostsME();

            try
            {
                SqlConnection dbConnection = DBConnection();
                SqlCommand command = new SqlCommand("GET_POST_BY_ID", dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandTimeout = 9999;

                command.Parameters.AddWithValue("@id", id);

                dbConnection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        post.userId = (reader.GetValue(0) != DBNull.Value ? reader.GetInt32(0) : 0);
                        post.id = (reader.GetValue(1) != DBNull.Value ? reader.GetInt32(1) : 0);
                        post.title = (!reader.IsDBNull(2) ? reader.GetString(2) : string.Empty);
                        post.body = (!reader.IsDBNull(3) ? reader.GetString(3) : string.Empty);

                    }

                    dbConnection.Close();
                    dbConnection.Dispose();

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return post;
        }


        public int CreatePost(PostsME post)
        {
            int valiCreate = 0;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("CREATE_POST", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    command.Parameters.AddWithValue("@postId", post.id);
                    command.Parameters.AddWithValue("@title", post.title);
                    command.Parameters.AddWithValue("@body", post.body);
                    command.Parameters.AddWithValue("@userId", post.userId);

                    dbConnection.Open();

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            valiCreate = dr.GetInt32(0);
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

            return valiCreate;
        }


        public int ModifyPost(PostsME post)
        {
            int isUpdatePost = 0;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("MODIFY_POST", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    command.Parameters.AddWithValue("@id", post.id);
                    command.Parameters.AddWithValue("@title", post.title);
                    command.Parameters.AddWithValue("@body", post.body);

                    dbConnection.Open();

                    command.ExecuteNonQuery();

                    isUpdatePost = 1;

                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return isUpdatePost;
        }

        public int DeletePost(int id)
        {
            int isDelPos = 0;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("DELETE_POST", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    command.Parameters.AddWithValue("@id", id);

                    dbConnection.Open();

                    command.ExecuteNonQuery();

                    isDelPos = 1;

                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return isDelPos;
        }


        public int[] InsertPosts(List<PostsME> posts)
        {
            int[] valiCreate = new int[posts.Count()];
            int index = 0;

            try
            {            
                using (SqlConnection dbConnection = DBConnection())
                {
                    dbConnection.Open();

                    foreach (PostsME post in posts)
                    {
                        int isCreate = 0;

                        SqlCommand command = new SqlCommand("CREATE_POST", dbConnection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandTimeout = 1800;

                        command.Parameters.AddWithValue("@postId", post.id);
                        command.Parameters.AddWithValue("@title", post.title);
                        command.Parameters.AddWithValue("@body", post.body);
                        command.Parameters.AddWithValue("@userId", post.userId);

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
