using System.Net;
using UnityEngine;

public class RaycastVis : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _viser;
    [SerializeField] private Transform _viserVisual;
    [SerializeField] private LayerMask _layerMask;


    public bool HasHit { get; private set; }
    public Vector3 HitPoint { get; private set; }
    Vector3 endPoint;

    private void Update()
    {
        Vector3 direction = (_viser.position - _player.position).normalized;
        
        float distance = Vector3.Distance(_player.position, _viser.position);

        if (Physics.Raycast(_player.position, direction, out RaycastHit hit, distance, _layerMask))
        {
            HasHit = true;
            HitPoint = hit.point;
            _viserVisual.transform.position = hit.point;
            Debug.Log("Touché : " + hit.collider.name);
        }
        else
        {
            endPoint = _player.position + direction * distance;
            HasHit = false;
            _viserVisual.transform.position = endPoint;
        }
    }

    private void OnDrawGizmos()
    {
        if (_player == null || _viser == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(_player.position, _viser.position);

        if (HasHit)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(HitPoint, 0.1f);
        }
    }
}