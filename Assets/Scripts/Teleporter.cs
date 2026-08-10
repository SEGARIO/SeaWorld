using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public Transform _position;
    public GameObject player;
   public GameObject _cam;

    public GameObject _circleIn;
    public GameObject _circleOut;
    public PlayerController playerController;
    public CharacterController _ch;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Trigger touché par : " + other.name);
            Debug.Log("Référence player : " + player.name);
            playerController.enabled = false;
            _ch.enabled = false;
            _circleOut.SetActive(false);
            _circleIn.SetActive(true);
            //player = other.gameObject;
            Invoke("Teleport", 1);
        }
    }

    void Teleport()
    {
        
      
        _circleOut.SetActive(true);
        _circleIn.SetActive(false);
        player.transform.position = _position.position;
         _cam.gameObject.transform.position = _position.position;
        playerController.enabled = true;
        _ch.enabled = true;

    }
}
