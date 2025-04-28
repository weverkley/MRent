using MRent.Domain.Base;

namespace MRent.Domain.Entities
{
    public class RentEntity : BaseEntity
    {
        public Guid CourierId { get; set; }
        public Guid MotorcycleId { get; set; }
        public Guid PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }

        public required virtual CourierEntity Courier { get; set; }
        public required virtual MotorcycleEntity Motorcycle { get; set; }
        public required virtual PlanEntity Plan { get; set; }
    }
}
