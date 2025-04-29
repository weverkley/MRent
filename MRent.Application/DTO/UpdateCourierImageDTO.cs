namespace MRent.Application.DTO
{
    public class UpdateCourierImageDTO
    {
        public string? identificador { get; set; }
        public required string nome { get; set; }
        public required string cnpj { get; set; }
        public required DateTime data_nascimento { get; set; }
        public required string numero_cnh { get; set; }
        public required string tipo_cnh { get; set; }
        public required string? imagem_cnh { get; set; }
    }
}
