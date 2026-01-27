namespace Falcare.Cadastro.Core.Enums;

public enum StatusFornecedor
{
    Pendente = 0, // Cadastro inicial / Incompleto
    EmAnalise = 1, // Enviado para aprovação
    Aprovado = 2, // Aprovado pelo Admin
    Reprovado = 3, // Rejeitado (precisa de correção)
    Bloqueado = 4 // Acesso suspenso
}
