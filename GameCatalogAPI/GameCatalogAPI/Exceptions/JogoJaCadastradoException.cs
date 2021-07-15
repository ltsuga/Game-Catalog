using System;

namespace GameCatalogAPI.Exceptions
{
    public class JogoJaCadastradoException : Exception
    {
        public JogoJaCadastradoException() : base("Game already filled.")
        { }
    }
}
