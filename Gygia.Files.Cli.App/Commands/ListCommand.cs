using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Gigya.Files.Cli.App.Commands
{
    public class ListCommand : INotification
    {
        public string Path { get; set; }
        public string Option { get; set; }
    }

    public class ListCommandHandler : INotificationHandler<ListCommand>
    {
        public Task Handle(ListCommand notification, CancellationToken cancellationToken)
        {
            string[] res;

            res = notification.Option == null 
                ? Directory.GetFiles(notification.Path) 
                : Directory.EnumerateFileSystemEntries(notification.Path).ToArray();


            foreach (var filename in res)
                Console.WriteLine(filename);

            return Task.CompletedTask;
        }
    }
}
