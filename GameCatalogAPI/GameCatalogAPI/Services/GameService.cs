using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameCatalogAPI.Entities;
using GameCatalogAPI.Exceptions;
using GameCatalogAPI.InputModel;
using GameCatalogAPI.Repositories;
using GameCatalogAPI.ViewModel;

namespace GameCatalogAPI.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository jogoRepository)
        {
            _gameRepository = jogoRepository;
        }

        public async Task<List<GameViewModel>> Obtain(int pagina, int quantidade)
        {
            var jogos = await _gameRepository.Obtain(pagina, quantidade);

            return jogos.Select(jogo => new GameViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco,
                Ano = jogo.Ano
            })
                               .ToList();
        }

        public async Task<GameViewModel> Obtain(Guid id)
        {
            var jogo = await _gameRepository.Obtain(id);

            if (jogo == null)
                return null;

            return new GameViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco,
                Ano = jogo.Ano
            };
        }

        public async Task<GameViewModel> Insert(GameInputModel jogo)
        {
            var entidadeJogo = await _gameRepository.Obtain(jogo.Nome, jogo.Produtora);

            if (entidadeJogo.Count > 0)
                throw new JogoJaCadastradoException();
                

            var jogoInsert = new Game
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco,
                Ano = jogo.Ano
            };

            await _gameRepository.Insert(jogoInsert);

            return new GameViewModel
            {
                Id = jogoInsert.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco,
                Ano = jogo.Ano
            };
        }

        public async Task Update(Guid id, GameInputModel jogo)
        {
            var entidadeJogo = await _gameRepository.Obtain(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();
                

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;
            entidadeJogo.Ano = jogo.Ano;

            await _gameRepository.Update(entidadeJogo);
        }

        public async Task Update(Guid id, double preco)
        {
            var entidadeJogo = await _gameRepository.Obtain(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();
                

            entidadeJogo.Preco = preco;

            await _gameRepository.Update(entidadeJogo);
        }

        public async Task Delete(Guid id)
        {
            var jogo = await _gameRepository.Obtain(id);

            if (jogo == null)
                throw new JogoNaoCadastradoException();
                

            await _gameRepository.Delete(id);
        }

        public void Dispose()
        {
            //Close Db Connections
          
        }

        public async Task<List<GameViewModel>> YearPublished(string ano)
        {
            var jogos = await _gameRepository.YearPublished(ano);

            return jogos.Select(jogo => new GameViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco,
                Ano = jogo.Ano
            })
                               .ToList();
        }

    }
}
