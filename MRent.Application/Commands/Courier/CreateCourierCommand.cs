using MRent.Domain.Commands;
using MRent.Domain.Enums;

namespace MRent.Application.Commands.Motorcycle
{
    public sealed class CreateCourierCommand : ICommand
    {
        public required string Identifier { get; set; }
        public required string Name { get; set; }
        public required string CNPJ { get; set; }
        public required DateTime BornDate { get; set; }
        public required string CNH { get; set; }
        public required ECNHType CNHType { get; set; }
        public string? CNHImage { get; set; }

        public CreateCourierCommand() { }

        public CreateCourierCommand(string identifier, string name, string cnpj, DateTime bornDate, string cnh, ECNHType cnhtype, string cnhimage)
        {
            Identifier = identifier;
            Name = name;
            CNPJ = cnpj;
            BornDate = bornDate;
            CNH = cnh;
            CNHType = cnhtype;
            CNHImage = cnhimage;
        }
    }
}
