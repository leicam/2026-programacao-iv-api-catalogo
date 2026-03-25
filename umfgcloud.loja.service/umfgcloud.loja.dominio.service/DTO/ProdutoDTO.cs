using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace umfgcloud.loja.dominio.service.DTO;

public sealed class ProdutoDTO
{
    public abstract class AbstractProdutoDTO
    {
        [JsonPropertyName("descricao")]
        [Required(ErrorMessage = "O atributo descricao é obrigatório.")]
        public string Descricao { get; set; } = string.Empty;

        [JsonPropertyName("ean")]
        [Required(ErrorMessage = "O atributo ean é obrigatório.")]
        public string EAN { get; set; } = string.Empty;

        [JsonPropertyName("valorCompra")]
        [Required(ErrorMessage = "O atributo valorCompra é obrigatório.")]
        public decimal ValorCompra { get; set; } = decimal.Zero;

        [JsonPropertyName("valorVenda")]
        [Required(ErrorMessage = "O atributo valorVenda é obrigatório.")]
        public decimal ValorVenda { get; set; } = decimal.Zero;
    }

    public sealed class ProdutoRequest : AbstractProdutoDTO { }  

    public sealed class ProdutoWithIdRequest : AbstractProdutoDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
    }

    public sealed class ProdutoResponse : AbstractProdutoDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("createdByUserId")]
        public string CreatedByUserId { get; set; } = string.Empty;

        [JsonPropertyName("createdByEmail")]
        public string CreatedByEmail { get; set; } = string.Empty;

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("updatedByUserId")]
        public string UpdatedByUserId { get; set; } = string.Empty;

        [JsonPropertyName("updatedByEmail")]
        public string UpdatedByEmail { get; set; } = string.Empty;

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}