
using UnityEngine;

public class TrafficPath : MonoBehaviour
{
    public Transform[] waypoints;

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length < 2)
            return;

        Gizmos.color = Color.cyan;

        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] == null) continue;

            Gizmos.DrawSphere(waypoints[i].position, 0.3f);

            int next = (i + 1) % waypoints.Length;

            if (waypoints[next] != null)
            {
                Gizmos.DrawLine(
                    waypoints[i].position,
                    waypoints[next].position
                );
            }
        }
    }
}
