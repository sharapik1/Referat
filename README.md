# WEB API

---

Разработала: Екатерина Шарапова

## Методы

---

### Product

---

### all

---

Получить все существующие товары.

Адрес
```text
GET /product/all
```

Пример ответа:
```json
[
  {
    "id": 1,
    "title": "example",
    "description": "example",
    "price": 10
  },
  {
    "id": 2,
    "title": "example",
    "description": "example",
    "price": 100
  }
]
```

### Find By ID

Найти товар по ID

---

Адрес
```text
GET /product?id={id}
```

Пример ответа:
```json
{
  "id": 1,
  "title": "example",
  "description": "example",
  "price": 10
}
```

Ошибки
```text
404 - Товар не найден
```


### Save

Сохранить или создать товар

---

Адрес
```text
GET /product/save
```


Пример тела запроса
```text
{
  "title": "example",
  "description": "example",
  "price": 10
}
```

Пример ответа:
```json
{
  "id": 1,
  "title": "example",
  "description": "example",
  "price": 10
}
```

### User

---

### Login

Авторизоваться

---

Адрес
```text
GET /user/login?nickname={nickname}&password={password}
```

Пример ответа:
```json
{
  "id": 1,
  "nickname": "example",
  "name": "example",
  "password": "example"
}
```

Ошибки
```text
404 - Пользователь не найден
401 - Неверный пароль
```


### Reg

Зарегистрироваться

---

Адрес
```text
GET /user/reg?nickname={nickname}&password={password}&name={name}
```

Пример ответа:
```json
{
  "id": 1,
  "nickname": "example",
  "name": "example",
  "password": "example"
}
```

Ошибки
```text
600 - Пользователь с таким ником уже существует 
```