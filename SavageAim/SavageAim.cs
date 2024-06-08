using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using ECommons;
using SavageAim.Windows;
using SavageAimPlugin;
using SavageAimPlugin.Manager;

namespace SavageAim;

public sealed class SavageAim : IDalamudPlugin
{
    private const string CommandName = "/sa";

    private DalamudPluginInterface PluginInterface { get; init; }
    private ICommandManager CommandManager { get; init; }
    public readonly WindowSystem WindowSystem = new("SavageAimPlugin");
    private SavageAimWindow MainWindow { get; init; }

    public SavageAim(
        [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
        [RequiredVersion("1.0")] ICommandManager commandManager)
    {
        PluginInterface = pluginInterface;
        CommandManager = commandManager;

        pluginInterface.Create<Service>();

        Service.Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        Service.Configuration.Initialize(PluginInterface);

        Service.APIKeyManager = new APIKeyManager();
        Service.BISListDataManager = new BISListDataManager();
        Service.CharacterDataManager = new CharacterDataManager();

        MainWindow = new SavageAimWindow(this);
        WindowSystem.AddWindow(MainWindow);

        CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
        {
            HelpMessage = "Check your BIS Lists from SavageAim in game, and sync your current gear to lists of your choice!"
        });

        PluginInterface.UiBuilder.Draw += DrawUI;

        // This adds a button to the plugin installer entry of this plugin which allows
        // to toggle the display status of the main ui of the plugin
        PluginInterface.UiBuilder.OpenMainUi += ToggleMainUI;

        // Add ECommons
        ECommonsMain.Init(pluginInterface, this);

        // Add a Handler to the Logout to reset the data
        Service.ClientState.Logout += ResetData;
    }

    public void Dispose()
    {
        WindowSystem.RemoveAllWindows();

        MainWindow.Dispose();

        CommandManager.RemoveHandler(CommandName);
        ECommonsMain.Dispose();

        // Remove Event Handlers
        Service.ClientState.Logout -= ResetData;
    }

    private void OnCommand(string command, string args)
    {
        // in response to the slash command, just toggle the display status of our main ui
        ToggleMainUI();
    }

    private void ResetData()
    {
        Service.BISListDataManager.Reset();
    }

    private void DrawUI() => WindowSystem.Draw();
    public void ToggleMainUI() => MainWindow.Toggle();
}
