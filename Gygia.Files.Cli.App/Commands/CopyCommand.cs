using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Gigya.Files.Cli.App.Commands
{

    public class CopyCommand : INotification
    {
        public string SourcePath { get; set; }
        public string TargetPath { get; set; }
    }

    public class CopyCommandHandler : INotificationHandler<CopyCommand>
    {
        public Task Handle(CopyCommand notification, CancellationToken cancellationToken)
        {
                
            File.Copy(notification.SourcePath, notification.TargetPath);
            return Task.CompletedTask;
        }
    }
}
