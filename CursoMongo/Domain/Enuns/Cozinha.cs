using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public enum Cozinha
    {
        [Display(Name = "Brasileira")]
        Brasileira = 1,

        [Display(Name = "Italiana")]
        Italiana = 2,

        [Display(Name = "Árabe")]
        Arabe = 3,

        [Display(Name = "Japonesa")]
        Japonesa = 4,

        [Display(Name = "Fast Food")]
        FastFood = 5
    }
}