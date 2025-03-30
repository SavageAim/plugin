using Dalamud.Logging;
using SavageAimPlugin.Data;
using System.Threading.Tasks;

namespace SavageAimPlugin.Manager;

public class GearImportManager
{
    public volatile bool IsDataReady;
    public volatile bool IsDataLoading;
    public volatile bool HasFailed;
    public volatile bool IsSaving;
    public volatile bool HasSaved;
    public volatile bool HasFailedSaving;
    public ImportResponse? Data;

    public void FetchData(string apiKey)
    {
        if (this.IsDataLoading) return;

        this.IsDataReady = false;
        this.IsDataLoading = true;
        var currentCharacter = Service.CharacterDataManager.InGameCharacter;
        Task.Run(async () =>
        {
            await SavageAimClient.ImportCurrentGear(apiKey, currentCharacter).ConfigureAwait(false);
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

    public void SaveBis(string apiKey, BISList bis, ImportResponse currentGear)
    {
        if (this.IsSaving) return;

        this.IsSaving = true;
        this.HasSaved = false;
        this.HasFailedSaving = false;
        var currentCharacter = Service.CharacterDataManager.GetCurrentCharacterInSA();
        Task.Run(async () =>
        {
            // Create an Instance of a BISListModify, taking the current BIS data along with the imported current data
            var data = new BISListModify(bis, currentGear);
            await SavageAimClient.UpdateBis(apiKey, data, currentCharacter).ConfigureAwait(false);
        }).ContinueWith(task =>
        {
            if (!this.HasSaved) this.HasFailedSaving = true;
            this.IsSaving = false;
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

    public void FinishedSaving()
    {
        this.HasSaved = true;
    }

    public void Reset()
    {
        this.IsDataReady = false;
        this.Data = null;
        this.IsDataLoading = false;
        this.HasFailed = false;
        this.HasSaved = false;
        this.IsSaving = false;
        this.HasFailedSaving = false;
    }
}
