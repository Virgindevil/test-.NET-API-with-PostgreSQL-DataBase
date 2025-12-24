# API Менеджер возврата покупок 

Пет-проект для стажировки в **Ozon Tech**.  
Реализует управление возвратами товаров от Ozon продавцам (селлерам): создание запросов на возврат, отслеживание статусов, интеграция со складской логистикой.

> **Стек**: C#, .NET, ASP.NET Core Web API, Entity Framework Core, PostgreSQL, xUnit

---

## Функционал

-  Создание запроса на возврат (`POST /api/returns`)
-  Валидация существования продавца и товара
-  Сохранение данных в PostgreSQL
-  Unit-тесты (xUnit + EF Core In-Memory)
-  Автоматическое применение миграций при запуске

**Бизнес-сценарий**:  
Продавец создаёт запрос на возврат бракованного или ошибочно доставленного товара. Система проверяет корректность данных и сохраняет заявку в статусе `CREATED`.

---

## Технические детали

### Модели
- `Seller` — продавец (`Id`, `Name`, `ContactEmail`)
- `Product` — товар (`Id`, `Sku`, `Name`, `Category`)
- `ReturnRequest` — запрос на возврат  
  (`SellerId`, `ProductId`, `Quantity`, `Reason`, `Status`, `CreatedAt`)

### Архитектура
- Чистое разделение: контроллеры → DbContext → модели
- Использование `record` для DTO
- Валидация на уровне контроллера
- Поддержка миграций EF Core

### Тестирование
- Unit-тесты для `ReturnsController`
- Изоляция от БД через `Microsoft.EntityFrameworkCore.InMemory`
- Покрытие сценариев: валидные данные, создание записи

---

## Как запустить

### Требования
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL 16](https://postgrespro.ru/products/postgrespro/standard)

### Шаги
1. Клонируйте репозиторий:
   ```bash
   git clone https://github.com/Virgindevil/test-.NET-API-with-PostgreSQL-DataBase.git
   cd test-.NET-API-with-PostgreSQL-DataBase
   ```

2. Настройте подключение к PostgreSQL:  
   Убедитесь, что в системе запущен PostgreSQL с:
   - **Пользователь**: `postgres`
   - **Пароль**: `*********` (секретная информация)

3. Запустите API:
   ```bash
   cd src/SellerReturnApi
   dotnet run
   ```

4. API будет доступен на:
   - **Swagger UI**: [`http://localhost:5079/swagger`](http://localhost:5079/swagger)
   - **Базовый адрес**: `http://localhost:5079`

### Пример запроса

```bash
curl -X POST http://localhost:5079/api/returns \
  -H "Content-Type: application/json" \
  -d '{
    "sellerId": "00000000-0000-0000-0000-000000000001",
    "productId": "00000000-0000-0000-0000-000000000002",
    "quantity": 3,
    "reason": "DEFECT"
  }'
```

>  Перед отправкой запроса необходимо добавить тестовые данные в БД (см. раздел ниже).

---

## Подготовка тестовых данных

Подключитесь к `returns_db` через DBeaver/psql и выполните:

```sql
-- Продавец
INSERT INTO "Sellers" ("Id", "Name", "ContactEmail")
VALUES ('00000000-0000-0000-0000-000000000001', 'Test Seller', 'test@example.com');

-- Товар
INSERT INTO "Products" ("Id", "Sku", "Name", "Category")
VALUES ('00000000-0000-0000-0000-000000000002', 'SKU-123', 'Test Product', 'Electronics');
```

---

## Запуск тестов

```bash
dotnet test
```

---

## Структура проекта

```
src/
└── SellerReturnApi/          # Основной Web API
tests/
└── SellerReturnApi.UnitTests/ # Unit-тесты (xUnit)
```

---

## Почему этот проект?

- Полностью соответствует стеку Ozon: **.NET Core, C#, PostgreSQL**
- Реализует **теоретический бизнес-кейс** из вакансии 
- Демонстрирует **production-ready подход**: миграции, валидация, тестирование
- Готов к расширению: можно добавить статусы `PICKED`/`SHIPPED`, админки склада, отчёты

---

> **Автор**: Матузов Сергей
> **Цель**: Стажировка в Ozon Tech — направление C# / .NET
