namespace AuroraLauncher
{
    internal class Build
    {
        internal const string ClientExecutable = "FortniteClient-Win64-Shipping.exe";
#if !NO_EGL
        internal const string ClientArguments = "";
#else
        internal const string ClientArguments = "-epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal";
#endif

        internal const string LauncherNative = "AuroraNative.dll";
        internal const string LauncherUri = "https://aurorafn.dev";

        // TODO: Figure out how to generate FLToken's without hardcoding them.
        internal const string BeToken = "f7b9gah4h5380d10f721dd6a";
        internal const string EacToken = "10ga222d803bh65851660E3d";
    }
}
