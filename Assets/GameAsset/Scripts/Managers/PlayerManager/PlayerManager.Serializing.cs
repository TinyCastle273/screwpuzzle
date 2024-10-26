using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;


public partial class PlayerManager
{
    private const string PLAYER_DATA_PATH = "player";
    private const string PREFERENCE_KEY = "preference";

    private string PlayerPath => Path.Combine(Application.persistentDataPath, PLAYER_DATA_PATH);

    private bool _loadFlag = false;
    private bool _saveFlagPlayer = false;
    private bool _saveFlagPref = false;

    public void RequestSaveData(
        bool savePlayer = true,
        bool savePref = true)
    {
        if (savePlayer) SavePlayerData();
        if (savePref) SavePreferenceData();
    }

    private void SavePlayerData()
    {
        if (_saveFlagPlayer)
        {
            Log.Warn("A Player Data save request is active, Cannot request another save.");
            return;
        }

        _saveFlagPlayer = true;
        Log.Info("Player Data save request started.");
        var task = WriteToDisk(PlayerPath, _playerData);
        task.ContinueWith(t =>
        {
            _saveFlagPlayer = false;
            Log.Info("Player Data save request completed.");
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private void SavePreferenceData()
    {
        if (_saveFlagPref)
        {
            Log.Warn("Preference save request is active, Cannot request another save.");
            return;
        }

        _saveFlagPref = true;
        Log.Info("Preference save request started.");
        var task = WriteToPref(PREFERENCE_KEY, _preference);
        task.ContinueWith(t =>
        {
            _saveFlagPref = false;
            Log.Info("Preference save request completed.");
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private async Task<(bool found, T data)> ReadFromDisk<T>(string path) where T : new()
    {
        if (!File.Exists(path))
        {
            Log.Info($"File at \"{path}\" does not exist, new data will be generated.");
            return (false, new T());
        }

        var json = await File.ReadAllTextAsync(path);
        return await Task.Run(() =>
        {
            try
            {
                var data = JsonConvert.DeserializeObject<T>(json);
                return (data != null, data != null ? data : new T()); ;
            }
            catch (Exception e)
            {
                Log.Warn(e.Message);
                Log.Warn(e.StackTrace);
                Log.Info("Player data exists but cannot be deserialized.");
            }

            return (false, new T());
        });
    }

    private async Task<(bool found, T data)> ReadFromPref<T>(string key) where T : new()
    {
        var str = PlayerPrefs.GetString(key, null);
        return await Task.Run(() =>
        {
            T data;
            if (str != null || str == "")
            {
                try
                {
                    data = JsonConvert.DeserializeObject<T>(str);
                    return (data != null, data != null ? data : new T());
                }
                catch (Exception e)
                {
                    Log.Warn(e.Message);
                    Log.Warn($"{typeof(T)} data exists on \"{key}\" in PlayerPrefs but cannot be deserialized.");
                }
            }
            else
            {
                Log.Info($"{typeof(T)} data is not found on \"{key}\" in PlayerPrefs. Will return default");
            }

            data = new T();
            return (false, data);
        });
    }

    private async Task<bool> WriteToPref<T>(string key, T serializableData)
    {
        try
        {
            var json = await Task.Run(() => JsonConvert.SerializeObject(serializableData));
            PlayerPrefs.SetString(key, json);
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
            Log.Warn($"Write content in key ${key} is aborted.");
            return false;
        }

        return true;
    }

    private async Task<bool> WriteToDisk<T>(string path, T serializableData)
    {
        try
        {
            var json = await Task.Run(() => JsonConvert.SerializeObject(serializableData, Formatting.Indented));

            if (File.Exists(path))
            {
                Log.Info($"File at \"{path}\" exists. Will be over-written.");
            }

            await File.WriteAllTextAsync(path, json);
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
            Log.Warn($"Write content at path \"{path}\" is aborted.");
            return false;
        }

        return true;
    }

    private static void ConvertVersion1(out PlayerData newData)
    {
        newData = new PlayerData();
        newData.Resources[GlobalConstants.COINS_RESOURCE] = 0;
    }
}