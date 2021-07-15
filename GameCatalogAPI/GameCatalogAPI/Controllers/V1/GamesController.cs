using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GameCatalogAPI.Exceptions;
using GameCatalogAPI.InputModel;
using GameCatalogAPI.Services;
using GameCatalogAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GameCatalogAPI.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }
        /// <summary>
        /// List all games in paging format
        /// </summary>
        /// <remarks>
        /// Paging reduces resources consumption
        /// </remarks>
        /// <param name="pagina">Min: 1</param>
        /// <param name="quantidade">Rows per page. Min: 1 - Max: 50</param>
        /// <response code="200">Game list.</response>
        /// <response code="204">No games found!</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Obtain([FromQuery,Range(1,int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            
            var jogos = await _gameService.Obtain(pagina, quantidade);

            if (jogos.Count() == 0)
                return NoContent();

            return Ok(jogos);
        }

        /// <summary>
        /// Search game using Id
        /// </summary>
        /// <param name="idJogo">Game Id</param>
        /// <response code="200">Game found.</response>
        /// <response code="204">Game not found!</response> 
        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<GameViewModel>> Obtain([FromRoute] Guid idJogo)
        {
            var jogo = await _gameService.Obtain(idJogo);
            if (jogo == null)
                return NoContent();

            return Ok(jogo);
        }

        /// <summary>
        /// Insert game 
        /// </summary>
        /// <param name="gameInputModel">Game data</param>
        /// <response code="200">Game added successfully.</response>
        /// <response code="422">Game and Publisher already filled.</response> 
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody]GameInputModel gameInputModel)
        {
            try
            {
                var jogo = await _gameService.Insert(gameInputModel);

                return Ok(jogo);
            }
            catch (JogoJaCadastradoException ex)
            
            {
                return UnprocessableEntity("This game is already filled to this publisher");
            }
        }

        /// <summary>
        /// Update game info
        /// </summary>
        /// /// <param name="idJogo">Game Id</param>
        /// <param name="gameInputModel">Update info</param>
        /// <response code="200">Updated successfully.</response>
        /// <response code="404">Game Id not found.</response>   
        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idJogo, [FromBody] GameInputModel gameInputModel)
        {
            try 
            {
                await _gameService.Update(idJogo, gameInputModel);

                return Ok();
            }
             catch (JogoNaoCadastradoException ex)
            
            {
                return NotFound("Game not found");
            
            }
        }


        /// <summary>
        /// Update game price
        /// </summary>
        /// /// <param name="idJogo">Game Id</param>
        /// <param name="preco">New price</param>
        /// <response code="200">Price successfully changed.</response>
        /// <response code="404">Game Id not found!</response>  
        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _gameService.Update(idJogo, preco);

                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            
            {
                return NotFound("Game not found!");
            }
        }

        /// <summary>
        /// Delete game
        /// </summary>
        /// /// <param name="idJogo">Game Id</param>
        /// <response code="200">Game deleted successfully.</response>
        /// <response code="404">Game Id not found!</response>  
        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid idJogo)
        {
            try
            {
                await _gameService.Delete(idJogo);

                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            
            {
                return NotFound("Game not found!");
            }
        }
 

    }
}
