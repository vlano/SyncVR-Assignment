using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Acorn : MonoBehaviour
{
    private Color _color;
    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            Material.color = _color;
        }
    }

    private Material _material;

    private Material Material
    {
        get
        {
            if (_material == null)
                _material = GetComponent<Renderer>().material;
            return _material;
        }
        set => _material = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            StartCoroutine(DestroyAfterSeconds(5));
    }

    private IEnumerator DestroyAfterSeconds(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Destroy(gameObject);
    }
}
