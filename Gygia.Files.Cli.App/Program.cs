using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using Gigya.Files.Cli.App.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using static System.IO.Directory;

namespace Gygia.Files.Cli.App
{
    internal class Program
    {



        static void Main()
        {
            var text = "list full";
            var args = text.Split();
            Console.WriteLine($"args: {text}");


            var result = CommandLine.Parser.Default.ParseArguments<Options>(args)
                .MapResult((opts) => 
                        RunOptionsAndReturnExitCode(opts), //in case parser sucess
                errs => 
                    HandleParseError(errs)); //in  case parser fail

            Console.WriteLine("Return code= {0}", result);

        }

        /// <summary>
        /// Service Provider added
        /// </summary>
        /// <returns></returns>
        private static IMediator BuildMediator()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(Program));
            var provider = services.BuildServiceProvider();
            return provider.GetRequiredService<IMediator>();
        }


        /// <summary>
        /// CLI parsing method - using MediatR INotification by Command Factory
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        static int RunOptionsAndReturnExitCode(Options o)
        {
            var exitCode = 0;
            var props = o.Props;
            var cmdType = props.ToArray()[0];
            
            IMediator mediator = BuildMediator();

            INotification cmd = cmdType.ToLowerInvariant() switch
            {
                "list" => new ListCommand()
                {
                    Path = GetCurrentDirectory(), Option = props.ToArray().Length > 1 ? props.ToArray()[1] : null,
                },
                "makedir" => new MakedirCommand() { Path = GetCurrentDirectory(), Name = "newDir" },
                "remove" => new RemoveCommand() { Path = GetCurrentDirectory(), },
                "move" => new MoveCommand() { SourcePath = props.ToArray()[1], TargetPath = props.ToArray()[2] },
                "copy" => new CopyCommand() { SourcePath = props.ToArray()[1], TargetPath = props.ToArray()[2] },
                _ => null
            };
            mediator.Publish(cmd).Wait();
            
            return exitCode;
        }


        /// <summary>
        /// Error Handler for CLI Parsing
        /// </summary>
        /// <param name="errs"></param>
        /// <returns></returns>
        static int HandleParseError(IEnumerable<Error> errs)
        {
            var result = -2;
            Console.WriteLine("errors {0}", errs.Count());
            if (errs.Any(x => x is HelpRequestedError || x is VersionRequestedError))
                result = -1;
            Console.WriteLine("Exit code {0}", result);
            return result;
        }



    }

    class Options
    {
        [Value(0)]
        public IEnumerable<string> Props
        {
            get;
            set;
        }
    }


}
