using Newtonsoft.Json;

namespace SupportHub.Authentication.Domain.DTOs.Responses.APIs;

public class ResponseCnpj
{
	public string Cnpj { get; set; } = string.Empty;

	[JsonProperty("descricao_matriz_filial")]
	public string DescricaoMatriz { get; set; } = string.Empty;

	[JsonProperty("razao_social")] public string RazaoSocial { get; set; } = string.Empty;
	[JsonProperty("nome_fantasia")] public string NomeFantasia { get; set; } = string.Empty;
	[JsonProperty("cnae_fiscal")] public int CnaeFiscal { get; set; }

	[JsonProperty("cnae_fiscal_descricao")]
	public string CnaeDescricao { get; set; } = string.Empty;

	[JsonProperty("descricao_tipo_logradouro")]
	public string DescricaoLogradouro { get; set; } = string.Empty;

	[JsonProperty("logradouro")] public string Logradouro { get; set; } = string.Empty;
	[JsonProperty("numero")] public string Numero { get; set; } = string.Empty;
	[JsonProperty("complemento")] public string Complemento { get; set; } = string.Empty;
	[JsonProperty("bairro")] public string Bairro { get; set; } = string.Empty;
	[JsonProperty("cep")] public int Cep { get; set; }
	[JsonProperty("uf")] public string Uf { get; set; } = string.Empty;
	[JsonProperty("municipio")] public string Municipio { get; set; } = string.Empty;
}