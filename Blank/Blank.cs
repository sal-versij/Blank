using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Blank.Services.App;
using Blank.Services.Registry;
using Blank.Services.Registry.Requests;
using Lamar;

namespace Blank {
    public static class Blank {
    #region Fields
        public static Container BlankContainer { get; private set; }
        public static BlankServiciesRegistry BlankServiciesRegistry { get; private set; }
    #endregion
    #region Methods
        public static async Task<int> Main(string[] args) {
            try {
                Console.WriteLine("Starting initialization of Blank");

            #region Lamar Initialization
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Starting initialization of Lamar container");
                BlankServiciesRegistry = new BlankServiciesRegistry();

                BlankContainer = new Container(BlankServiciesRegistry);
                var assemblyLoadRequests = BlankContainer.GetAllInstances<IAssemblyLoadRequest>();

                foreach (var request in assemblyLoadRequests)
                    request.Load();

                var configRequests = BlankContainer.GetAllInstances<IConfigRequest>();

                foreach (var request in configRequests)
                    BlankContainer.Configure(request);

                Console.WriteLine(BlankContainer.WhatDidIScan());

                Console.WriteLine("initialization of Lamar container completed");
                Console.ResetColor();
            #endregion
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Initialization of Blank completed");
                Console.ResetColor();

            #region App Initialization
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("App initialization");

                if (!(BlankContainer.GetInstance(typeof(AppManager)) is AppManager appInitializer)) throw new Exception("AppManager unavailable");

                Console.WriteLine("App execution");
                Console.ResetColor();
                await appInitializer.Execute();

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("App Terminated");
                Console.ResetColor();
            #endregion

                Console.WriteLine("Blank Closed");
            } catch(Exception ex) {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkRed;
                await Console.Error.WriteLineAsync(ex.ToString());
                Console.ResetColor();
                return -1;
            }

            return 0;
        }
    #endregion
    }
}