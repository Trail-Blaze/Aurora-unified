using RestSharp;

namespace AuroraLauncher.Providers
{
    static class Api
    {
        #region Field Region

        static RestClient _client = new RestClient($"{Build.LauncherUrl}/aurora/api");

        #endregion

        #region Property Region

        public static string Version => GetVersion();

        #endregion

        #region Method Region

        static string GetVersion()
        {
            string version = _client.Get(new RestRequest("version")).Content;

            if (string.IsNullOrEmpty(version))
                return "Offline";

            return version;
        }

        #endregion
    }
}
