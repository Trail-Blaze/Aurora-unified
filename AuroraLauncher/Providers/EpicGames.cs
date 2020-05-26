using Newtonsoft.Json;
using System.IO;

using static System.Environment;

namespace AuroraLauncher.Providers
{
    static class EpicGames
    {
        #region Class Region

        public class Installed
        {
            public class Installation
            {
                [JsonProperty("InstallLocation")]
                public string InstallLocation { get; set; }

                [JsonProperty("AppName")]
                public string AppName { get; set; }
                [JsonProperty("AppVersion")]
                public string AppVersion { get; set; }
            }

            [JsonProperty("InstallationList")]
            public Installation[] InstallationList { get; set; }
        }

        #endregion

        #region Property Region

        public static Installed LauncherInstalled => GetLauncherInstalled();

        #endregion

        #region Method Region

        static Installed GetLauncherInstalled()
        {
            string path = Path.Combine(GetFolderPath(SpecialFolder.CommonApplicationData),
                "Epic\\UnrealEngineLauncher\\LauncherInstalled.dat");

            if (!File.Exists(path))
                return null;
            else
                return JsonConvert.DeserializeObject<Installed>(File.ReadAllText(path));
        }

        #endregion
    }
}
