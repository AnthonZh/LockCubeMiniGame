using UnityEngine;

public class SawScript : MonoBehaviour
{
    public Transform[] Transforms = new Transform[4];

    public float speed = 3f;
    public float waypointThreshold = 0.2f;

    private int currentWaypoint = 0;

    // Update is called once per frame
    void Update()
    {
        Transform target = Transforms[currentWaypoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) <= waypointThreshold)
        {
            currentWaypoint = (currentWaypoint + 1) % Transforms.Length;
        }
    }
}
