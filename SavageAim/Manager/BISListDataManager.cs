using Dalamud.Logging;
using SavageAimPlugin.Data;
using System.Threading.Tasks;

namespace SavageAimPlugin.Manager;

public class BISListDataManager
{
    public volatile bool IsDataReady;
    public volatile bool IsDataLoading;
    public volatile bool HasFailed;
    public BISList[] Data = [];

    public void FetchData(string apiKey, uint charId)
    {
        if (this.IsDataLoading) return;

        this.IsDataReady = false;
        this.IsDataLoading = true;
        Task.Run(async () =>
        {
            await SavageAimClient.GetBisLists(apiKey, charId).ConfigureAwait(false);
        }).ContinueWith(task =>
        {
            if (!this.IsDataReady) this.HasFailed = true;

            this.IsDataLoading = false;
            if (!task.IsFaulted) return;
            if (task.Exception == null) return;
            foreach (var e in task.Exception.Flatten().InnerExceptions)
            {
                PluginLog.Error(e, "Network error.");
            }
        });
    }

    public void SetData(BISList[] data)
    {
        this.Data = data;
        this.IsDataReady = true;
    }

    public void Reset()
    {
        this.IsDataReady = false;
        this.Data = [];
        this.IsDataLoading = false;
        this.HasFailed = false;
    }
}
