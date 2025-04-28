using System.ComponentModel.DataAnnotations;
using MRent.WebApi.Attributes;

namespace MRent.WebApi.Models
{
    public class AtualizarImagem
    {
        [DataType(DataType.Upload)]
        [MaxFileSize(3 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".png", ".bmp" })]
        public IFormFile Imagem_cnh { get; set; }
    }
}
