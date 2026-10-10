using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ChessBlock : MonoBehaviour
{
    public Renderer[] _rend;
    public Material[] _mats;
    public Material _overMat;
    public Material _clickMat;
    public UnityEvent onMouseEnter;
    public UnityEvent onMouseExit;
    public UnityEvent onMouseClick;
    public GameObject _target;

    private void Start()
    {
        for (int i = 0; i < _rend.Length; i++)
        {
          _mats[i] =  _rend[i].material;
        }
    }


    private void OnMouseEnter()
    {
        Debug.Log("Enter");
        onMouseEnter?.Invoke();
        for (int i = 0; i < _rend.Length; i++)
        {
            _rend[i].material = _overMat;
        }
        

    }
    private void OnMouseExit()
    {
        onMouseExit?.Invoke();

        for (int i = 0; i < _rend.Length; i++)
        {
            _rend[i].material = _mats[i];
        }
     
    }

    private void OnMouseDown()
    {
        onMouseClick?.Invoke();

        for (int i = 0; i < _rend.Length; i++)
        {
            _rend[i].material = _clickMat;
        }
        _target.transform.position = this.transform.position;
    }
}
