using System;
using System.Collections.Generic;
using UnityEngine;

public class DefilingTexture : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private string _textureProperty = "_BaseMap"; // "_MainTex" si Built-in
    [SerializeField] private float _speed = 1f;

    public float _max = 1;
    public float _min = -0.28f;

    private Vector2 _offset;

    private void Start()
    {
        _offset = _material.GetTextureOffset(_textureProperty);
    }

    private void Update()
    {
        _offset.x += _speed * Time.deltaTime;

        if (_offset.x > _max)
            _offset.x = _min;

        _material.SetTextureOffset(_textureProperty, _offset);
    }
}
