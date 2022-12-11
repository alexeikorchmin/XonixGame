using UnityEngine;
using UnityEngine.Tilemaps;

public class BoundConverter : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    [SerializeField] private Tilemap tilemap;
    
    [SerializeField] private GameObject topLeft;
    [SerializeField] private GameObject bottomRight;

    private Plane plane;
    
    public Vector3Int GetTopLeftCornerPosition()
    {
        return GetTilePosition(topLeft);
    }

    public Vector3Int GetBottomRightCornerPosition()
    {
        return GetTilePosition(bottomRight);
    }
    
    private void Awake()
    {
        plane = new Plane(Vector3.back, Vector3.zero);
    }

    private Vector3Int GetTilePosition(GameObject go)
    {
        var ray = gameCamera.ScreenPointToRay(go.transform.position);
        plane.Raycast(ray, out var hitDist);
        var point = ray.GetPoint(hitDist);
        return tilemap.WorldToCell(point);
    }
}