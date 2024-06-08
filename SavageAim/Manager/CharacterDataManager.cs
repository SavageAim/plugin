using SavageAimPlugin.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SavageAimPlugin.Manager;

public class CharacterDataManager
{
    public volatile bool IsDataReady;
    public volatile bool IsDataLoading;
    public volatile bool HasFailed;
    public List<SACharacter> Data = new();

    public InGameCharacterData? InGameCharacter
    {
        get
        {
            if (Service.ClientState.LocalPlayer == null) return null;
            return new InGameCharacterData();
        }
    }

    public void FetchData(string apiKey)
    {
        if (this.IsDataLoading) return;

        this.IsDataReady = false;
        this.IsDataLoading = true;
        Task.Run(async () =>
        {
            await SavageAimClient.GetCharacters(apiKey).ConfigureAwait(false);
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

    public SACharacter? GetCurrentCharacterInSA()
    {
        if (!this.IsDataReady) return null;
        var currentChar = this.InGameCharacter;
        if (currentChar == null) return null;
        return this.Data.Find(sa => sa.Name == currentChar!.Name && sa.World.Split(" ")[0] == currentChar.World);
    }

    public void SetData(List<SACharacter> data)
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
