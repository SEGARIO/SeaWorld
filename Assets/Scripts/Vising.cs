using UnityEngine;

public class Vising : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private LayerMask planeLayer;
    [SerializeField] private float maxDistance = 10f;
    public Animator _anim;
    bool _isVising = true;

    void Update()
    {
        if(_isVising)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, planeLayer))
            {
                objectToMove.transform.position = hit.point;
            }
        }
       
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            _isVising = false;
            _anim.SetTrigger("Drop");
        }


    }


}