using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int _mana;
    public CharaControl _character;
    public GameObject _destination;
    public bool _canMoveCharacter;


    public float speed = 2f;
    public GameObject _pawnPrefab;
    public float t = 0f;
    PiecesController _piecesController;

    public Transform _spawnPoint;
    public Transform _enemySpawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _piecesController = GetComponent<PiecesController>();

        _spawnPoint.position = new Vector3((int)Random.Range(-10, 10), 0, (int)Random.Range(-30, -20));
        _enemySpawnPoint.position = new Vector3((int)Random.Range(-10, 10), 0, (int)Random.Range(30, 20));

        Instantiate(_pawnPrefab, this.transform.position, Quaternion.identity);
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
