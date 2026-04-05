using UnityEngine;
using UnityEngine.InputSystem;

public class InteractibleScript : MonoBehaviour
{
    public Transform player;
    public float range = 3f;
    
    public GameObject _pressA;
    public bool _givesItem;
    public SO_Item item;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= range)
        {
            if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
               
                Interact();

            }
            else
            {

            }
            _pressA.SetActive(true);
           
        }
        else
        {
            _pressA.SetActive(false);
            
        }
    }

    void Interact()
    {
        if (_givesItem)
        {
            item._quantity += 1;
        }
    }
}
