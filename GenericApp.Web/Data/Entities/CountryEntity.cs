using System.ComponentModel.DataAnnotations;

namespace GenericApp.Web.Data.Entities
{
    public class CountryEntity
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "País")]
        public string Name { get; set; }

        [Display(Name = "Bandera")]
        public string FlagImageUrl { get; set; }

        public string FlagImageFullPath => string.IsNullOrEmpty(FlagImageUrl)
          ? $"http://keypress.serveftp.net:88/GenericAppApi/images/Flags/noimage.png"
          : $"http://keypress.serveftp.net:88/GenericAppApi{FlagImageUrl.Substring(1)}";
    }
}