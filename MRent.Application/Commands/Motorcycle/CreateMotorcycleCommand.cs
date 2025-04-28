using MRent.Domain.Commands;

namespace MRent.Application.Commands.Motorcycle
{
    public sealed class CreateMotorcycleCommand : ICommand
    {
        public required string Identifier { get; set; }
        public int Year { get; set; }
        public required string Model { get; set; }
        public required string Plate { get; set; }

        public CreateMotorcycleCommand() { }

        public CreateMotorcycleCommand(string identifier, int year, string model, string plate)
        {
            Identifier = identifier;
            Year = year;
            Model = model;
            Plate = plate;
        }
    }
}
