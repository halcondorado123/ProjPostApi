using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CommentsME
    {
        public int postId { get; set; } //<-- PostsME (Est funcion se reemplazo y funciono)
        
        public int id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string body { get; set; }
              
    }
}
