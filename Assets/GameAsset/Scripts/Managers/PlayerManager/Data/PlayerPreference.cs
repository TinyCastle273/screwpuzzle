using Newtonsoft.Json;
using System;

[Serializable]
public class PlayerPreference
{
    public int MusicVolume { get; set; }
    public int SfxVolume { get; set; }
    public bool Vibration { get; set; }
    public bool Notification { get; set; }

    [JsonConstructor]
    public PlayerPreference()
    {
        MusicVolume = 50;
        SfxVolume = 50;
        Vibration = true;
        Notification = true;
    }

    public PlayerPreference(PlayerPreference other) : this()
    {
        if (other != null)
        {
            MusicVolume = other.MusicVolume;
            SfxVolume = other.SfxVolume;
            Vibration = other.Vibration;
            Notification = other.Notification;
        }
    }
}
