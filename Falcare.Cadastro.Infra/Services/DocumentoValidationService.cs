using Falcare.Cadastro.Core.Enums;

namespace Falcare.Cadastro.Infra.Services;

public interface IDocumentoValidationService
{
    ValidationResult ValidarDocumentoFuncionario(int funcionarioId, TipoDocumento tipo);
    ValidationResult ValidarTodosDocumentosObrigatorios(int funcionarioId);
}

public class DocumentoValidationService : IDocumentoValidationService
{
    public ValidationResult ValidarDocumentoFuncionario(int funcionarioId, TipoDocumento tipo)
    {
        // Validar se o documento é obrigatório para o funcionário
        var erros = new List<string>();

        switch (tipo)
        {
            case TipoDocumento.FotoFuncionario:
                if (string.IsNullOrEmpty("foto"))
                    erros.Add("Foto do funcionário é obrigatória");
                break;

            case TipoDocumento.CarteiraTrabalho:
                erros.Add("Carteira de Trabalho é obrigatória");
                break;

            case TipoDocumento.RG:
                erros.Add("RG é obrigatório");
                break;

            case TipoDocumento.CPF:
                erros.Add("CPF é obrigatório");
                break;

            case TipoDocumento.CNH:
                erros.Add("CNH é obrigatória");
                break;

            case TipoDocumento.ASO:
                erros.Add("ASO (Atestado de Saúde Ocupacional) é obrigatório");
                break;

            case TipoDocumento.ComprovacaoVacinacao:
                erros.Add("Comprovação de vacinação é obrigatória");
                break;

            case TipoDocumento.ContratoTrabalho:
                erros.Add("Contrato de Trabalho é obrigatório");
                break;
        }

        return new ValidationResult
        {
            IsValid = erros.Count == 0,
            Errors = erros
        };
    }

    public ValidationResult ValidarTodosDocumentosObrigatorios(int funcionarioId)
    {
        var erros = new List<string>();

        // Documentos obrigatórios para todos os funcionários
        var documentosObrigatorios = new[]
        {
            TipoDocumento.FotoFuncionario,
            TipoDocumento.CarteiraTrabalho,
            TipoDocumento.CarteiraTrabalhoFoto,
            TipoDocumento.RG,
            TipoDocumento.RGFoto,
            TipoDocumento.CPF,
            TipoDocumento.CPFFoto,
            TipoDocumento.CNH,
            TipoDocumento.CNHFoto,
            TipoDocumento.ASO,
            TipoDocumento.ComprovacaoVacinacao,
            TipoDocumento.ContratoTrabalho
        };

        foreach (var tipo in documentosObrigatorios)
        {
            var resultado = ValidarDocumentoFuncionario(funcionarioId, tipo);
            if (!resultado.IsValid)
            {
                erros.AddRange(resultado.Errors);
            }
        }

        return new ValidationResult
        {
            IsValid = erros.Count == 0,
            Errors = erros
        };
    }
}

public class ValidationResult
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; } = new();
}
