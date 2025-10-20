using Microsoft.EntityFrameworkCore;
using Poliedro.Client.Domain.ClientPos.Entities;

namespace Poliedro.Client.Infraestructure.Persistence.Mysql.Context
{
    public class ClientDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ClientLegalPosEntity> ClientLegalPos { get; set; }
        public DbSet<ClientNaturalPosEntity> ClientNaturalPos { get; set; }
        public DbSet<DocumentTypeEntity> DocumentTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityConfiguration(modelBuilder);
        }

        private static void EntityConfiguration(ModelBuilder modelBuilder)
        {

        }
    }
}
