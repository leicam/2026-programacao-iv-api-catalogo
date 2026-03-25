using umfgcloud.loja.dominio.service.DTO;

namespace umfgcloud.loja.dominio.service.Interfaces.Servicos;

public interface IProdutoServico
{
    Task<IEnumerable<ProdutoDTO.ProdutoResponse>> ObterTodosAsync();
    Task<ProdutoDTO.ProdutoResponse> ObterPorIdAsync(Guid id);
    Task AdicionarAsync(ProdutoDTO.ProdutoRequest dto);
    Task RemoverAsync(Guid id);
    Task AtualizarAsync(ProdutoDTO.ProdutoWithIdRequest dto);
}