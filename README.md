# 🦜 Quetzal

<p align="center">
  <strong>Sistema de gerenciamento de projetos de Design de Interiores</strong>
</p>

<p align="center">
  Uma solução completa desenvolvida com <strong>.NET 10</strong>, arquitetura em camadas, API REST, aplicação Web MVC e aplicação Desktop.
</p>

<p align="center">

![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge\&logo=dotnet\&logoColor=white)
![C#](https://img.shields.io/badge/C%23-13-239120?style=for-the-badge\&logo=csharp\&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-10-512BD4?style=for-the-badge\&logo=dotnet\&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?style=for-the-badge\&logo=microsoftsqlserver\&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-512BD4?style=for-the-badge\&logo=dotnet\&logoColor=white)
![Status](https://img.shields.io/badge/Status-Em%20Desenvolvimento-orange?style=for-the-badge)

</p>

---

## 📖 Sobre

O **Quetzal** é uma plataforma desenvolvida para gerenciamento e apresentação de projetos de **Design de Interiores**.

A aplicação foi construída utilizando uma arquitetura organizada em camadas e possui múltiplas interfaces que se comunicam através de uma **API REST centralizada**.

O sistema permite administrar:

* 👤 Usuários
* 🏠 Ambientes
* 🎨 Projetos de interiores
* 🖼️ Fotografias
* 📁 Portfólios
* 🔐 Autenticação e autorização

A solução possui três aplicações principais:

```text
             ┌──────────────────┐
             │   Quetzal Web    │
             │    ASP.NET MVC   │
             └────────┬─────────┘
                      │
                      │
┌─────────────────────▼─────────────────────┐
│                 Quetzal API               │
│             ASP.NET Core REST             │
└─────────────────────┬─────────────────────┘
                      │
                      │
             ┌────────▼────────┐
             │    SQL Server   │
             └─────────────────┘
                      ▲
                      │
             ┌────────┴────────┐
             │ Quetzal Desktop │
             │   Windows Forms │
             └─────────────────┘
```

---

# ✨ Funcionalidades

### 🔐 Autenticação

* Cadastro de usuários
* Login
* Controle de sessão
* Autenticação baseada em ASP.NET Core Identity
* Controle de acesso

### 👤 Usuários

* Gerenciamento de usuários
* Associação de usuários aos projetos
* Controle de informações cadastrais

### 🏠 Ambientes

Gerenciamento dos ambientes utilizados nos projetos:

* Sala
* Cozinha
* Quarto
* Banheiro
* Escritório
* Lavanderia

### 🎨 Projetos

* Criação de projetos
* Associação do projeto ao usuário
* Descrição do projeto
* Controle de status
* Upload/gerenciamento de imagens
* Múltiplas fotos por projeto

### 🖼️ Portfólio

* Criação de projetos para apresentação
* Associação com ambientes
* Associação opcional com projetos
* Seleção de fotos
* Múltiplas fotos por portfólio
* Controle de publicação

---

# 🏗️ Arquitetura

O Quetzal utiliza uma arquitetura baseada em **separação de responsabilidades**, dividindo o sistema em diferentes projetos.

```text
Quetzal
│
├── 🧠 Quetzal
│   └── Domain
│
├── ⚙️ Quetzal.Application
│   └── Application
│
├── 🗄️ Quetzal.Infrastructure
│   └── Infrastructure
│
├── 🔌 Quetzal.API
│   └── REST API
│
├── 🌐 Quetzal.UI
│   └── Web MVC
│
└── 🖥️ Quetzal.Desktop
    └── Windows Forms
```

### Fluxo da aplicação

```text
┌──────────────┐
│     UI       │
│ Web/Desktop  │
└──────┬───────┘
       │
       ▼
┌──────────────┐
│     API      │
│ Controllers  │
└──────┬───────┘
       │
       ▼
┌──────────────┐
│ Application  │
│   Services   │
└──────┬───────┘
       │
       ▼
┌──────────────┐
│    Domain    │
│   Entities   │
└──────┬───────┘
       ▲
       │
┌──────┴───────┐
│Infrastructure│
│ Repositories │
└──────┬───────┘
       │
       ▼
┌──────────────┐
│ SQL Server   │
└──────────────┘
```

---

# 🧩 Domínio

As principais entidades do sistema são:

```text
ApplicationUser
      │
      │ 1:N
      ▼
   ProjetoC
      │
      │ 1:N
      ▼
ProjetoCFoto


Ambiente
   │
   │ 1:N
   ▼
Portfolio
   │
   │ 1:N
   ▼
PortfolioFoto
```

O `Portfolio` também pode possuir uma associação opcional com `ProjetoC`.

> **Nota:** `ProjetoC` e `Ambiente` não possuem relacionamento direto.

---

# 🛠️ Tecnologias

## Backend

| Tecnologia                | Utilização            |
| ------------------------- | --------------------- |
| **C#**                    | Linguagem principal   |
| **.NET 10**               | Plataforma            |
| **ASP.NET Core**          | Backend               |
| **ASP.NET Core Web API**  | API REST              |
| **ASP.NET Core MVC**      | Aplicação Web         |
| **Entity Framework Core** | ORM                   |
| **SQL Server**            | Banco de dados        |
| **ASP.NET Core Identity** | Autenticação          |
| **AutoMapper**            | Mapeamento de objetos |
| **FluentValidation**      | Validação             |
| **Swagger / OpenAPI**     | Documentação da API   |

## Desktop

| Tecnologia            | Utilização          |
| --------------------- | ------------------- |
| **Windows Forms**     | Interface Desktop   |
| **HttpClient**        | Comunicação com API |
| **Newtonsoft.Json**   | Serialização JSON   |
| **Guna.UI2.WinForms** | Componentes visuais |

---

# 🗄️ Banco de Dados

O projeto utiliza:

**Microsoft SQL Server + Entity Framework Core Code First**

O contexto principal da aplicação é:

```csharp
QuetzalContexto
```

que utiliza:

```csharp
IdentityDbContext<ApplicationUser>
```

Principais entidades persistidas:

```text
ApplicationUser
Ambiente
ProjetoC
ProjetoCFoto
Portfolio
PortfolioFoto
```

O banco também utiliza as tabelas necessárias para o ASP.NET Core Identity.

---

# 🔄 Entity Framework Core

O banco é versionado através de **Migrations**.

Exemplo:

```bash
dotnet ef migrations add NomeDaMigration \
  --project Quetzal.Infrastructure \
  --startup-project Quetzal.API
```

Aplicar migrations:

```bash
dotnet ef database update \
  --project Quetzal.Infrastructure \
  --startup-project Quetzal.API
```

As migrations permitem manter a estrutura do banco sincronizada com o modelo de domínio.

---

# 🌱 Seed

O projeto possui inicialização automática de dados através do:

```text
Quetzal.Infrastructure
└── Dados
    └── SeedDados.cs
```

O Seed é responsável por inicializar dados necessários para o funcionamento do sistema, incluindo ambientes padrão e dados relacionados à aplicação.

---

# 📂 Estrutura do projeto

```text
Quetzal/
│
├── Quetzal/
│   ├── Entidades/
│   └── Interfaces/
│
├── Quetzal.Application/
│   ├── DTOs/
│   ├── Mapeamentos/
│   ├── Servicos/
│   └── DependencyInjection.cs
│
├── Quetzal.Infrastructure/
│   ├── Dados/
│   ├── Migrations/
│   └── Repositorios/
│
├── Quetzal.API/
│   ├── Controllers/
│   ├── Program.cs
│   └── appsettings.json
│
├── Quetzal.UI/
│   ├── Areas/
│   ├── Controllers/
│   ├── Views/
│   ├── Servicos/
│   └── Program.cs
│
├── Quetzal.Desktop/
│   ├── ApiClientes/
│   ├── Formularios/
│   ├── UserControls/
│   ├── Sessao/
│   └── Program.cs
│
└── Quetzal.slnx
```

---

# 🔌 API

A API disponibiliza endpoints para os principais recursos do sistema.

```text
/api/Auth
/api/Usuario
/api/Ambiente
/api/ProjetoC
/api/Portfolio
```

A documentação dos endpoints pode ser acessada através do **Swagger/OpenAPI**.

Após iniciar a API:

```text
/swagger
```

---

# 🚀 Como executar

## Pré-requisitos

Antes de executar o projeto, certifique-se de possuir:

* [.NET 10 SDK](https://dotnet.microsoft.com/)
* SQL Server
* Visual Studio
* ASP.NET and web development workload
* Desktop development with .NET workload
* Entity Framework Core Tools

Verifique o SDK:

```bash
dotnet --version
```

---

## 1. Clonar o projeto

```bash
git clone <URL_DO_REPOSITORIO>

cd Quetzal
```

---

## 2. Restaurar dependências

```bash
dotnet restore
```

---

## 3. Configurar o banco

Configure a connection string no:

```text
Quetzal.API/appsettings.json
```

Exemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=Quetzal;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> ⚠️ Não utilize credenciais reais diretamente no repositório. Para ambientes de desenvolvimento, considere utilizar User Secrets ou variáveis de ambiente.

---

## 4. Aplicar as migrations

```bash
dotnet ef database update \
  --project Quetzal.Infrastructure \
  --startup-project Quetzal.API
```

---

## 5. Compilar

```bash
dotnet build
```

---

## 6. Executar a API

```bash
dotnet run --project Quetzal.API
```

---

## 7. Executar a aplicação Web

Em outro terminal:

```bash
dotnet run --project Quetzal.UI
```

---

## 8. Executar o Desktop

No Windows:

```bash
dotnet run --project Quetzal.Desktop
```

---

# 🧪 Desenvolvimento

Durante o desenvolvimento, recomenda-se utilizar:

```bash
dotnet build
```

para verificar a compilação da solução e:

```bash
dotnet ef migrations add NomeDaMigration
```

para alterações estruturais no banco.

Antes de criar uma migration, revise cuidadosamente as alterações realizadas nas entidades e no `DbContext`.

---

# 🔒 Segurança

O projeto utiliza **ASP.NET Core Identity** para gerenciamento de usuários.

Boas práticas recomendadas:

* Não versionar senhas;
* Não versionar tokens;
* Não versionar connection strings com credenciais;
* Utilizar User Secrets em desenvolvimento;
* Utilizar variáveis de ambiente em produção;
* Manter dependências atualizadas;
* Proteger endpoints administrativos através de autorização.

---

# 📌 Status

🚧 **Em desenvolvimento**

Novas funcionalidades, melhorias de interface, validações e ajustes arquiteturais podem ser adicionados ao projeto durante sua evolução.

---

# 🗺️ Roadmap

Algumas possíveis evoluções do projeto:

* [ ] Testes unitários
* [ ] Testes de integração
* [ ] Melhorias no sistema de autorização
* [ ] Upload de imagens para armazenamento externo
* [ ] Paginação dos resultados
* [ ] Filtros avançados
* [ ] Dashboard administrativo
* [ ] Melhorias de UX/UI
* [ ] Logs estruturados
* [ ] Dockerização
* [ ] CI/CD
* [ ] Deploy em ambiente cloud
      
---

# 🎯 Objetivo técnico

Além de atender às necessidades do sistema de Design de Interiores, o Quetzal foi desenvolvido como uma aplicação prática para aplicação de conceitos de:

* Arquitetura de software;
* Programação orientada a objetos;
* Desenvolvimento Web;
* Desenvolvimento Desktop;
* APIs REST;
* Entity Framework Core;
* SQL Server;
* Design Patterns;
* Dependency Injection;
* Autenticação e autorização;
* Versionamento de banco de dados;
* Separação de responsabilidades.

---

# 👨‍💻 Autores

**Davi Alves Nogueira, Fausto Puim Feriani, Kelly Siqueira, Samuel de Santana e Sthefanny Carvalho**

Desenvolvedores em formação com foco em **Desenvolvimento de Software, C#, .NET, APIs, Banco de Dados e Arquitetura de Sistemas**.

---

# 📄 Licença

Este projeto está distribuído sob os termos definidos no arquivo:

```text
LICENSE
```

Consulte o arquivo para conhecer as permissões e condições de utilização do projeto.

---

  <p align="center"> Desenvolvido com 💜 e ☕ utilizando C# e .NET </p>
 
