using MRent.Domain.Commands;

namespace MRent.Application.Commands.Rent
{
    public sealed class CreateRentCommand : ICommand
    {
        public Guid CourierId { get; set; }
        public Guid MotorcycleId { get; set; }
        public Guid PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }

        public CreateRentCommand() { }

        public CreateRentCommand(Guid courierId, Guid motorcycleId, Guid planId, DateTime startDate, DateTime endDate, DateTime expectedEndDate)
        {
            CourierId = courierId;
            MotorcycleId = motorcycleId;
            PlanId = planId;
            StartDate = startDate;
            EndDate = endDate;
            ExpectedEndDate = expectedEndDate;
        }
    }
}
