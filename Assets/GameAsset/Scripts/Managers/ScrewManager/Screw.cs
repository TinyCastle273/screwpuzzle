using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum ScrewState
{
    IDLE = 0,
    SELECTED,
    MOVING_TO_HOLE
}

public class Screw : MonoBehaviour
{
    private ScrewState state;

    private readonly float duration = 0.05f;

    private float elapseTime = 0f;

    private Vector3 startPosition;

    private Vector3 endPosition;

    private Image image;
    private Sprite idleSprite;
    private Sprite selectedSprite;

    [SerializeField]
    private List<CircleCollider2D> circleColliders = new List<CircleCollider2D>();

    private void Start()
    {
        state = ScrewState.IDLE;
        image = GetComponent<Image>();
        idleSprite = image.sprite;
        selectedSprite = GM.Instance.MainGame.selectedScrew;

        circleColliders = transform.GetComponents<CircleCollider2D>().ToList();
    }

    private void Update()
    {
        if (state == ScrewState.SELECTED)
        {
            RotateScrew();
        }

        if (state == ScrewState.MOVING_TO_HOLE)
        {
            moveScrew();
        }

    }

    public GameObject getScrew() => gameObject;

    public bool isScrewSelected() => state == ScrewState.SELECTED;

    public void screwSelected()
    {
        state = ScrewState.SELECTED;
        image.sprite = selectedSprite;
    }

    public void screwDeselected()
    {
        state = ScrewState.IDLE;
        image.sprite = idleSprite;
    }

    public void stay()
    {
        screwDeselected();

        // TODO: animation stuff
    }

    public void RotateScrew()
    {
        transform.Rotate(0, 0, -100 * Time.deltaTime);
    }

    public void setEndPosition(Hole hole)
    {
        startPosition = transform.position;
        endPosition = hole.transform.position;

        endPosition.z = -1;

        state = ScrewState.MOVING_TO_HOLE;
        elapseTime = 0;
    }

    private void moveScrew()
    {
        circleColliders.ForEach(collider => collider.enabled = false);

        // Calculate t based on elapsed time
        elapseTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapseTime / duration);
        float a = Mathf.Round(t);

        // Apply the custom smoothing formula
        float smoothFactor = 4 * Mathf.Pow(t, 3) * (1 - a) + (1 - 4 * Mathf.Pow(1 - t, 3)) * a;

        // Interpolate between start and target positions
        transform.position = Vector3.Lerp(startPosition, endPosition, smoothFactor);

        // Check if movement is complete
        if (t >= 1.0f)
        {
            screwDeselected();
            elapseTime = 0f;
            circleColliders.ForEach(collider => collider.enabled = true);
        }

    }

}
