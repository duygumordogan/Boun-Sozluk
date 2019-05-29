using System;
using System.Collections.Generic;
using System.Text;
using BounSozluk.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BounSozluk.Data
{
    public class ApplicationDbContext : IdentityDbContext<BounSozlukUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BounSozluk.Models.PostGroup> PostGroup { get; set; }
        public DbSet<BounSozluk.Models.Post> Post { get; set; }
        public DbSet<BounSozluk.Models.Comment> Comment { get; set; }
        public DbSet<BounSozluk.Models.Like> Like { get; set; }
    }
}
