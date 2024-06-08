using Dalamud.IoC;
using Dalamud.Plugin.Services;
using SavageAim;
using SavageAimPlugin.Manager;

namespace SavageAimPlugin;

internal class Service
{
    internal static APIKeyManager APIKeyManager { get; set; } = null!;
    internal static BISListDataManager BISListDataManager { get; set; } = null!;
    internal static CharacterDataManager CharacterDataManager { get; set; } = null!;
    internal static Configuration Configuration { get; set; } = null!;

    [PluginService] internal static IClientState ClientState { get; private set; } = null!;
    [PluginService] internal static IPluginLog PluginLog { get; private set; } = null!;
}
