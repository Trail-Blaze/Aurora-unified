namespace AuroraLauncher.Providers
{
    static class Launcher
    {
        public static bool IsUpToDate => Api.Version == App.Version;
    }
}
