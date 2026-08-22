using UnityEngine;

public class RoadShip : MonoBehaviour
{
    public float speed;
    public float limit;

    public Renderer[] _wingrends;
    public Renderer _bodyRend;

    public Material[] _wingsColors;
    public Material[] _bodyColors;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3(this.transform.localPosition.x + speed, this.transform.localPosition.y, this.transform.localPosition.z);

        if(this.transform.localPosition.x <= limit && speed < 0)
        {
            this.transform.localPosition = new Vector3(-limit, this.transform.localPosition.y, this.transform.localPosition.z);
            Material _nextMat = _wingsColors[Random.Range(0, _wingsColors.Length)];

            for (int i = 0; i < _wingrends.Length; i++)
            {
                _wingrends[i].material = _nextMat;
            }
            _bodyRend.material = _bodyColors[Random.Range(0, _bodyColors.Length)];
        }
        if (this.transform.localPosition.x >= limit && speed >0)
        {
            this.transform.localPosition = new Vector3(-limit, this.transform.localPosition.y, this.transform.localPosition.z);
            Material _nextMat = _wingsColors[Random.Range(0, _wingsColors.Length)];

            for (int i = 0; i < _wingrends.Length; i++)
            {
                _wingrends[i].material = _nextMat;
            }
            _bodyRend.material = _bodyColors[Random.Range(0, _bodyColors.Length)];
        }
    }
}
