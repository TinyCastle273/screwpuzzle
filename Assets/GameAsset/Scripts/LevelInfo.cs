using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo")]
public class LevelInfo : ScriptableObject
{
    [SerializeField]
    private List<GameObject> levels;

    public GameObject GetLevel(int level)
    {

        if (level < levels.Count)
        {
            return levels[level];
        }
        else
        {
            return levels[levels.Count - 1];
        }
    }

    public int GetLevelsCount()
    {
        return levels.Count;
    }
}