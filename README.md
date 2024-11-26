# NeredeKal Hotel&Report Service
---

## Proje Açýklamasý

Bu proje, bir otel rehberi uygulamasý için iki mikroservis içermektedir:
1. **Hotel Service**: Otellerin yönetimi ve iletiþim bilgilerinin yönetilmesini saðlar.
2. **Report Service**: Otel verilerine dayalý raporlar oluþturur ve RabbitMQ kullanarak asenkron mesajlaþma saðlar.

Proje þu teknolojilere dayanmaktadýr:
- **ASP.NET Core**: Web API geliþtirme.
- **PostgreSQL**: Veritabaný.
- **Elasticsearch**: Loglama ve arama.
- **Kibana**: Elasticsearch üzerinde görselleþtirme.
- **RabbitMQ**: Mesaj kuyruðu.
- **Docker Compose**: Tüm bileþenlerin orkestrasyonu.

---

## Kurulum ve Çalýþtýrma

### Gereksinimler
- Docker
- .NET 8 SDK 

### Projeyi Çalýþtýrma

1. **Projeyi Klonlayýn**:
```bash
  git clone <repository-url>
  cd <repository-directory>
```
2. **Docker Compose Ýle Servisleri Baþlatýn**:
```bash
  docker-compose up --build
```
Bu komut aþaðýdaki bileþenleri baþlatacaktýr:

- Hotel Service (http://localhost:5000)
- Report Service (http://localhost:5002)
- PostgreSQL (localhost:5432)
- Elasticsearch (http://localhost:9200)
- Kibana (http://localhost:5601)
- RabbitMQ Yönetim Arayüzü (http://localhost:15672)

3. **Repo içerisindeki postman collectionýný indirip kullanabilirsiniz.**
