using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;

public partial class ScrewManager
{
    public bool testing;

    private bool clicked = false;

    private Hole selectedHole;
    private GameObject currentScrew;

    private void Update()
    {
        if (testing)
        {
            if (!clicked)
            {
                OnPlayerClicked();
            }

            checkWin();
        }
        else
        {
            if (GM.Instance.MainGame.GameState == GameState.PLAYING)
            {
                if (!clicked)
                {
                    OnPlayerClicked();
                }

                checkWin();
            }
        }
    }

    private void checkWin()
    {
        if (!testing)
        {
            StickManager.Instance.checkAllStick();
        }
    }

    private void OnPlayerClicked()
    {


#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {

                Vector2 ray = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

                inputHandle(hit);
            }
        }
#elif UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            int layerMask = ~LayerMask.GetMask("Ignore Raycast");

            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, layerMask);

            inputHandle(hit);
        }
#endif
    }

    private void inputHandle(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            // Log
            Log.Info($"{hit.collider.name}");

            Screw screw = hit.collider.GetComponent<Screw>();
            Hole hole = hit.collider.GetComponent<Hole>();
            Stick stick = hit.collider.GetComponent<Stick>();

            // Just found stick
            if (stick is not null)
            {
                return;
            }

            // Found Stick, selected Screw
            if (stick is not null && currentScrew is not null)
            {
                Log.Info("Found stick, reset state");

                // Reset screw state
                Screw selectedScrew = currentScrew.GetComponent<Screw>();
                selectedScrew.stay();

                currentScrew = null;
                return;
            }

            // Found screw
            if (screw is not null && currentScrew is null)
            {
                Log.Info($"Found Screw: {screw}");

                // Save screw
                currentScrew = screw.getScrew();
                //selectedHole = currentScrew.transform.GetComponentInParent<Hole>();
                screw.screwSelected();

                return;
            }

            // Found screw, already has screw selected
            if (screw != null && currentScrew != null)
            {
                Log.Info("Invalid hole, selected hole already busy");

                // Reset screw state
                /// temporary
                screw.stay();
                currentScrew.GetComponent<Screw>().stay();
                currentScrew = null;

                return;
            }

            // Found hole, selected screw, move screw
            if (hole is not null && currentScrew is not null)
            {
                if (hole.GetScrewInSlot() is not null)
                {
                    Log.Info("Selected hole already has screw");

                    // Reset screw state
                    Screw selectedScrew = currentScrew.GetComponent<Screw>();
                    selectedScrew.stay();

                    currentScrew = null;
                }
                else
                {
                    Log.Info($"Found empty hole, move screw {screw} to hole {hole}");
                    // Temporary
                    currentScrew.GetComponent<Screw>().setEndPosition(hole);
                    //currentScrew.transform.position = hole.transform.position;
                    //currentScrew.transform.SetParent(hole.transform);
                    hole.SetScrewToSlot(currentScrew);

                    // Clear save
                    currentScrew = null;
                    if (selectedHole is not null)
                        selectedHole.ClearSlot();

                    // Save hole
                    selectedHole = hole;
                }

                return;
            }

            // Found hole, no screw selected
            if (hole is not null && currentScrew is null)
            {
                if (hole.GetScrewInSlot() is not null)
                {
                    Log.Info($"Found hole, hole has screw");

                    // Save screw
                    currentScrew = hole.GetScrewInSlot();
                    screw = currentScrew.GetComponent<Screw>();
                    screw.screwSelected();
                    selectedHole = hole;
                }
                else
                {
                    Log.Info("Hole is empty");
                    // Play SFX

                }

                return;
            }

        }
        else
        {
            Log.Warn("Hit nothing");

            // Invalid hole selected
            if (currentScrew != null)
            {
                Log.Info("Keep selelected screw idle");

                // Reset screw state
                Screw screw = currentScrew.GetComponent<Screw>();
                screw.stay();

                currentScrew = null;
            }
        }
    }
}
