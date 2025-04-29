using MRent.Domain.Base;
using MRent.Domain.Enums;

namespace MRent.Domain.Entities
{
    public class CourierEntity : BaseEntity
    {
        public required string Name { get; set; }
        public required string CNPJ { get; set; }
        public required DateTime BornDate { get; set; }
        public required string CNH { get; set; }
        public required ECNHType CNHType { get; set; }
        public string? CNHImage { get; set; }

        public virtual ICollection<RentEntity> Rents { get; set; } = new List<RentEntity>();
    }
}
