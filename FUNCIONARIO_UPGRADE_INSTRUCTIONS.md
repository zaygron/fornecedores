# Upgrade do Cadastro de Funcionários - Instruções de Implementação

## 📋 Resumo das Alterações

Este upgrade expande significativamente o cadastro de funcionários para incluir todos os campos necessários para gerenciar terceiros conforme requisitos de segurança do trabalho (NRs).

---

## 🎯 Novos Campos Adicionados

### **Informações de Cadastro**
- ✅ Propósito do cadastro (Integração / Outros)
- ✅ Natureza das atividades (Administrativo / Fábrica / Outros)
- ✅ Descrição de outras naturezas

### **Responsável Legal**
- ✅ Nome do responsável legal
- ✅ E-mail do responsável

### **Dados Pessoais**
- ✅ Nome do funcionário
- ✅ Cargo/Função
- ✅ Data de nascimento

### **Documentos de Identificação**
- ✅ Número e série da Carteira de Trabalho (CTPS)
- ✅ RG com data de vencimento
- ✅ CPF com data de vencimento
- ✅ CNH com data de vencimento

### **Documentos de Saúde e Segurança**
- ✅ Data de vencimento do ASO (Atestado de Saúde Ocupacional)
- ✅ Comprovação de vacinação

### **Conformidade e Segurança**
- ✅ Trabalha com eletricidade? (S/N)
- ✅ Serviços com equipamentos de movimentação de cargas? (S/N) - NR11
- ✅ Serviços envolvendo caldeiras ou vasos de pressão? (S/N) - NR13
- ✅ Serviços com trabalho em altura? (S/N) - NR35

### **Metadados**
- ✅ Data de cadastro
- ✅ Data de atualização
- ✅ Status ativo/inativo

---

## 📁 Arquivos Modificados/Criados

### **Entidades (Core)**
| Arquivo | Mudança |
|---------|---------|
| `Funcionario.cs` | ✅ Expandida com novos campos |
| `Documento.cs` | ✅ Expandida com mais informações |
| `DomainEnums.cs` | ✅ Novos tipos de documentos |

### **Serviços (Infra)**
| Arquivo | Mudança |
|---------|---------|
| `FuncionarioService.cs` | ✅ Atualizado com novos métodos |
| `DocumentoValidationService.cs` | ✅ Novo - Validação de documentos |
| `DocumentoUploadService.cs` | ✅ Novo - Upload de arquivos |

---

## 🔧 Passos para Implementação

### **Passo 1: Atualizar o Código Local**

```powershell
cd C:\tmp\cadforn\fornecedores

# Atualizar do GitHub
git pull origin main

# Restaurar dependências
dotnet restore

# Reconstruir
dotnet build
```

### **Passo 2: Gerar e Aplicar Migrations**

```powershell
cd Falcare.Cadastro.Web

# Gerar nova migration
dotnet ef migrations add ExpandFuncionarioFields -p ..\Falcare.Cadastro.Infra

# Aplicar ao banco de dados
dotnet ef database update -p ..\Falcare.Cadastro.Infra
```

**Esperado:** Você verá:
```
Applying migration '20260127XXXXXX_ExpandFuncionarioFields'.
Done.
```

### **Passo 3: Registrar Novos Serviços**

Adicione ao `Program.cs`:

```csharp
// Adicionar após os outros serviços
builder.Services.AddScoped<IDocumentoValidationService, DocumentoValidationService>();
builder.Services.AddScoped<IDocumentoUploadService, DocumentoUploadService>();
```

### **Passo 4: Testar a Aplicação**

```powershell
dotnet run
```

Acesse: `http://localhost:5080`

---

## 📊 Novos Tipos de Documentos

| Tipo | Descrição |
|------|-----------|
| `FotoFuncionario` | Foto do funcionário (JPG) |
| `CarteiraTrabalho` | Carteira de Trabalho |
| `CarteiraTrabalhoFoto` | Foto da Carteira de Trabalho |
| `RG` | Registro Geral |
| `RGFoto` | Foto do RG |
| `CPF` | Cadastro de Pessoa Física |
| `CPFFoto` | Foto do CPF |
| `CNH` | Carteira Nacional de Habilitação |
| `CNHFoto` | Foto da CNH |
| `ASO` | Atestado de Saúde Ocupacional |
| `ComprovacaoVacinacao` | Comprovação de Vacinação |
| `ContratoTrabalho` | Contrato de Trabalho |
| `CertificadoEletrica` | Certificado em Eletricidade |
| `TreinamentoNR10` | Treinamento NR10 (Eletricidade) |
| `TreinamentoNR11` | Treinamento NR11 (Movimentação de Cargas) |
| `TreinamentoNR13` | Treinamento NR13 (Caldeiras) |
| `TreinamentoNR35` | Treinamento NR35 (Trabalho em Altura) |
| `TreinamentoSEP` | Treinamento SEP (Alta Tensão) |
| `ComprovacaoExperienciaEletricidade` | Comprovação de Experiência em Eletricidade |
| `ComprovacaoExperienciaCaldeira` | Comprovação de Experiência em Caldeira |

---

## 📤 Upload de Documentos

### **Configurações**
- **Diretório:** `wwwroot/uploads/documentos/`
- **Tamanho máximo:** 10 MB
- **Extensões permitidas:** `.jpg`, `.jpeg`, `.png`, `.pdf`
- **MIME types:** `image/jpeg`, `image/png`, `application/pdf`

### **Uso do Serviço**

```csharp
// Injetar o serviço
private readonly IDocumentoUploadService _uploadService;

// Fazer upload
var (sucesso, path, erro) = await _uploadService.UploadDocumentoAsync(
    arquivo: formFile,
    funcionarioId: 123,
    tipo: TipoDocumento.FotoFuncionario
);

if (sucesso)
{
    // Salvar path no banco de dados
}
else
{
    // Mostrar erro ao usuário
}
```

---

## ✅ Validação de Documentos

### **Documentos Obrigatórios**

Todos os funcionários devem ter:
- Foto do funcionário
- Carteira de Trabalho (CTPS)
- RG
- CPF
- CNH
- ASO
- Comprovação de Vacinação
- Contrato de Trabalho

### **Documentos Condicionais**

Conforme as respostas às perguntas de segurança:

| Pergunta | Se SIM, Exigir |
|----------|----------------|
| Trabalha com eletricidade? | TreinamentoNR10, CertificadoEletrica, ComprovacaoExperienciaEletricidade |
| Movimentação de cargas? | TreinamentoNR11 |
| Caldeiras/Vasos de pressão? | TreinamentoNR13, ComprovacaoExperienciaCaldeira |
| Trabalho em altura? | CertificadoEletrica, TreinamentoNR10, TreinamentoSEP |

---

## 🗄️ Estrutura do Banco de Dados

### **Tabela: Funcionarios**

```sql
CREATE TABLE "Funcionarios" (
    "Id" INTEGER PRIMARY KEY,
    "FornecedorId" INTEGER NOT NULL,
    "PropositoCadastro" TEXT NOT NULL,
    "NaturezaAtividade" INTEGER NOT NULL,
    "OutraNaturezaDescricao" TEXT,
    
    -- Responsável
    "NomeResponsavelLegal" TEXT NOT NULL,
    "EmailResponsavel" TEXT NOT NULL,
    
    -- Dados Pessoais
    "Nome" TEXT NOT NULL,
    "Cargo" TEXT,
    "DataNascimento" TIMESTAMP,
    
    -- Documentos
    "CTPS_NumeroSerie" TEXT,
    "RG" TEXT,
    "RG_DataVencimento" TIMESTAMP,
    "CPF" TEXT,
    "CPF_DataVencimento" TIMESTAMP,
    "CNH" TEXT,
    "CNH_DataVencimento" TIMESTAMP,
    "ASO_DataVencimento" TIMESTAMP,
    
    -- Segurança
    "TrabalhaComEletricidade" BOOLEAN,
    "MovimentacaoCarga" BOOLEAN,
    "CaldeirasVasosPressao" BOOLEAN,
    "TrabalhoAltura" BOOLEAN,
    
    -- Metadados
    "DataCadastro" TIMESTAMP NOT NULL,
    "DataAtualizacao" TIMESTAMP,
    "Ativo" BOOLEAN NOT NULL,
    
    FOREIGN KEY ("FornecedorId") REFERENCES "Fornecedores"("Id")
);
```

### **Tabela: Documentos**

```sql
CREATE TABLE "Documentos" (
    "Id" INTEGER PRIMARY KEY,
    "OwnerType" TEXT NOT NULL,
    "OwnerId" INTEGER NOT NULL,
    "Tipo" INTEGER NOT NULL,
    
    "DataEmissao" TIMESTAMP,
    "DataValidade" TIMESTAMP,
    
    "ArquivoNomeOriginal" TEXT NOT NULL,
    "ArquivoMimeType" TEXT NOT NULL,
    "ArquivoPath" TEXT NOT NULL,
    "ArquivoTamanho" BIGINT,
    
    "Status" INTEGER NOT NULL,
    "ObservacaoAprovacao" TEXT,
    "DataAprovacao" TIMESTAMP,
    "AprovadoPorUserId" TEXT,
    
    "DataUpload" TIMESTAMP NOT NULL,
    "UploadedByUserId" TEXT,
    "DataAtualizacao" TIMESTAMP,
    "Descricao" TEXT
);
```

---

## 🔍 Checklist de Implementação

- ✅ Código atualizado do GitHub
- ✅ Dependências restauradas
- ✅ Projeto compilado
- ✅ Migration gerada
- ✅ Migration aplicada ao banco
- ✅ Novos serviços registrados em Program.cs
- ✅ Aplicação testada
- ✅ Formulário de cadastro atualizado (próximo passo)

---

## 📝 Próximas Etapas

1. **Criar/Atualizar Formulário Razor** para capturar todos os novos campos
2. **Implementar Upload de Arquivos** no formulário
3. **Adicionar Validações** de campos obrigatórios
4. **Criar Página de Visualização** de funcionários e documentos
5. **Implementar Aprovação** de documentos

---

## ⚠️ Importante

- Faça backup do banco de dados antes de aplicar as migrations
- Teste em ambiente de desenvolvimento primeiro
- Valide todos os uploads de arquivo
- Implemente validação de CPF e CNH quando necessário

---

**Data:** 27 de Janeiro de 2026  
**Versão:** 1.0  
**Status:** ✅ Pronto para Implementação
