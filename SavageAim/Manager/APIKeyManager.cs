using System.Threading.Tasks;

namespace SavageAimPlugin.Manager;

public class APIKeyManager
{
    public volatile bool IsDataReady;
    public volatile bool IsDataLoading;
    public volatile bool HasFailed;
    public volatile bool NeedsRevalidation;
    public volatile bool APIKeyIsValid = false;

    public void ValidateAPIKey()
    {
        if (this.IsDataLoading) return;

        this.IsDataReady = false;
        this.IsDataLoading = true;
        Task.Run(async () =>
        {
            await SavageAimClient.TestApiKey(Service.Configuration.apiKey).ConfigureAwait(false);
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

    public void SetKeyIsValid(bool valid)
    {
        this.APIKeyIsValid = valid;
        this.IsDataReady = true;
        this.NeedsRevalidation = false;

        if (valid)
        {
            Service.Configuration.Save();
            Service.CharacterDataManager.Reset();
            Service.BISListDataManager.Reset();
        }
    }

    public void Reset()
    {
        this.IsDataReady = false;
        this.APIKeyIsValid = false;
        this.IsDataLoading = false;
        this.HasFailed = false;
    }
}
