# Migração de SQLite para PostgreSQL

## Alterações Realizadas

Este documento descreve as alterações feitas para migrar o projeto **CadastroFornecedores** de SQLite para PostgreSQL.

### 1. Dependências Atualizadas

**Arquivo:** `Falcare.Cadastro.Web/Falcare.Cadastro.Web.csproj`

- ❌ Removido: `Microsoft.EntityFrameworkCore.Sqlite` (versão 9.0.0)
- ✅ Adicionado: `Npgsql.EntityFrameworkCore.PostgreSQL` (versão 9.0.0)

### 2. Configuração de Conexão

**Arquivo:** `Falcare.Cadastro.Web/appsettings.json`

**Antes (SQLite):**
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=app_v2.db"
}
```

**Depois (PostgreSQL):**
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=cadastro_fornecedores;Username=postgres;Password=postgres"
}
```

### 3. Configuração do DbContext

**Arquivo:** `Falcare.Cadastro.Web/Program.cs`

**Antes (SQLite):**
```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString)
           .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));
```

**Depois (PostgreSQL):**
```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString)
           .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));
```

### 4. Migrações

**Pasta:** `Falcare.Cadastro.Infra/Migrations/`

- Todas as migrações antigas do SQLite foram removidas
- Novas migrações serão geradas para PostgreSQL

## Pré-requisitos

### 1. Instalar PostgreSQL

**Windows:**
- Baixe em: https://www.postgresql.org/download/windows/
- Execute o instalador
- Anote a senha do usuário `postgres`

**macOS:**
```bash
brew install postgresql@15
brew services start postgresql@15
```

**Linux (Ubuntu/Debian):**
```bash
sudo apt-get update
sudo apt-get install postgresql postgresql-contrib
sudo systemctl start postgresql
```

### 2. Criar Banco de Dados

Abra o **pgAdmin** ou use o terminal:

```sql
CREATE DATABASE cadastro_fornecedores;
```

## Passos para Executar

### 1. Restaurar Dependências

```powershell
cd C:\tmp\cadforn\fornecedores
dotnet restore
```

### 2. Gerar Migrações para PostgreSQL

```powershell
cd Falcare.Cadastro.Web
dotnet ef migrations add InitialCreate -p ..\Falcare.Cadastro.Infra
```

### 3. Aplicar Migrações ao Banco de Dados

```powershell
dotnet ef database update -p ..\Falcare.Cadastro.Infra
```

### 4. Executar a Aplicação

```powershell
dotnet run
```

### 5. Acessar a Aplicação

Abra seu navegador e acesse:
```
http://localhost:5150
```

## Configuração de Produção

Para ambientes de produção, atualize a string de conexão em `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=seu-servidor-postgres;Port=5432;Database=cadastro_fornecedores;Username=seu-usuario;Password=sua-senha"
}
```

## Troubleshooting

### Erro: "Connection refused"
- Verifique se PostgreSQL está rodando
- Verifique se a porta 5432 está aberta
- Verifique as credenciais (usuário/senha)

### Erro: "Database does not exist"
- Crie o banco de dados usando pgAdmin ou SQL:
```sql
CREATE DATABASE cadastro_fornecedores;
```

### Erro: "Migrations folder is empty"
- Execute o comando de geração de migrações:
```powershell
dotnet ef migrations add InitialCreate -p ..\Falcare.Cadastro.Infra
```

## Diferenças Entre SQLite e PostgreSQL

| Aspecto | SQLite | PostgreSQL |
|---------|--------|-----------|
| **Tipo** | Arquivo local | Servidor de banco de dados |
| **Escalabilidade** | Limitada | Excelente |
| **Concorrência** | Limitada | Excelente |
| **Segurança** | Básica | Avançada |
| **Performance** | Boa para pequenos projetos | Excelente para produção |

## Próximas Etapas

1. Teste a aplicação localmente com PostgreSQL
2. Valide todas as funcionalidades
3. Faça backup do banco de dados
4. Deploy em produção

---

**Data da Migração:** 27 de Janeiro de 2026
**Versão do .NET:** 9.0
**Versão do PostgreSQL:** 15+
