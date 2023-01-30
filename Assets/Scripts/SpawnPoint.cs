using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Acorn _currentAcorn;
    private void Start()
    {
        SpawnNewAcorn();
    }

    private void SpawnNewAcorn()
    {
        _currentAcorn = AcornFactory.Instance.CreateAcorn(transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Acorn>() == _currentAcorn)
            SpawnNewAcorn();
    }
}
