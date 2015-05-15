using System.Data.Entity;

namespace EventPlanner.Models
{
    public class EvenementEntities : DbContext
    {
        public EvenementEntities()
            : base("name=EvenementEntities")
        {
        }

        public virtual DbSet<Evenement> Evenementen { get; set; }
        public virtual DbSet<Straat> Straten { get; set; }
        public virtual DbSet<Periode> Periodes { get; set; }
        public virtual DbSet<Dag> Dagen { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evenement>()
                .Property(e => e.Omschrijving)
                .IsUnicode(false);

            modelBuilder.Entity<Evenement>()
                .Property(e => e.Eigenaar)
                .IsUnicode(false);

            modelBuilder.Entity<Evenement>()
                .HasMany<Periode>(v => v.Periodes)
                .WithRequired(p => p.Evenement)
                .HasForeignKey(p => p.VergunningId);

            modelBuilder.Entity<Periode>()
                .HasMany(p => p.Dagen)
                .WithRequired(d => d.Periode)
                .HasForeignKey(d => d.PeriodeId);

            modelBuilder.Entity<Periode>()
                .HasMany(p => p.Straten)
                .WithMany(s => s.Periodes)
                .Map(m =>
                {
                    m.MapLeftKey("periodeid");
                    m.MapRightKey("straatid");
                    m.ToTable("locatie");
                });
        }
    }
}