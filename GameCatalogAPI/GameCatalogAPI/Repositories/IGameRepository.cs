using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCatalogAPI.Entities;

namespace GameCatalogAPI.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Obtain(int pagina, int quantidade);
        Task<Game> Obtain(Guid id);
        Task<List<Game>> Obtain(string nome, string produtora);
        Task Insert(Game jogo);
        Task Update(Game jogo);
        Task Delete(Guid id);
        Task<List<Game>> YearPublished(string ano);

    }
}
