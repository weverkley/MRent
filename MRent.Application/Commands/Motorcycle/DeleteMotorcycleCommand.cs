using MRent.Domain.Commands;

namespace MRent.Application.Commands.Motorcycle
{
    public sealed class DeleteMotorcycleCommand : ICommand
    {
        public Guid Id { get; set; }

        public DeleteMotorcycleCommand() { }

        public DeleteMotorcycleCommand(Guid id)
        {
            Id = id;
        }
    }
}
