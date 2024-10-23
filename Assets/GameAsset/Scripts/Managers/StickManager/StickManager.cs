using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StickManager
{
    [Header("Components")]
    [SerializeField]
    private List<Stick> sticks;

    private bool isWin;

    public static StickManager Instance;

    private void Awake()
    {
        Instance = this;
        sticks = new List<Stick>();
        isWin = false;
    }

    private void Start()
    {
        sticks.AddRange(GetComponentsInChildren<Stick>());
    }

    public void checkAllStick()
    {
        if (sticks.Count > 0)
        {
            bool status = true;

            sticks.ForEach(stick =>
            {
                if (!stick.getStatus())
                {
                    status = false;
                }
            });

            isWin = status;

            if (isWin)
            {
                Win();
            }
        }
    }

    private void Win()
    {
        Debug.Log("Win");
        gameObject.SetActive(false);
        GM.Instance.MainGame.RequestEndGame(isWin);
        //GM.Instance.MainGame.startNextLevel();
    }

}
