using System;
using System.Threading.Tasks;

namespace Blank.Services.App {
    public class AppManager {
    #region Fields
        private readonly IApp _app;
    #endregion
    #region Ctor
        public AppManager(IApp app) {
            _app = app;
        }
    #endregion
    #region Methods
        public Task Execute() {
            if (_app == null)
                throw new Exception("No app imported");
            return _app.Run();
        }
    #endregion
    }
}