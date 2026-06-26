using System;
using UnityEngine;

public class LuminousStones : MonoBehaviour
{
    public int[] _officialOrder;
    public int[] _givenOrder;
    public Stone[] _stones;
    public int _index;
    public int _correct;
    public bool[] _canAdd;
    bool _isCompleted;
    public ParticleSystem _system;
    private void Start()
    {
        for (int i = 0; i < _givenOrder.Length; i++)
        {
            _canAdd[i] = true;
        }
    }
    private void Update()
    {
        if(_isCompleted)
        {
            Debug.Log("completed");
           
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - Time.deltaTime * 2.5f, this.transform.position.z);

        }
        if (_index >= 4)
        {
            for (int i = 0; i < _givenOrder.Length; i++)
            {
                if (_givenOrder[i] == _officialOrder[i])
                {
                    if (_canAdd[i])
                    {
                        _correct += 1;
                        _canAdd[i] = false;
                    }
                    
                   
                }
                else
                {
                    Invoke("Resetter", 1);
                }
            }

            if(_correct == 4)
            {
               
                Invoke("Completed", 1);
                
            }
        }
    }

    public void Activate(int indexer)
    {
        _index += 1;
        _givenOrder[_index - 1] = indexer;
       
    }

    void Resetter()
    {
        for(int i = 0; i < _stones.Length; i++) 
        {
            _stones[i]._isLuminous = false;
            _index = 0;
            _givenOrder[i] = 0;
            _correct = 0;
        }
        for (int i = 0; i < _givenOrder.Length; i++)
        {
            _canAdd[i] = true;
        }
    }

    void Completed()
    {
        _system.Play();
        for (int i = 0; i < _givenOrder.Length; i++)
        {
            _canAdd[i] = true;

        }
        GameFeel.Instance.PlayJuice(2, 0.5f);
        Invoke("Destroyer", 3f);
        _isCompleted = true;
         }

    void Destroyer()
    {
        Destroy(this.gameObject);
    }
}
