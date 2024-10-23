using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHolder : MonoBehaviour
{
    public static LevelHolder Instance;

    private GameObject level;

    private void Awake()
    {
        Instance = this;
        level = new GameObject();
    }

    public GameObject createLevel(GameObject l)
    {
        level = GameObject.Instantiate(l);
        level.transform.SetParent(gameObject.transform, false);
        level.transform.localScale = new Vector3(1, 1, 1);
        level.transform.localPosition = Vector3.zero;
        level.SetActive(true);
        return level;
    }

    public void CloseFinishedLevel()
    {
        level.SetActive(false);
        DestroyImmediate(level);
        level = null;
    }

}
