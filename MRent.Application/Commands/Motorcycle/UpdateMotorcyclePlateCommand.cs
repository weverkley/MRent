using MRent.Domain.Commands;

namespace MRent.Application.Commands.Motorcycle
{
    public sealed class UpdateMotorcyclePlateCommand : ICommand
    {
        public required Guid Id { get; set; }
        public required string Plate { get; set; }

        public UpdateMotorcyclePlateCommand() { }

        public UpdateMotorcyclePlateCommand(Guid id, string plate)
        {
            Id = id;
            Plate = plate;
        }
    }
}
