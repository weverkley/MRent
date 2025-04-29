using MRent.Domain.Base;

namespace MRent.Domain.Entities
{
    public class MotorcycleLogEntity : BaseEntity
    {
        public Guid MotorcycleId { get; set; }
        public int Year { get; set; }
        public required string Model { get; set; }
        public required string Plate { get; set; }
    }
}
