using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteroidSpawner : MonoBehaviour
{

    [SerializeField] float timeBetweenSpawns = 3f;
    [SerializeField] GameObject meteroid;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoSpawnMeteroids(timeBetweenSpawns));
    }
    
    public IEnumerator CoSpawnMeteroids(float timeBetweenSpawns)
    {
        while (true)
        {
            Instantiate(meteroid, transform.position, Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0, 360f)));
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        
    }
}
