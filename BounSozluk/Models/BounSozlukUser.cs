using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BounSozluk.Models
{
    public class BounSozlukUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Surname { get; set; }

        [StringLength(200)]
        public string PhotoUrl { get; set; }

        public virtual IList<Post> Posts { get; set; }

        public virtual IList<Like> Likes { get; set; }
    }
}
