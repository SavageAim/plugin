using Dalamud.Logging;
using SavageAimPlugin.Data;
using System.Threading.Tasks;

namespace SavageAimPlugin.Manager;

public class GearImportManager
{
    public volatile bool IsDataReady;
    public volatile bool IsDataLoading;
    public volatile bool HasFailed;
    public ImportResponse? Data;

    public void FetchData(string apiKey)
    {
        if (this.IsDataLoading) return;

        this.IsDataReady = false;
        this.IsDataLoading = true;
        Task.Run(async () =>
        {
            await SavageAimClient.ImportCurrentGear(apiKey).ConfigureAwait(false);
        }).ContinueWith(task =>
        {
            if (!this.IsDataReady) this.HasFailed = true;

            this.IsDataLoading = false;
            if (!task.IsFaulted) return;
            if (task.Exception == null) return;
            foreach (var e in task.Exception.Flatten().InnerExceptions)
            {
                Service.PluginLog.Error(e, "Network error.");
            }
        });
    }

    public void SetData(ImportResponse? data)
    {
        this.Data = data;
        this.IsDataReady = true;
    }

    public void Reset()
    {
        this.IsDataReady = false;
        this.Data = null;
        this.IsDataLoading = false;
        this.HasFailed = false;
    }
}
