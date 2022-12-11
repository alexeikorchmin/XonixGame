using UnityEngine;

public class SafeArea : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] rectTransforms;

    private void Awake()
    {
        ChangeSafeArea();
    }

    private void ChangeSafeArea()
    {
        var safeArea = Screen.safeArea;
        var anchorMin = safeArea.position;
        var anchorMax = anchorMin + safeArea.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        foreach(var rectTransform in rectTransforms)
        {
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }
    }
}