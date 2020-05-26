using System.Diagnostics;
using System.Reflection;

namespace AuroraLauncher.Providers
{
    static class App
    {
        #region Property Region

        public static string Version => $"v{FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion}";

        #endregion
    }
}
