﻿using McMaster.Extensions.CommandLineUtils;
using NameSearch.App.Commands;

namespace NameSearch.App.CommandConfigurations
{
    public static class GreetCommandConfiguration
    {
        public static void Configure(CommandLineApplication command, CommandLineOptions options)
        {
            command.Description = "An example command from the neat .NET Core Starter";
            command.HelpOption("--help|-h|-?");

            var nameArgument = command.Argument("name",
                                   "Name I should say hello to");

            command.OnExecute(() =>
            {
                options.Command = new GreetCommand(nameArgument.Value, options);

                return 0;
            });
        }
    }
}