using MRent.Domain.Commands;

namespace MRent.Application.Commands.Courier
{
    public sealed class UpdateCourierImageCNHCommand : ICommand
    {
        public required Guid Id { get; set; }
        public string CNHImage { get; set; }

        public UpdateCourierImageCNHCommand() { }

        public UpdateCourierImageCNHCommand(Guid id, string cnhImage)
        {
            Id = id;
            CNHImage = cnhImage;
        }
    }
}
