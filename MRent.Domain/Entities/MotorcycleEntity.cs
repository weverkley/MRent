using MRent.Domain.Base;

namespace MRent.Domain.Entities
{
    public class MotorcycleEntity : BaseEntity
    {
        public required string Identifier { get; set; }
        public int Year { get; set; }
        public required string Model { get; set; }
        public required string Plate { get; set; }

        public virtual ICollection<RentEntity> Rents { get; set; } = new List<RentEntity>();
    }
}
