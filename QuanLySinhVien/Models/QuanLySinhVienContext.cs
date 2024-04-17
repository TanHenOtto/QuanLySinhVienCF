using Microsoft.EntityFrameworkCore;

namespace QuanLySinhVien
{
    public class QuanLySinhVienContext : DbContext
    {
        public QuanLySinhVienContext(DbContextOptions<QuanLySinhVienContext> options) : base(options)
        {
        }
        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<Lop> Lops { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships between entities
            modelBuilder.Entity<Lop>()
                .HasMany(l => l.SinhViens)
                .WithOne(sv => sv.Lop)
                .HasForeignKey(sv => sv.LopId);
            // Configure other relationships as needed
            modelBuilder.Entity<Khoa>()
             .HasMany(k => k.Lops)         // Each Khoa can have multiple Lops
             .WithOne(l => l.Khoa)         // Each Lop belongs to one Khoa
             .HasForeignKey(l => l.KhoaId);  // Foreign key in Lop referring to Khoa
            }
    }
}
