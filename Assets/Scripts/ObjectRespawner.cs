using UnityEngine;

public class ObjectRespawner : MonoBehaviour
{
    public GameObject _player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("Respawn", Random.Range(5, 10));
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.1f);
    }

    void Respawn()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, _player.transform.position.z + 25);
        Invoke("Respawn", Random.Range(5, 10));
    }
}
