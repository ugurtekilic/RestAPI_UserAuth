using Microsoft.EntityFrameworkCore;
using UserAuthApi.Models; // Model dosyamızı görebilmesi için

namespace UserAuthApi.Data
{
    // DbContext sınıfından miras alıyoruz, bu sınıf EF Core'un kalbidir.
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Veritabanındaki 'Users' tablosunu temsil eder.
        // Buradaki 'Users' ismi, veritabanındaki tablo adıyla aynı olmalıdır.
        public DbSet<User> Users { get; set; }
    }
}