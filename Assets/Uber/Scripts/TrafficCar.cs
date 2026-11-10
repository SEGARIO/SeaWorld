
using UnityEngine;

public class TrafficCar : MonoBehaviour
{
    [Header("References")]
    public TrafficPath path;

    [Header("Movement")]
    public float speed = 8f;
    public float acceleration = 3f;
    public float rotationSpeed = 5f;
    public float waypointDistance = 3f;

    [Header("Cornering")]
    public float cornerSlowdown = 0.5f;
    public float cornerAngle = 45f;

    private int currentWaypoint;
    private float currentSpeed;

    void Start()
    {
        currentSpeed = 0f;
    }

    void Update()
    {
        if (path == null || path.waypoints.Length == 0)
            return;

        Transform target = path.waypoints[currentWaypoint];

        if (target == null)
            return;

        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        // Check waypoint distance

        if (direction.magnitude < waypointDistance)
        {
            currentWaypoint++;

            if (currentWaypoint >= path.waypoints.Length)
                currentWaypoint = 0;

            return;
        }

        direction.Normalize();

        // Calculate corner speed

        float angle = Vector3.Angle(
            transform.forward,
            direction
        );

        float targetSpeed = speed;

        if (angle > cornerAngle)
        {
            targetSpeed *= cornerSlowdown;
        }

        // Smooth acceleration

        currentSpeed = Mathf.MoveTowards(
            currentSpeed,
            targetSpeed,
            acceleration * Time.deltaTime
        );

        // Smooth rotation

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        // Move forward

        transform.position +=
            transform.forward * currentSpeed * Time.deltaTime;
    }
}
