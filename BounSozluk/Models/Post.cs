using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BounSozluk.Models
{

    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime CreateDate { get; set; }

        [Display(Name = "Post Group")]
        public int PostGroupId { get; set; }
        public PostGroup PostGroup { get; set; }

        public string BounSozlukUserId { get; set; }
        public BounSozlukUser BounSozlukUser { get; set; }

        public IList<Comment> Comments { get; set; }
    }
}
