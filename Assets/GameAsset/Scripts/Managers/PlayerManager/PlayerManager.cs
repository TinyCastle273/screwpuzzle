using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerManager
{
    private PlayerData _playerData;
    private PlayerPreference _preference;

    public bool CheckCompletedWithoutExistence(string level)
    {
        return _playerData.CompletedLevels.ContainsKey(level) && _playerData.CompletedLevels[level].Completed;
    }

    public void GetLevelState(string level, out CompletedLevelData completion)
    {
        // Get completion
        if (!_playerData.CompletedLevels.ContainsKey(level))
        {
            completion = new CompletedLevelData(completed: false, bestTime: int.MaxValue);
        }
        else
        {
            completion = _playerData.CompletedLevels[level];
        }
    }

    public void SetLevelAsComplete(string level, int time)
    {
        Log.Info($"Now marking level {level} as completed.");

        // Set completion struct
        var bestTime = int.MinValue;
        if (_playerData.CompletedLevels.TryGetValue(level, out var completedLevel))
        {
            bestTime = completedLevel.BestTime;
        }

        if (time > bestTime)
        {
            _playerData.CompletedLevels[level] = new CompletedLevelData(completed: true, bestTime: time);
        }

        RequestSaveData(true, false);
    }

    public int GetResource(string name)
    {
        if(_playerData == null) _playerData = new PlayerData();
        if (_playerData.Resources == null) _playerData.Resources = new Dictionary<string, int>();

        if (!_playerData.Resources.ContainsKey(name))
        {
            Log.Warn($"Resource \"{name}\" doesn't exist in player's data dictionary.");
            return 0;
        }

        return _playerData.Resources[name];
    }

    public bool UseResource(string name, int count, bool doNotSave = false)
    {
        if (count <= 0)
        {
            Log.Warn($"Resource \"{name}\": Cannot use 0 or negative resource.");
            return true;
        }

        if (!_playerData.Resources.ContainsKey(name))
        {
            Log.Warn($"Resource \"{name}\" doesn't exist in player's data dictionary.");
            return false;
        }

        if (_playerData.Resources[name] < count)
        {
            Log.Info($"Resource \"{name}\" is not enough to use (use: {count}) is not used (still is {_playerData.Resources[name]}).");
            return false;
        }

        _playerData.Resources[name] -= count;
        Log.Info($"Resource \"{name}\" is decreased by {count}, {_playerData.Resources[name]} remaining.");

        if (!doNotSave)
        {
            RequestSaveData(true, false);
        }

        return true;
    }

    public bool AddResource(string name, int count, bool allowCreateEntry = false, bool doNotSave = false)
    {
        if (count <= 0)
        {
            Log.Warn($"Resource \"{name}\": Cannot add 0 or negative resource.");
            return true;
        }

        if (!_playerData.Resources.ContainsKey(name) && !allowCreateEntry)
        {
            Log.Warn($"Resource \"{name}\" doesn't exist in player's data dictionary and the addition call" +
                     $"does not allow creation of this resource.");
            return false;
        }
        else if (!_playerData.Resources.ContainsKey(name))
        {
            Log.Info($"Resource \"{name}\" is created in player's data dictionary.");
            _playerData.Resources.Add(name, 0);
        }

        _playerData.Resources[name] += count;
        Log.Info($"Resource \"{name}\" is increased by {count}, now having {_playerData.Resources[name]}.");

        if (!doNotSave) RequestSaveData(true, false);

        return true;
    }

    public void SetPreference(PlayerPreference newPreference)
    {
        _preference = new PlayerPreference(newPreference);
        RequestSaveData(false, true);
    }

    public PlayerPreference GetPreference()
    {
        return new PlayerPreference(_preference);
    }
}