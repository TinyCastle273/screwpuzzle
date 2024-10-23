using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField]
    private GameObject slot;

    private CircleCollider2D col;

    private int originalLayer;
    public string ignoreRaycastLayer = "Ignore Raycast";

    private void Start()
    {
        col = GetComponent<CircleCollider2D>();

        Screw screw = transform.GetComponentInChildren<Screw>();
        if (screw != null)
        {
            slot = screw.gameObject;
        }
        else
        {
            slot = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if it overlaps with any object with a collider
        if (IsObstacle(other))
        {
            // Change the layer to Ignore Raycast layer when colliding
            gameObject.layer = LayerMask.NameToLayer(ignoreRaycastLayer);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // When the object stops overlapping, restore its original layer
        if (IsObstacle(other))
        {
            gameObject.layer = originalLayer;
        }
    }

    private bool IsObstacle(Collider2D collider)
    {
        // Check for specific layers
        return collider.gameObject.layer == LayerMask.NameToLayer("RedStick") ||
               collider.gameObject.layer == LayerMask.NameToLayer("PinkStick") ||
               collider.gameObject.layer == LayerMask.NameToLayer("BrownStick") ||
               collider.gameObject.layer == LayerMask.NameToLayer("GrayStick") ||
               collider.gameObject.layer == LayerMask.NameToLayer("BlueStick") ||
               collider.gameObject.layer == LayerMask.NameToLayer("GreenStick");
    }

    public GameObject GetScrewInSlot() => slot;

    public GameObject SetScrewToSlot(GameObject screw) => slot = screw;

    public void ClearSlot()
    {
        if (slot is null) return;
        slot = null;
    }
}
