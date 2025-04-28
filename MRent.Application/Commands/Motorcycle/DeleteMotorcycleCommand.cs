using MRent.Domain.Commands;

namespace MRent.Application.Commands.Motorcycle
{
    public sealed class DeleteMotorcycleCommand : ICommand
    {
        public required string Identifier { get; set; }
        public int Year { get; set; }
        public required string Model { get; set; }
        public required string Plate { get; set; }

        public DeleteMotorcycleCommand() { }

        public DeleteMotorcycleCommand(string identifier, int year, string model, string plate)
        {
            Identifier = identifier;
            Year = year;
            Model = model;
            Plate = plate;
        }
    }
}
