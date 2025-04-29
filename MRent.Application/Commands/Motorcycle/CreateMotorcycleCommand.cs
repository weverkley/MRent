using MRent.Domain.Commands;

namespace MRent.Application.Commands.Motorcycle
{
    public sealed class CreateMotorcycleCommand : ICommand
    {
        public int Year { get; set; }
        public required string Model { get; set; }
        public required string Plate { get; set; }

        public CreateMotorcycleCommand() { }

        public CreateMotorcycleCommand(int year, string model, string plate)
        {
            Year = year;
            Model = model;
            Plate = plate;
        }
    }
}
