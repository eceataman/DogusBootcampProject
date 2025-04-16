# DogusBootcampProject 🧑‍💻

Bu proje, .NET Core (ASP.NET MVC) ve Entity Framework Core kullanılarak geliştirilmiş bir **Blog Yönetim Sistemi**dir. Uygulama kullanıcıların blog yazması, yorum yapması, hesap yönetimi gibi birçok özelliği desteklemektedir.

## 🚀 Özellikler

### 📝 Blog Sistemi
- Kullanıcılar giriş yaparak blog yazısı oluşturabilir.
- Kategori seçimi ve görsel yükleme desteklenir.
- Bloglar silindiğinde yorumları da otomatik olarak silinir (Cascade delete).
- Dinamik blog arama ve kategori filtreleme (AJAX destekli).

### 💬 Yorum Sistemi
- Giriş yapmış kullanıcılar bloglara yorum yapabilir.
- Admin, kullanıcı yorumlarına yanıt verebilir.
- Yorumlar alt alta (reply) mantığı ile gösterilir.

### 👤 Kimlik Yönetimi
- Kullanıcı kayıt olma, giriş yapma, çıkış yapma.
- Roller: Admin ve normal kullanıcı ayrımı yapılır.
- Giriş yapan kullanıcının adı üst menüde görünür.

### 🔐 Şifremi Unuttum Özelliği
- "Şifremi Unuttum" bağlantısı sayesinde kullanıcılar e-posta adreslerini girerek sıfırlama linki alır.
- E-posta içindeki bağlantı ile yeni şifre oluşturabilirler.
- Mail gönderimi SMTP üzerinden yapılandırılmıştır (Gmail desteği).

### 🌗 Tema Desteği
- Açık/Karanlık tema geçişi sağ üstteki `🌙` butonuyla yapılabilir.

### 1. Projeyi Klonlayın

```bash
git clone https://github.com/kullanici/DogusBootcampProject.git
cd DogusBootcampProject
