namespace MRent.Application.DTO
{
    public class MotorcycleDTO
    {
        public string? identificador { get; set; }
        public int ano { get; set; }
        public required string modelo { get; set; }
        public required string placa { get; set; }
    }
}
