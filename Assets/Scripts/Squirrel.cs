using UnityEngine;
using Random = UnityEngine.Random;

public class Squirrel : MonoBehaviour
{
    private Color _color;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = _color;
        ChangeColor();
    }
    

    private void ReceiveAcorn(Acorn acorn)
    {
        if (acorn.Color == _color)
            StashAcorn(acorn);
    }
    
    private void StashAcorn(Acorn acorn)
    {
        Destroy(acorn.gameObject);
        PointManager.AddPoints(GameManager._scoreValue);
        ChangeColor();
    }

    private void ChangeColor()
    {
        var newColor = GameManager.Colors[Random.Range(0,GameManager.Colors.Length)];
        _renderer.material.color = newColor;
        _color = newColor;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        var acorn = other.gameObject.GetComponent<Acorn>();
        ReceiveAcorn(acorn);
    }
}