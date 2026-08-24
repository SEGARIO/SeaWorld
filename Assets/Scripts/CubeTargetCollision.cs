using System.Net;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CubeTargetCollision : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform _ship;
    [SerializeField] private Transform _target;
    [SerializeField] private LayerMask layerMask = ~0;

    public bool hasHit;
    public Vector3 hitPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (endPoint.position - startPoint.position).normalized;
        float distance = Vector3.Distance(startPoint.position, endPoint.position);

        if (Physics.Raycast(startPoint.position, direction, out RaycastHit hit, distance, layerMask))
        {
            hasHit = true;
            hitPoint = hit.point;
            if (hit.point.z > _ship.transform.position.z)
            {
                _target.transform.position = hit.point;
            }

            Debug.DrawLine(startPoint.position, hit.point, Color.red);
        }
        else
        {
            hasHit = false;
            hitPoint = endPoint.position;

            Debug.DrawLine(startPoint.position, endPoint.position, Color.green);
        }
    }
}
