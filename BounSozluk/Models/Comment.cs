using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BounSozluk.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }

        public string BounSozlukUserId { get; set; }
        public BounSozlukUser BounSozlukUser { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public virtual IList<Like> Likes { get; set; }
    }
}
