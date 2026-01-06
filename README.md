🚀 Secure User Auth API & WinForms Client
Bu proje, modern güvenlik standartlarına uygun olarak geliştirilmiş bir ASP.NET Core Web API ve bu API'yi tüketen bir Windows Forms istemcisinden oluşmaktadır. Kullanıcı kayıt, giriş ve yetkilendirme süreçlerini uçtan uca yönetir.
🛠 Kullanılan Teknolojiler
Backend (API)
.NET 8.0: En güncel .NET framework sürümü.
Entity Framework Core: Veritabanı işlemleri (ORM) için.
MariaDB (MySQL): Veri depolama.
JWT (JSON Web Token): Güvenli kimlik doğrulama ve yetkilendirme.
BCrypt.Net-Next: Şifrelerin güvenli bir şekilde hash'lenmesi.
Rate Limiting: DDoS saldırılarını engellemek için istek sınırlama.
Frontend (WinForms)
HttpClient: API ile asenkron (async/await) iletişim.
DTO (Data Transfer Objects): Sunucu ve istemci arasında veri taşıma kalıpları.
✨ Temel Özellikler & Güvenlik
Güvenli Şifreleme: Şifreler veritabanında asla düz metin olarak tutulmaz; BCrypt algoritması ile hash'lenir.
JWT Auth: Kullanıcı giriş yaptığında bir dijital anahtar (Token) alır ve yetki gerektiren sayfalara bu anahtar ile erişir.
Account Lockout (Hesap Kilitleme): 5 kez hatalı giriş denemesinde hesap otomatik olarak 15 dakika boyunca kilitlenir (Brute-force koruması).
DDoS Koruması (Rate Limiting): Aynı IP üzerinden saniyede çok fazla istek gelmesi durumunda API kendini korumaya alır.
Gelişmiş Hata Yönetimi: API'den dönen özel hata mesajları (401 Unauthorized, 429 Too Many Requests vb.) kullanıcıya arayüzde dinamik olarak gösterilir.
⚙️ Kurulum ve Çalıştırma
Veritabanı Yapılandırması:
UserDb adında bir MariaDB veritabanı oluşturun.
appsettings.json dosyasındaki ConnectionStrings bölümünü kendi şifrenizle güncelleyin.
API'yi Başlatın:
Visual Studio'da projeyi çalıştırdığınızda Swagger arayüzü açılacaktır.
WinForms İstemcisini Bağlayın:
LoginForm.cs içindeki https://localhost:XXXX adresini kendi API portunuzla güncelleyin.
Çoklu Başlatma:
Solution üzerine sağ tıklayarak hem API hem WinForms projesini aynı anda başlatacak şekilde ayarlayın.
🚀 English Version: Secure User Auth API
A full-stack authentication system featuring an ASP.NET Core Web API and a WinForms client. Built with security-first principles.
🛡️ Security Highlights
Password Hashing: Utilizing BCrypt for one-way secure hashing.
Authorization: Token-based security using JWT.
Brute-Force Protection: Account lockout mechanism after 5 failed attempts.
Anti-DDoS: Fixed window rate limiting implemented in .NET 8.
Clean Architecture: Use of DTOs for decoupled data handling.
GitHub için Küçük Bir İpucu:
Projeni GitHub'a yüklerken şu dosyaların gitmediğinden emin ol (Genelde .gitignore dosyası bunu halleder):
bin/ ve obj/ klasörleri.
appsettings.json içindeki şifrelerin (Eğer projen çok gizliyse, örnek bir appsettings.Example.json koyabilirsin).
