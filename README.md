# UnivAcademico

Microservicio de operaciones académicas, construido con **ASP.NET Core**. Desarrollado bajo principios de arquitectura limpia, con soporte para contenedores Docker.

## 🚀 Características

- API RESTful construida con ASP.NET Core.
- Consume una BD POSTGRES.
- Arquitectura por capas (Domain, Application, Infrastructure, API).
- Preparado para despliegue en Docker.

---

## 🛠️ Tecnologías utilizadas

- .NET 8
- Npgsql (Conexión a POSTGRES)
- Docker & Docker Compose
- Clean Architecture (Separación de responsabilidades)

---

## 📦 Docker Compose

```yaml
services:
  api:
    build:
      context: .
      dockerfile: UnivAcademico.Api/Dockerfile
    ports:
      - "5002:8080"
    environment:
      - ConnectionStrings__dbPersona=Server=${POSTGRES_IP_SERVER};Port=3306;Database=${POSTGRES_BD_NAME};User=postgres;Password=${POSTGRES_ROOT_PASSWORD};
```

---

## 📬 Endpoint disponible

### POST `/api/v1/estudiante/matricula/info`

Obtiene información acerca del estudiante.

#### 🔹 Request JSON
```json
{
    "id": 1
}
```

#### 🔹 Response JSON
```json
{
    "id": 1,
    "sede": "LIMA",
    "facultad": "CIENCIAS ECONOMICAS",
    "programa": "CONTABILIDAD Y FINANZAS",
    "plan": 8,
    "curricula_id": 18,
    "categoria_id": 3,
    "semestre_id": 170,
    "semestre": "20251",
    "matricula_id": 3170
}
```

### GET `/api/v1/estudiante/matricula/oferta-academica/{estudianteId}`

Obtiene la lista de cursos en los que el estudiante se puede matricular.

#### 🔹 Response JSON
```json
[
    {
        "id": 70513,
        "ciclo": 5,
        "curso_id": 231,
        "nombre": "ADMINISTRACION FINANCIERA",
        "creditos": 4,
        "tipo": "O",
        "seccion": "500",
        "modalidad": "P",
        "horario": "JUE 07:00-08:20 - JUE 08:30-10:00"
    },
    {
        "id": 70514,
        "ciclo": 5,
        "curso_id": 231,
        "nombre": "ADMINISTRACION FINANCIERA",
        "creditos": 4,
        "tipo": "O",
        "seccion": "505",
        "modalidad": "P",
        "horario": "LUN 19:10-20:40 - LUN 20:50-22:20"
    }
]
```

### POST `/api/v1/estudiante/matricula/registrar`

Obtiene información acerca del estudiante.

#### 🔹 Request JSON
```json
{
  "estudiante_id": 456,
  "horarios": [
    {
      "curso": 101,
      "horario": 1
    },
    {
      "curso": 102,
      "horario": 3
    }
  ]
}
```

#### 🔹 Response JSON
```json
{
  "id": 1,
  "ciclo": 1,
  "curso_id": 1005,
  "nombre": "Programación Orientada a Objetos",
  "creditos": 4,
  "tipo": "O",
  "seccion": "A1",
  "modalidad": "P",
  "horario": "JUE 07:00-08:20 - JUE 08:30-10:00",
  "matricula_id": 789
}
```

### GET `/api/v1/estudiante/matricula/cursos-matriculados/{matriculaId}`

Obtiene la lista de cursos en los que el estudiante se puede matricular.

#### 🔹 Response JSON
```json
[
    {
        "id": 74352,
        "ciclo": 2,
        "curso_id": 1619,
        "nombre": "HISTORIA Y GEOGRAFIA UNIVERSAL II",
        "creditos": 3,
        "tipo": "O",
        "seccion": "350",
        "modalidad": "P",
        "horario": "MIE 15:00-16:30 - VIE 15:00-16:30",
        "matricula_id": 1
    },
    {
        "id": 73210,
        "ciclo": 3,
        "curso_id": 3226,
        "nombre": "CASTELLANO III",
        "creditos": 4,
        "tipo": "O",
        "seccion": "350",
        "modalidad": "P",
        "horario": "MAR 09:45-11:15 - JUE 09:45-11:15 - VIE 11:30-13:00",
        "matricula_id": 1
    }
]
```

---

## ▶️ Cómo ejecutar

1. Clonar este repositorio:
   ```bash
   git clone https://github.com/Charlie-Nash/UnivAcademico
   cd UnivAcademico
   ```

2. Crear un archivo `.env` con las siguientes variables:
   ```env
   POSTGRES_IP_SERVER=ip_del_servidor_de_BD
   POSTGRES_BD_NAME=nombre_de_la_BD
   POSTGRES_ROOT_PASSWORD=tu_password
   ```

3. Ejecutar con Docker Compose:
   ```bash
   sudo docker-compose up --build -d
   o
   sudo docker compose up --build -d
   ```
---

## 👤 Autor

Desarrollado por **Charlie Nash**

---

## 📄 Licencia

Este proyecto se puede usar libremente para fines educativos o personales.