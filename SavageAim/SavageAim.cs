using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using SavageAim.Windows;
using SavageAimPlugin;
using SavageAimPlugin.Manager;

namespace SavageAim;

public sealed class SavageAim : IDalamudPlugin
{
    [PluginService] internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService] internal static ICommandManager CommandManager { get; private set; } = null!;

    private const string CommandName = "/sa";
    public readonly WindowSystem WindowSystem = new("SavageAimPlugin");
    private SavageAimWindow MainWindow { get; init; }
    public GearUpdateWindow UpdateWindow { get; init; }

    public SavageAim()
    {
        PluginInterface.Create<Service>();

        Service.Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();

        Service.APIKeyManager = new APIKeyManager();
        Service.BISListDataManager = new BISListDataManager();
        Service.CharacterDataManager = new CharacterDataManager();
        Service.GearImportManager = new GearImportManager();

        MainWindow = new SavageAimWindow(this);
        WindowSystem.AddWindow(MainWindow);
        UpdateWindow = new GearUpdateWindow(this);
        WindowSystem.AddWindow(UpdateWindow);

        CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
        {
            HelpMessage = "Check your BIS Lists from SavageAim in game, and sync your current gear to lists of your choice!"
        });

        PluginInterface.UiBuilder.Draw += DrawUI;

        // This adds a button to the plugin installer entry of this plugin which allows
        // to toggle the display status of the main ui of the plugin
        PluginInterface.UiBuilder.OpenMainUi += ToggleMainUI;

        // Add a Handler to the Logout to reset the data
        Service.ClientState.Logout += ResetData;
    }

    public void Dispose()
    {
        WindowSystem.RemoveAllWindows();

        MainWindow.Dispose();

        CommandManager.RemoveHandler(CommandName);

        // Remove Event Handlers
        Service.ClientState.Logout -= ResetData;
    }

    private void OnCommand(string command, string args)
    {
        // in response to the slash command, just toggle the display status of our main ui
        ToggleMainUI();
    }

    private void ResetData(int type, int code)
    {
        Service.BISListDataManager.Reset();
    }

    private void DrawUI() => WindowSystem.Draw();
    public void ToggleMainUI() => MainWindow.Toggle();
}
