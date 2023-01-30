using UnityEngine;
using Random = UnityEngine.Random;


public class AcornFactory : MonoBehaviour
{
    private static AcornFactory _instance;
    public static AcornFactory Instance => _instance;
    
    [SerializeField] private Acorn acornPrefab;

    private void Awake()
    {
        if(_instance != null)
            Destroy(this);
        else
            _instance = this;
    }

    public Acorn CreateAcorn(Vector3 positionWorld)
    {
        var acorn = Instantiate(acornPrefab,positionWorld,Quaternion.identity);
        acorn.Color = GameManager.Colors[Random.Range(0,GameManager.Colors.Length)];
        return acorn;
    }
}
