# NeredeKal Hotel&Report Service
---

## Proje A��klamas�

Bu proje, bir otel rehberi uygulamas� i�in iki mikroservis i�ermektedir:
1. **Hotel Service**: Otellerin y�netimi ve ileti�im bilgilerinin y�netilmesini sa�lar.
2. **Report Service**: Otel verilerine dayal� raporlar olu�turur ve RabbitMQ kullanarak asenkron mesajla�ma sa�lar.

Proje �u teknolojilere dayanmaktad�r:
- **ASP.NET Core**: Web API geli�tirme.
- **PostgreSQL**: Veritaban�.
- **Elasticsearch**: Loglama ve arama.
- **Kibana**: Elasticsearch �zerinde g�rselle�tirme.
- **RabbitMQ**: Mesaj kuyru�u.
- **Docker Compose**: T�m bile�enlerin orkestrasyonu.

---

## Kurulum ve �al��t�rma

### Gereksinimler
- Docker
- .NET 8 SDK 

### Projeyi �al��t�rma

1. **Projeyi Klonlay�n**:
```bash
  git clone <repository-url>
  cd <repository-directory>
```
2. **Docker Compose �le Servisleri Ba�lat�n**:
```bash
  docker-compose up --build
```
Bu komut a�a��daki bile�enleri ba�latacakt�r:

- Hotel Service (http://localhost:5000)
- Report Service (http://localhost:5002)
- PostgreSQL (localhost:5432)
- Elasticsearch (http://localhost:9200)
- Kibana (http://localhost:5601)
- RabbitMQ Y�netim Aray�z� (http://localhost:15672)

3. **Repo i�erisindeki postman collection�n� indirip kullanabilirsiniz.**
