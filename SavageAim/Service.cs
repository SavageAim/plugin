using Dalamud.IoC;
using Dalamud.Plugin.Services;
using SavageAim;
using SavageAimPlugin.Manager;

namespace SavageAimPlugin;

internal class Service
{
    internal static BISListDataManager BISListDataManager { get; set; } = null!;
    internal static Configuration Configuration { get; set; } = null!;

    [PluginService] internal static IClientState ClientState { get; private set; } = null!;
}
