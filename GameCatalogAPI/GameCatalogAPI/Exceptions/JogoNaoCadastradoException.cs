using System;

namespace GameCatalogAPI.Exceptions
{
    public class JogoNaoCadastradoException : Exception
    {
        public JogoNaoCadastradoException() : base("Game not filled!")
        { }
    }
}
