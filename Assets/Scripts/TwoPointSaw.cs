using UnityEngine;

public class TwoPointSaw : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 1f;

    private float t = 0f;
    private int direction = 1;

    void Update()
    {
        t += Time.deltaTime * speed * direction;

        if (t >= 1f)
        {
            t = 1f;
            direction = -1;
        }
        else if (t <= 0f)
        {
            t = 0f;
            direction = 1;
        }

        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
    }
}