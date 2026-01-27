namespace Falcare.Cadastro.Core.Enums;

public enum StatusDocumento
{
    Pendente,
    Enviado,
    Aprovado,
    Reprovado
}

public enum TipoDocumento
{
    // Empresa
    CND_INSS,
    CRF_FGTS,
    
    // Funcionario
    FotoFuncionario,
    CarteiraTrabalho,
    RG,
    CPF,
    CNH,
    ASO,
    Vacina,
    ContratoTrabalho,
    NR10,
    NR11,
    NR13,
    NR35,
    CertificadoEletrica,
    TreinamentoSEP,
    Outros
}

public enum NaturezaAtividade
{
    Administrativo,
    FabricaOficina,
    Outros
}
