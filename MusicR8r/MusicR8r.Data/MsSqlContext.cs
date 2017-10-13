using Microsoft.AspNet.Identity.EntityFramework;
using MusicR8r.Data.Model.Contracts;
using MusicR8r.Data.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicR8r.Data
{
    public class MsSqlDbContext : IdentityDbContext<User>
    {
        public MsSqlDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Album> Albums { get; set; }
        public IDbSet<Song> Songs { get; set; }
        public IDbSet<Artist> Artists { get; set; }
        public IDbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            foreach (var entry in
                this.ChangeTracker.Entries()
                .Where(
                    e =>
                    e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditable)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == null)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        public static MsSqlDbContext Create()
        {
            return new MsSqlDbContext();
        }

        //public System.Data.Entity.DbSet<MusicR8r.Areas.Admin.Models.AlbumViewModel> AlbumViewModels { get; set; }
    }
}
