using Mapster;
using Microsoft.AspNetCore.Http;
using umfgcloud.loja.dominio.service.DTO;
using umfgcloud.loja.dominio.service.Entidades;
using umfgcloud.loja.dominio.service.Interfaces.Repositorios;
using umfgcloud.loja.dominio.service.Interfaces.Servicos;

namespace umfgcloud.loja.aplicacao.service.Classes;

public sealed class ProdutoServico : AbstractServico, IProdutoServico
{
    private readonly IProdutoRepositorio _produtoRepositorio;

    public ProdutoServico(IHttpContextAccessor httpContextAccessor, IProdutoRepositorio produtoRepositorio) : base(httpContextAccessor)
        => _produtoRepositorio = produtoRepositorio ?? throw new ArgumentNullException(nameof(produtoRepositorio));

    public async Task AdicionarAsync(ProdutoDTO.ProdutoRequest dto)
    {
        var produto = new ProdutoEntity(UserId, UserEmail);

        produto.SetDescricao(dto.Descricao);
        produto.SetEAN(dto.EAN);
        produto.SetValorCompra(dto.ValorCompra);
        produto.SetValorVenda(dto.ValorVenda);

        await _produtoRepositorio.AdicionarAsync(produto);
    }

    public async Task AtualizarAsync(ProdutoDTO.ProdutoWithIdRequest dto)
    {
        var produto = await _produtoRepositorio.ObterPorIdAsync(dto.Id);

        produto.SetDescricao(dto.Descricao);
        produto.SetEAN(dto.EAN);
        produto.SetValorCompra(dto.ValorCompra);
        produto.SetValorVenda(dto.ValorVenda);

        await _produtoRepositorio.AtualizarAsync(produto);
    }

    public async Task<ProdutoDTO.ProdutoResponse> ObterPorIdAsync(Guid id)
        => (await _produtoRepositorio.ObterPorIdAsync(id)).Adapt<ProdutoDTO.ProdutoResponse>();

    public async Task<IEnumerable<ProdutoDTO.ProdutoResponse>> ObterTodosAsync() 
        => (await _produtoRepositorio.ObterTodosAsync()).Adapt<IEnumerable<ProdutoDTO.ProdutoResponse>>();

    public async Task RemoverAsync(Guid id) 
        => await _produtoRepositorio.RemoverAsync(await _produtoRepositorio.ObterPorIdAsync(id));
}
