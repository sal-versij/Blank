using System.IO;
using Blank.Services.Registry.Requests;
using Lamar;

namespace Blank.Services.Registry {
    public class BlankServiciesRegistry : ServiceRegistry {
    #region Fields
        private readonly string _modulesPath = Path.GetFullPath(@".\modules");
    #endregion
    #region Ctor
        public BlankServiciesRegistry() {
            Scan(scanner => {
                scanner.Description = "Blank Assembly Scan";
                scanner.AssemblyContainingType<BlankServiciesRegistry>();
                scanner.WithDefaultConventions();
            });

            Scan(scanner => {
                scanner.Description = "Modules Assembly Scan";
                scanner.AssembliesFromPath(_modulesPath);
                scanner.WithDefaultConventions();
                scanner.AddAllTypesOf<IAssemblyLoadRequest>();
                scanner.AddAllTypesOf<IConfigRequest>();
            });
        }
    #endregion
    }
}