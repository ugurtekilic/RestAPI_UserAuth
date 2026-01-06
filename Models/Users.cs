namespace UserAuthApi.Models // Bu dosyanın projedeki "adresi"
{
    public class User
    {
        // Her kullanıcının benzersiz bir numarası olur (Veritabanında otomatik artacak)
        public int Id { get; set; }

        // Kullanıcı adı
        public string Username { get; set; } = string.Empty;

        // E-posta adresi
        public string Email { get; set; } = string.Empty;

        // Şifre
        public string Password { get; set; } = string.Empty;

        // Hatalı giriş sayısı
        public int AccessFailedCount { get; set; }

        // Kilidin biteceği tarih (boş olabilir)
        public DateTime? LockoutEnd { get; set; }  // Kilidin biteceği tarih (boş olabilir)
    }
}