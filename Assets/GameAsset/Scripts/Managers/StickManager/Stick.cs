using System;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField]
    private bool isDestroyed;

    private void Start()
    {
        isDestroyed = false;
    }

    private void Update()
    {
        if (!isDestroyed)
        {
            float gY = gameObject.transform.position.y;

            if (gY > 50)
            {
                isDestroyed = true;
                gameObject.SetActive(false);
            }
        }
    }

    public bool getStatus()
    {
        return isDestroyed;
    }

}