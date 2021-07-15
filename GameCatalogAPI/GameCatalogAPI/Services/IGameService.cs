using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCatalogAPI.InputModel;
using GameCatalogAPI.ViewModel;

namespace GameCatalogAPI.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> Obtain(int pagina, int quantidade);
        Task<GameViewModel> Obtain(Guid id);
        Task<GameViewModel> Insert(GameInputModel jogo);
        Task Update(Guid id, GameInputModel jogo);
        Task Update(Guid d, double preco);
        Task Delete(Guid id);
        Task<List<GameViewModel>> YearPublished(string ano);
    }
}
