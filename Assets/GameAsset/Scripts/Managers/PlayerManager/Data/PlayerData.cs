using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public static class GlobalConstants
{
    public const string COINS_RESOURCE = "coins";
}

[Serializable]
public class CompletedLevelData
{
    public CompletedLevelData()
    {
        Completed = false;
        BestTime = 0;
    }

    public CompletedLevelData(bool completed, int bestTime)
    {
        Completed = completed;
        BestTime = bestTime;
    }

    public bool Completed { get; set; }
    public int BestTime { get; set; }
}

[Serializable]
public class PlayerData
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, CompletedLevelData> CompletedLevels { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, int> Resources { get; set; }

    public DateTime LastSaveTime { get; set; }

    [JsonConstructor]
    public PlayerData()
    {
        CompletedLevels = new Dictionary<string, CompletedLevelData>();

        Resources = new Dictionary<string, int>()
            {
                { GlobalConstants.COINS_RESOURCE, 0 }
            };

        LastSaveTime = DateTime.UtcNow;
    }

}