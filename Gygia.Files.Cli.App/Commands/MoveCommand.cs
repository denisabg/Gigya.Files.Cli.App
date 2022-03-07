using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Gigya.Files.Cli.App.Commands
{

    public class MoveCommand : INotification
    {
        public string SourcePath { get; set; }
        public string TargetPath { get; set; }
    }

    public class MoveCommandHandler : INotificationHandler<MoveCommand>
    {
        public Task Handle(MoveCommand notification, CancellationToken cancellationToken)
        {
            File.Move(notification.SourcePath, notification.TargetPath);

            return Task.CompletedTask;
        }
    }
}
