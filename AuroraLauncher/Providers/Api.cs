using RestSharp;
using System;
using System.Net;

namespace AuroraLauncher.Providers
{
    static class Api
    {
        #region Field Region

        static RestClient _client = new RestClient(Build.LauncherUri);

        #endregion

        #region Property Region

        public static string Version => GetVersion();

        public static int Clients => GetClients();

        public static int Parties => GetParties();

        #endregion

        #region Method Region

        static void SetApi()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            _client.UserAgent = $"AuroraLauncher/{App.Version}";
        }

        // Not really apart of the API, but who gives a fuck?
        static string GetVersion()
        {
#if FAKE_API
            return App.Version;
#endif

            SetApi();

            var version = _client.Get(new RestRequest("files/version")).Content;
            if (string.IsNullOrEmpty(version))
                version = "Offline";

            return version;
        }

        static int GetClients()
        {
#if FAKE_API
            return 0;
#endif

            SetApi();

            var clients = _client.Get(new RestRequest("id/api/clients")).Content;
            try
            {
                if (!string.IsNullOrEmpty(clients))
                    return Convert.ToInt32(clients);
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }

        static int GetParties()
        {
#if FAKE_API
            return 0;
#endif

            SetApi();

            var parties = _client.Get(new RestRequest("id/api/parties")).Content;
            try
            {
                if (!string.IsNullOrEmpty(parties))
                    return Convert.ToInt32(parties);
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }

#endregion
    }
}
