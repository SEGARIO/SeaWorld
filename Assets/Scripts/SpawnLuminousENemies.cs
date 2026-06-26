using UnityEngine;

public class SpawnLuminousENemies : MonoBehaviour
{
    public GameObject[] _enemies;
    public GameObject[] _spawners;
    public int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Spawn()
    {
        if(index < _enemies.Length) {

            Instantiate(_enemies[index], _spawners[index].transform.position, Quaternion.identity);
            index += 1;
            Invoke("Spawn", 0.5f);
        }
      
    }
}
