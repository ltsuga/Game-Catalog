using System;
using System.ComponentModel.DataAnnotations;

namespace GameCatalogAPI.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength =3,ErrorMessage ="Name field ranges from 3 to 100")]
        public string Nome { get; set; }


        [Required]
        [StringLength(100,MinimumLength =3,ErrorMessage ="Publihser field ranges from 3 to 100")]
        public string Produtora { get; set; }

        [Required]
        [Range(1,1000,ErrorMessage ="Price ranges form 1 to 1000")]
        public double Preco { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Year field format: YYYY")]
        public string Ano { get; set; }

    }
}
