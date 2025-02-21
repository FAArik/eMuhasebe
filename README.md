# eMuhasebe Uygulaması

Bu proje, .NET 8 ve Angular 17 kullanılarak geliştirilmiş bir e-muhasebe uygulamasıdır. Clean Architecture prensiplerine dayanarak tasarlanmış olup, muhasebe süreçlerini yönetmek için çeşitli modüller içermektedir.

## 🚀 Özellikler

- **Kullanıcı Yönetimi**: Yetkilendirme ve kimlik doğrulama.
- **Şirket Yönetimi**: Firma bilgileri ve ayarları.
- **Kasa ve Banka Yönetimi**: Nakit ve banka hesaplarının takibi.
- **Cari Hesap Yönetimi**: Müşteri ve tedarikçi yönetimi.
- **Stok Yönetimi**: Ürünlerin giriş-çıkış işlemleri.
- **Fatura Yönetimi**: Alış ve satış faturaları.
- **Raporlama**: Finansal durum ve performans analizi.

## 🛠️ Kullanılan Teknolojiler

- **Backend**: .NET 8, Clean Architecture, CQRS Pattern, Result Pattern, Repository Pattern (Unit of Work ile)
- **Frontend**: Angular 17
- **Veritabanı**: Entity Framework Core (EF Core) - Code-First  + Migrations
- **Diğer Kütüphaneler**:
    - AutoMapper
    - FluentValidation
    - SmartEnum
    - JWT Authentication
    - Fake SMTP (Mail işlemleri için)

## 🔧 Kurulum

### 1. Depoyu Klonlayın
```sh
git clone https://github.com/FAArik/eMuhasebe/
cd eMuhasebe
```

### 2. Backend Kurulumu

Bağımlılıkları yükleyin:
```sh
cd backend
dotnet restore
```

Veritabanı migrations'larını çalıştırın:
```sh
dotnet ef database update
```

Uygulamayı başlatın:
```sh
dotnet run
```

### 3. Frontend Kurulumu
```sh
cd frontend
npm install
ng serve
```
