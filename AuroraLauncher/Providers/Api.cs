using RestSharp;
using System;

namespace AuroraLauncher.Providers
{
    static class Api
    {
        #region Field Region

        static RestClient _client = new RestClient($"{Build.LauncherUrl}/aurora/api");

        #endregion

        #region Property Region

        public static string Version => GetVersion();

        public static int Online => GetOnline();

        #endregion

        #region Method Region

        static string GetVersion()
        {
            string version = _client.Get(new RestRequest("version")).Content;

            if (string.IsNullOrEmpty(version))
                return "Offline";

            return version;
        }

        public static void Heartbeat() => _client.Post(new RestRequest("heartbeat"));

        static int GetOnline()
        {
            string online = _client.Get(new RestRequest("online")).Content;

            if (string.IsNullOrEmpty(online))
                return 0;

            return Convert.ToInt32(online);
        }

        #endregion
    }
}
