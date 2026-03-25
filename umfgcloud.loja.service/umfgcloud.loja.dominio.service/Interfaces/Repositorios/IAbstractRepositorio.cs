using umfgcloud.loja.dominio.service.Entidades;

namespace umfgcloud.loja.dominio.service.Interfaces.Repositorios;

public interface IAbstractRepositorio<T> where T : AbstractEntity
{
    Task<ICollection<T>> ObterTodosAsync();
    Task<T> ObterPorIdAsync(Guid id);
    Task AdicionarAsync(T entity);
    Task RemoverAsync(T entity);
    Task AtualizarAsync(T entity);    
}