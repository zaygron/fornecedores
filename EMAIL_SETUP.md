# Configuração de E-mail - Gmail

## Status Atual

✅ **E-mail configurado e ativado para Gmail**

- **Servidor SMTP:** smtp.gmail.com
- **Porta:** 587
- **E-mail:** timoteo@falcare.com.br
- **Status:** Pronto para envio

---

## 🚀 Como Usar

### **Desenvolvimento Local**

A aplicação já está configurada com suas credenciais do Gmail. Basta rodar:

```powershell
cd C:\tmp\cadforn\fornecedores\Falcare.Cadastro.Web
dotnet run
```

Agora, quando você solicitar um cadastro de fornecedor, o e-mail será enviado automaticamente para o e-mail informado.

---

### **Teste de Envio**

1. Acesse: `http://localhost:5080/admin/solicitar-cadastro`
2. Preencha o formulário:
   - CNPJ: 12.345.678/0001-90
   - Nome da Empresa: Empresa Teste
   - E-mail: seu-email-teste@gmail.com
3. Clique em "Solicitar Cadastro"
4. Verifique o e-mail recebido

---

## ⚠️ Segurança em Produção

**IMPORTANTE:** Nunca faça commit de credenciais reais no repositório!

### **Opção 1: Variáveis de Ambiente (Recomendado)**

Crie um arquivo `.env` (não commitado):

```
SMTP_SERVER=smtp.gmail.com
SMTP_PORT=587
SMTP_USERNAME=timoteo@falcare.com.br
SMTP_PASSWORD=pgrq hjvm yjnf xpcc
SMTP_FROM_EMAIL=timoteo@falcare.com.br
SMTP_FROM_NAME=Falcare - Cadastro de Fornecedores
SMTP_ENABLE_SSL=true
EMAIL_ENABLED=true
```

Atualize `Program.cs`:

```csharp
var smtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER") 
    ?? builder.Configuration["Email:SmtpServer"];
var smtpUsername = Environment.GetEnvironmentVariable("SMTP_USERNAME") 
    ?? builder.Configuration["Email:SmtpUsername"];
var smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD") 
    ?? builder.Configuration["Email:SmtpPassword"];
```

### **Opção 2: Azure Key Vault**

Para aplicações em produção no Azure:

```csharp
var keyVaultUrl = new Uri($"https://{keyVaultName}.vault.azure.net/");
var credential = new DefaultAzureCredential();
builder.Configuration.AddAzureKeyVault(keyVaultUrl, credential);
```

### **Opção 3: GitHub Secrets**

Se usar CI/CD com GitHub Actions:

```yaml
env:
  SMTP_PASSWORD: ${{ secrets.SMTP_PASSWORD }}
  SMTP_USERNAME: ${{ secrets.SMTP_USERNAME }}
```

---

## 🔍 Troubleshooting

### **Erro: "Authentication failed"**

**Causa:** Credenciais incorretas ou senha de app não gerada corretamente

**Solução:**
1. Verifique se a senha de app foi gerada em: https://myaccount.google.com/apppasswords
2. Certifique-se de que a autenticação de dois fatores está ativada
3. Teste as credenciais em: https://www.gmailsmtptest.com/

### **Erro: "Connection timeout"**

**Causa:** Firewall ou porta bloqueada

**Solução:**
1. Verifique se a porta 587 está aberta
2. Tente usar a porta 465 (SSL) em vez de 587 (TLS)
3. Verifique se o firewall não está bloqueando conexões SMTP

### **E-mail não chega**

**Causa:** Pode estar na pasta de spam

**Solução:**
1. Verifique a pasta de spam/lixo
2. Marque como "Não é spam"
3. Verifique os logs da aplicação: `Falcare.Cadastro.Infra.Services.SmtpEmailService`

---

## 📊 Monitoramento

### **Logs de Envio**

Verifique os logs da aplicação para ver o status de envio:

```
info: Falcare.Cadastro.Infra.Services.SmtpEmailService[0]
      E-mail enviado com sucesso para contato@fornecedor.com.br com assunto 'Convite Falcare - Definição de Senha'
```

### **Gmail Activity**

Monitore a atividade da sua conta Gmail em:
https://myaccount.google.com/security-checkup

---

## 🔄 Próximas Etapas

1. ✅ Configuração do Gmail concluída
2. ⏭️ Testar envio de e-mail
3. ⏭️ Validar recebimento
4. ⏭️ Fazer backup das credenciais em local seguro
5. ⏭️ Configurar variáveis de ambiente para produção

---

## 📝 Checklist

- ✅ Gmail configurado
- ✅ Senha de app gerada
- ✅ `appsettings.json` atualizado
- ✅ `Email:Enabled` = true
- ⏳ Testar envio
- ⏳ Configurar produção

---

**Data de Configuração:** 27 de Janeiro de 2026  
**Status:** ✅ Pronto para Uso
