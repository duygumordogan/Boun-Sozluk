using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BounSozluk.Models
{
    public class Like
    {
        [Key]
        public int Id { get; set; }

        public string BounSozlukUserId { get; set; }
        public BounSozlukUser BounSozlukUser { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
