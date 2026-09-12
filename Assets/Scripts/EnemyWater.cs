using UnityEngine;

public class EnemyWater : MonoBehaviour
{
    public GameObject _water;
    EnemyScript _script;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _script = GetComponent<EnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y < _water.transform.position.y)
        {
            _script._life = 0;
        }
    }
}
