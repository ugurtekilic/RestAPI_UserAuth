# 🚀 Secure User Auth System (API & WinForms)

Bu proje, modern güvenlik standartları (JWT, BCrypt, Rate Limiting) kullanılarak geliştirilmiş kapsamlı bir **Kullanıcı Kayıt ve Giriş** sistemidir. Bir **ASP.NET Core Web API** sunucusu ve bu sunucuyla haberleşen bir **Windows Forms** istemcisinden oluşur.

## 🌟 Öne Çıkan Özellikler

- **Full-Stack Mimari:** Backend (Sunucu) ve Frontend (İstemci) ayrımı ile gerçek dünya senaryosu.
- **JWT Yetkilendirme:** Kullanıcılar giriş yaptıklarında dijital imzalı bir anahtar (Token) alırlar.
- **Gelişmiş Güvenlik:** 
  - **BCrypt:** Şifreler veritabanına asla düz metin olarak kaydedilmez, güçlü bir hash algoritması ile saklanır.
  - **Rate Limiting:** DDoS saldırılarına karşı saniyeler içindeki istek sayısı sınırlandırılmıştır.
  - **Account Lockout:** 5 kez hatalı denemede hesap otomatik olarak 15 dakika kilitlenir (Brute-Force koruması).
- **Asenkron İletişim:** WinForms tarafında UI donmalarını engellemek için tamamen `async/await` yapısı kullanılmıştır.

## 🛠 Teknoloji Yığını

### **Backend (API)**
- **.NET 8.0:** Modern ve hızlı çalışma zamanı.
- **Entity Framework Core:** Veritabanı yönetim aracı (ORM).
- **MariaDB / MySQL:** İlişkisel veritabanı.
- **Pomelo.EntityFrameworkCore.MySql:** MariaDB bağlantı sürücüsü.
- **JwtBearer:** Güvenli kimlik doğrulama middleware.

### **Frontend (WinForms)**
- **HttpClient:** API ile JSON veri alışverişi.
- **DTOs:** Veri taşıma nesneleri ile temiz kod yapısı.

- ### ⚙️ Kurulum ve Yapılandırma
**1. Veritabanı Hazırlığı
**MariaDB veya MySQL üzerinde aşağıdaki tabloyu oluşturun:

- **CREATE DATABASE UserDb;
- **USE UserDb;

- **CREATE TABLE Users (
  - **Id INT AUTO_INCREMENT PRIMARY KEY,
  - **Username VARCHAR(50) NOT NULL,
  - **Email VARCHAR(100) NOT NULL,
  - **Password VARCHAR(255) NOT NULL,
  - **AccessFailedCount INT DEFAULT 0,
  - **LockoutEnd DATETIME NULL
- **);

## 📂 Proje Yapısı

```text
├── UserAuthApi (Web API)
│   ├── Controllers/    # API Endpoint'leri (Auth/Login/Register)
│   ├── Data/           # DbContext ve Veritabanı Yapılandırması
│   ├── Models/         # Veritabanı tabloları ve DTO'lar
│   └── Program.cs      # Güvenlik ve Middleware ayarları
│
└── UserAuthWinForm (İstemci)
    ├── Forms/          # Login, Register ve Dashboard ekranları
    ├── Models/         # API ile uyumlu DTO sınıfları
    └── Program.cs      # Uygulama başlangıç noktası


