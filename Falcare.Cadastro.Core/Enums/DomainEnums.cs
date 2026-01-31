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
    
    // Funcionário - Documentos Pessoais
    FotoFuncionario,
    CarteiraTrabalho,
    CarteiraTrabalhoFoto,
    RG,
    RGFoto,
    CPF,
    CPFFoto,
    CNH,
    CNHFoto,
    
    // Funcionário - Documentos de Saúde e Segurança
    ASO,
    ComprovacaoVacinacao,
    ContratoTrabalho,
    
    // Funcionário - Certificados de Segurança (NRs)
    CertificadoEletrica,
    TreinamentoNR10,
    TreinamentoNR11,
    TreinamentoNR13,
    TreinamentoNR35,
    TreinamentoSEP,
    ComprovacaoExperienciaEletricidade,
    ComprovacaoExperienciaCaldeira,
    
    // Funcionário - Outros
    Outros
}

public enum NaturezaAtividade
{
    Administrativo,
    FabricaOficina,
    Outros
}
