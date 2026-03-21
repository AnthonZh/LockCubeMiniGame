using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraBounds : MonoBehaviour
{
    public Transform player;
    public Tilemap tilemap;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    public Camera cam;
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;

        if (tilemap != null)
        {
            Bounds b = tilemap.localBounds;
            minX = b.min.x;
            maxX = b.max.x;
            minY = b.min.y;
            maxY = b.max.y;
        }
    }

    void LateUpdate()
    {
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        transform.SetParent(null);
        Vector3 pos = player.position;
        
        pos.x += 1.25f;
        pos.y += 0.5f;
        pos.z = transform.position.z;

        pos.x = Mathf.Clamp(pos.x, minX + halfWidth,  maxX - halfWidth);
        pos.y = Mathf.Clamp(pos.y, minY + halfHeight, maxY - halfHeight);

        transform.position = pos;
    }
}
