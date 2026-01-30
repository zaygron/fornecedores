# Tecnologias e Linguagens - CadastroFornecedores

## 📋 Visão Geral

O projeto **CadastroFornecedores** é uma aplicação web moderna construída com tecnologias Microsoft .NET, utilizando a arquitetura de camadas (Layered Architecture) para separação de responsabilidades.

---

## 🏗️ Arquitetura do Projeto

```
CadastroFornecedores/
├── Falcare.Cadastro.Web          (Apresentação - UI)
├── Falcare.Cadastro.Core         (Domínio - Entidades)
├── Falcare.Cadastro.Infra        (Infraestrutura - Dados)
└── Falcare.Cadastro.sln          (Solução)
```

### **Camadas:**

| Camada | Responsabilidade | Tecnologias |
|--------|------------------|-------------|
| **Web (Apresentação)** | Interface do usuário | Blazor Server, MudBlazor |
| **Core (Domínio)** | Lógica de negócio | C#, Entidades |
| **Infra (Infraestrutura)** | Acesso a dados | Entity Framework Core, PostgreSQL |

---

## 🛠️ Linguagens de Programação

### **C# (Principal)**
- **Versão:** .NET 9.0
- **Uso:** Backend, lógica de negócio, acesso a dados
- **Arquivos:** ~60 arquivos `.cs`
- **Paradigma:** Orientado a Objetos (OOP)

### **Razor (Template Engine)**
- **Versão:** Razor Components (Blazor)
- **Uso:** Interface do usuário interativa
- **Arquivos:** ~8 componentes `.razor`
- **Características:** C# + HTML + CSS integrados

### **HTML/CSS**
- **Uso:** Estrutura e estilo dos componentes
- **Framework CSS:** MudBlazor (Material Design)

### **JSON**
- **Uso:** Configuração da aplicação
- **Arquivos:** `appsettings.json`, `launchSettings.json`

---

## 📦 Framework e Plataforma

### **.NET 9.0**
- **Tipo:** Framework de desenvolvimento multiplataforma
- **Linguagem:** C#
- **Versão:** 9.0.305
- **Licença:** Open Source (MIT)
- **Suporte:** Microsoft
- **Plataformas:** Windows, Linux, macOS

**Características do .NET 9:**
- ✅ Performance melhorada
- ✅ Suporte a C# 13
- ✅ Melhorias em Entity Framework Core
- ✅ Blazor Server otimizado

---

## 🎨 Frontend

### **Blazor Server**
- **Tipo:** Framework web interativo
- **Renderização:** Server-side
- **Comunicação:** WebSocket em tempo real
- **Componentes:** Reutilizáveis e reativas
- **Versão:** Incluído no .NET 9.0

**Vantagens:**
- C# no frontend (sem JavaScript)
- Interatividade em tempo real
- Acesso direto ao backend

### **MudBlazor 8.15.0**
- **Tipo:** Biblioteca de componentes UI
- **Design:** Material Design
- **Componentes:** DataGrid, Forms, Dialogs, etc.
- **Versão:** 8.15.0
- **Licença:** MIT

**Componentes Utilizados:**
- MudAppBar - Barra de navegação
- MudButton - Botões
- MudTextField - Campos de texto
- MudDataGrid - Tabelas de dados
- MudDialog - Diálogos modais
- MudCard - Cartões

---

## 🗄️ Banco de Dados

### **PostgreSQL 18**
- **Tipo:** Banco de dados relacional
- **Versão:** 18.x
- **Porta:** 5433 (instalação local)
- **Licença:** Open Source (PostgreSQL License)

**Características:**
- ✅ ACID completo
- ✅ Suporte a JSON
- ✅ Índices avançados
- ✅ Replicação e backup
- ✅ Escalabilidade horizontal

**Tabelas Principais:**
- `AspNetUsers` - Usuários do sistema
- `AspNetRoles` - Papéis/Permissões
- `AspNetUserRoles` - Relacionamento usuário-papel
- `Fornecedores` - Dados de fornecedores
- `Funcionarios` - Dados de funcionários
- `Documentos` - Armazenamento de documentos

---

## 📚 Bibliotecas e Pacotes NuGet

### **Entity Framework Core 9.0.0**
- **Tipo:** ORM (Object-Relational Mapping)
- **Uso:** Acesso e manipulação de dados
- **Componentes:**
  - `Microsoft.EntityFrameworkCore` - Core
  - `Microsoft.EntityFrameworkCore.Design` - Ferramentas de design
  - `Microsoft.EntityFrameworkCore.Tools` - Ferramentas CLI

### **Npgsql.EntityFrameworkCore.PostgreSQL 9.0.0**
- **Tipo:** Provider de banco de dados
- **Uso:** Integração do Entity Framework com PostgreSQL
- **Funcionalidades:**
  - Suporte a tipos PostgreSQL nativos
  - Otimizações específicas do PostgreSQL
  - Migrations automáticas

### **ASP.NET Identity 9.0.0**
- **Tipo:** Framework de autenticação e autorização
- **Componentes:**
  - `Microsoft.AspNetCore.Identity` - Core
  - `Microsoft.AspNetCore.Identity.EntityFrameworkCore` - Integração com EF Core
- **Recursos:**
  - Autenticação de usuários
  - Gerenciamento de roles (papéis)
  - Hash de senhas seguro (PBKDF2)
  - Confirmação de e-mail

### **Microsoft.Extensions.Identity.Stores 10.0.2**
- **Tipo:** Armazenamento de identidade
- **Uso:** Persistência de dados de autenticação

---

## 📧 Envio de E-mail

### **SMTP via Gmail**
- **Servidor:** smtp.gmail.com
- **Porta:** 587 (TLS)
- **Autenticação:** Senha de app do Google
- **Protocolo:** SMTP

**Implementação:**
- Classe: `SmtpEmailService.cs`
- Suporta templates de e-mail
- Tratamento de erros robusto
- Logging de envios

---

## 🔐 Segurança

### **Autenticação**
- ✅ ASP.NET Identity
- ✅ Hash de senhas (PBKDF2)
- ✅ Tokens de confirmação

### **Autorização**
- ✅ Role-based Access Control (RBAC)
- ✅ Roles: Admin, Fornecedor
- ✅ Proteção de rotas

### **Dados Sensíveis**
- ✅ Credenciais em `appsettings.json` (desenvolvimento)
- ✅ Recomendação: Variáveis de ambiente em produção
- ✅ Conexão SSL/TLS com PostgreSQL

---

## 🚀 Performance

### **Otimizações Implementadas**
- ✅ Índices no PostgreSQL
- ✅ Lazy loading de relacionamentos
- ✅ Caching de dados
- ✅ Queries otimizadas com LINQ

### **Comparação SQLite vs PostgreSQL**
| Métrica | SQLite | PostgreSQL |
|---------|--------|-----------|
| Concorrência | Limitada | Excelente |
| Queries Complexas | Lenta | Rápida (5-10x) |
| Índices | Básicos | Avançados |
| Escalabilidade | Até ~100 usuários | Ilimitada |

---

## 📊 Estatísticas do Projeto

| Métrica | Valor |
|---------|-------|
| **Arquivos de código** | ~69 arquivos |
| **Linguagens** | C#, Razor, HTML, CSS, JSON |
| **Projetos** | 3 (Web, Core, Infra) |
| **Versão .NET** | 9.0 |
| **Versão PostgreSQL** | 18.x |
| **Componentes Blazor** | ~8 componentes |
| **Entidades** | ~5 entidades principais |

---

## 🔄 Fluxo de Dados

```
┌─────────────────────────────────────────────────────────┐
│                    NAVEGADOR (Cliente)                  │
│                  (HTML + Blazor WebSocket)              │
└────────────────────────┬────────────────────────────────┘
                         │ WebSocket (Tempo Real)
                         ▼
┌─────────────────────────────────────────────────────────┐
│              Falcare.Cadastro.Web (Blazor)              │
│         ┌─────────────────────────────────────┐         │
│         │   Componentes Razor (.razor)        │         │
│         │   - SolicitarCadastro               │         │
│         │   - Dashboard                       │         │
│         │   - Login                           │         │
│         └──────────────┬──────────────────────┘         │
│                        │ Chamadas de Serviço
│         ┌──────────────▼──────────────────────┐         │
│         │   Serviços (Services)               │         │
│         │   - FornecedorService               │         │
│         │   - EmailService (SMTP)             │         │
│         │   - FileStorageService              │         │
│         └──────────────┬──────────────────────┘         │
└────────────────────────┬────────────────────────────────┘
                         │ Entity Framework Core
                         ▼
┌─────────────────────────────────────────────────────────┐
│        Falcare.Cadastro.Infra (Data Access)            │
│         ┌─────────────────────────────────────┐         │
│         │   ApplicationDbContext              │         │
│         │   - DbSet<Fornecedor>               │         │
│         │   - DbSet<Funcionario>              │         │
│         │   - DbSet<Documento>                │         │
│         └──────────────┬──────────────────────┘         │
│                        │ Npgsql Provider
│         ┌──────────────▼──────────────────────┐         │
│         │   Migrations                        │         │
│         │   - PostgreSQLInitial               │         │
│         └──────────────┬──────────────────────┘         │
└────────────────────────┬────────────────────────────────┘
                         │ SQL Queries
                         ▼
┌─────────────────────────────────────────────────────────┐
│              PostgreSQL 18 (Banco de Dados)             │
│         ┌─────────────────────────────────────┐         │
│         │   Tabelas                           │         │
│         │   - AspNetUsers                     │         │
│         │   - AspNetRoles                     │         │
│         │   - Fornecedores                    │         │
│         │   - Funcionarios                    │         │
│         │   - Documentos                      │         │
│         └─────────────────────────────────────┘         │
└─────────────────────────────────────────────────────────┘
```

---

## 📦 Dependências Externas

### **Envio de E-mail**
- ✅ Gmail SMTP (smtp.gmail.com:587)
- ✅ Suporte a TLS/SSL

### **Armazenamento de Arquivos**
- ✅ Sistema de arquivos local
- ✅ Pasta: `wwwroot/uploads/`

---

## 🔧 Ferramentas de Desenvolvimento

### **IDE Recomendada**
- Visual Studio 2022 (Community, Professional, Enterprise)
- Visual Studio Code + C# Extensions

### **Ferramentas CLI**
- `dotnet CLI` - Compilação e execução
- `dotnet ef` - Entity Framework Core Migrations
- `git` - Controle de versão

### **Gerenciamento de Banco de Dados**
- pgAdmin 4 - Interface gráfica para PostgreSQL
- psql - CLI do PostgreSQL

---

## 📝 Padrões de Projeto Utilizados

| Padrão | Uso |
|--------|-----|
| **Repository Pattern** | Acesso a dados |
| **Dependency Injection** | Injeção de dependências |
| **Service Layer** | Lógica de negócio |
| **Entity Framework** | ORM |
| **MVC/MVVM** | Arquitetura web |
| **Layered Architecture** | Separação de responsabilidades |

---

## 🎯 Versões Recomendadas

| Tecnologia | Versão | Status |
|------------|--------|--------|
| .NET | 9.0+ | ✅ Atual |
| C# | 13.0+ | ✅ Atual |
| PostgreSQL | 15+ | ✅ Compatível |
| Visual Studio | 2022+ | ✅ Recomendado |
| MudBlazor | 8.15.0+ | ✅ Atual |

---

## 📚 Recursos Adicionais

- [Documentação .NET 9](https://learn.microsoft.com/pt-br/dotnet/)
- [Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/)
- [Blazor Server](https://learn.microsoft.com/pt-br/aspnet/core/blazor/)
- [MudBlazor](https://www.mudblazor.com/)
- [PostgreSQL](https://www.postgresql.org/docs/)
- [ASP.NET Identity](https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/identity/)

---

**Data de Atualização:** 27 de Janeiro de 2026  
**Versão:** 1.0  
**Status:** ✅ Produção
