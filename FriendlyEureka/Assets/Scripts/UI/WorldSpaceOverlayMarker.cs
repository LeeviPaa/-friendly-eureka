using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceOverlayMarker : MonoBehaviour
{
    public Sprite visibleIcon;
    public Sprite edgeIcon;
    public Image image;

    [System.NonSerialized]
    public WorldSpaceOverlayMarkerTracker tracker;
    [System.NonSerialized]
    new public RectTransform transform;
    [System.NonSerialized]
    public RectTransform imageTransform;

    private bool isVisible;
    private SixDirections currentDir = SixDirections.Up;
    private bool firstCheck = true;

    private void Awake() {
        transform = GetComponent<RectTransform>();
        imageTransform = image.GetComponent<RectTransform>();
    }

    public void SetVisibleStatus() {
        if (!isVisible || firstCheck) {
            image.sprite = visibleIcon;
            isVisible = true;
        }
        firstCheck = false;
    }

    public void SetEdgeStatus(SixDirections dir) {
        if (isVisible || firstCheck) {
            image.sprite = edgeIcon;
            isVisible = false;
        }
        if (currentDir != dir || firstCheck) {
            switch (dir) {
                case SixDirections.Left:
                    imageTransform.rotation = Quaternion.Euler(0f, 0f, 90f);
                    break;
                case SixDirections.Right:
                    imageTransform.rotation = Quaternion.Euler(0f, 0f, -90f);
                    break;
                case SixDirections.Up:
                    imageTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    break;
                case SixDirections.Down:
                    imageTransform.rotation = Quaternion.Euler(0f, 0f, 180f);
                    break;
                default:
                    break;
            }
            currentDir = dir;
        }
        firstCheck = false;
    }
}
