using System;

namespace GameCatalogAPI.ViewModel
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Produtora { get; set; }
        public double Preco { get; set; }

        public string Ano { get; set; }

    }
}
