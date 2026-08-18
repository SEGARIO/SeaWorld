using UnityEngine;

public class PillarScript : MonoBehaviour
{
    public GameObject _pillar;
    public ParticleSystem _s;
    public GameObject _parent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        Destroy(_pillar);
        Invoke("Destroyer", 2);
        _s.Play();
        GameFeel.Instance.PlayJuice(3f, 0.6f);
    }

    void Destroyer()
    {
        Destroy(_parent);
    }

}
