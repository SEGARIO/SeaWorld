using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int _mana;
    public CharaControl _character;
    public GameObject _destination;
    public bool _canMoveCharacter;


    public float speed = 2f;

    public float t = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_canMoveCharacter)
        {
            t += Time.deltaTime * speed;

            _character.transform.position = Vector3.Lerp(
                _character.transform.position,
                _destination.transform.position,
                t
            );
        }
    }
}
