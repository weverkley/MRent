using MRent.Domain.Commands;

namespace MRent.Application.Commands.Rent
{
    public sealed class UpdateRentCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid CourierId { get; set; }
        public Guid MotorcycleId { get; set; }
        public Guid PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public double Tax { get; set; }
        public double Subtotal { get; set; }
        public double Total { get; set; }

        public UpdateRentCommand() { }

        public UpdateRentCommand(Guid id, Guid courierId, Guid motorcycleId, Guid planId, DateTime startDate, DateTime endDate, DateTime expectedEndDate, DateTime returnDate, double tax, double subtotal, double total)
        {
            Id = id;
            CourierId = courierId;
            MotorcycleId = motorcycleId;
            PlanId = planId;
            StartDate = startDate;
            EndDate = endDate;
            ExpectedEndDate = expectedEndDate;
            ReturnDate = returnDate;
            Tax = tax;
            Subtotal = subtotal;
            Total = total;
        }
    }
}
