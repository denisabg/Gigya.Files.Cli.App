using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Gigya.Files.Cli.App.Commands
{
    public class MakedirCommand : INotification
    {
        public string Path { get; set; }
        public string Name { get; set; }
    }

    public class MakedirCommandHandler : INotificationHandler<MakedirCommand>
    {
        public Task Handle(MakedirCommand notification, CancellationToken cancellationToken)
        {
            Directory.CreateDirectory($"{notification.Path}//{notification.Name}");
            return Task.CompletedTask;
        }
    }
}
