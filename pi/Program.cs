﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using pi_dotnetcore.Gpio;
using pi.commands;

namespace pi
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IGpioAdapter, FileSystemGpioAdapter>()
                .AddSingleton<ICommandRunner, DefaultCommandRunner>()
                .AddTransient<ICommand, InitCommand>()
                .AddTransient<ICommand, DeinitCommand>()
                .AddTransient<ICommand, StatusCommand>()
                .AddTransient<ICommand, InputCommand>()
                .AddTransient<ICommand, OutputCommand>()
                .AddTransient<ICommand, StatusCommand>()
                .AddTransient<ICommand, OnCommand>()
                .AddTransient<ICommand, OffCommand>()
                .AddTransient<ICommand, PhotoCommand>()
                .AddTransient<ICommand, PhotoloopCommand>()
                .BuildServiceProvider();

            try
            {
                serviceProvider.GetService<ICommandRunner>().RunCommand(args);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
            }
        }
    }
}