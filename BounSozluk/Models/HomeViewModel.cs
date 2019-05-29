using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BounSozluk.Models
{
    public class HomeViewModel
    {
        public List<Post> LastPosts { get; set; }
        public Post RandomPost { get; set; }
    }
}
