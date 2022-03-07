using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Gigya.Files.Cli.App.Commands
{

    public class RemoveCommand : INotification
    {
        public string Path { get; set; }
    }

    public class RemoveCommandHandler : INotificationHandler<RemoveCommand>
    {
        public Task Handle(RemoveCommand notification, CancellationToken cancellationToken)
        {
            foreach (var file in Directory.EnumerateFileSystemEntries(notification.Path))
                File.Delete(file);

            return Task.CompletedTask;
        }
    }
}
