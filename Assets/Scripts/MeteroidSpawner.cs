using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteroidSpawner : MonoBehaviour
{
    private GameObject meteoroid;
    private GameObject newMeteoroid;
    private Transform meteoroids;
    
    // Start is called before the first frame update
    void Start()
    {
        meteoroids = GameObject.Find("Meteoroids").transform;
        meteoroid = FindObjectOfType<Meteroid>().transform.parent.gameObject;
        meteoroid.SetActive(false);
    }
    
    public IEnumerator CoSpawnMeteroids(float timeBetweenSpawns)
    {
        while (true)
        {
            newMeteoroid = Instantiate(meteoroid, transform.position, Random.rotation, meteoroids);
            newMeteoroid.SetActive(true);
            yield return new WaitForSeconds(Random.Range(timeBetweenSpawns/2, timeBetweenSpawns*2));
        }
    }
}
