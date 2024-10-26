using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public partial class PlayerManager : ManagerBase
{
    public override ReinitializationPolicy ReInitPolicy => ReinitializationPolicy.NOT_ALLOWED;

    protected override void EndInitializationBehavior()
    {
        //
    }

    protected override void StartInitializationBehavior()
    {
        StartInitializationHelper().ContinueWith(_ => EndInitialize(true), TaskScheduler.FromCurrentSynchronizationContext());
    }
    private async Task StartInitializationHelper()
    {
        var loadPlayerTask = ReadFromDisk<PlayerData>(PlayerPath);
        var loadPreferenceTask = ReadFromPref<PlayerPreference>(PREFERENCE_KEY);

        var tasks = new List<Task> { loadPlayerTask, loadPreferenceTask };
        var total = tasks.Count;

        while (tasks.Count > 0)
        {
            var finishedTask = await Task.WhenAny(tasks);
            await finishedTask;
            tasks.Remove(finishedTask);
        }

        var (foundPlayer, player) = await loadPlayerTask;
        var (foundPref, pref) = await loadPreferenceTask;

        if (!foundPlayer)
        {
            ConvertVersion1(out _playerData);

            PlayerPrefs.DeleteAll();
        }
        else
        {
            _playerData = player;
        }

        _preference = foundPref ? pref : new PlayerPreference();

    }
}
