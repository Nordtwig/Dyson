using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteroidSpawner : MonoBehaviour
{

    [SerializeField] float timeBetweenSpawns = 3f;
    private GameObject meteoroid;
    private GameObject newMeteoroid;
    private Transform meteoroids;
    
    // Start is called before the first frame update
    void Start()
    {
        meteoroids = GameObject.Find("Meteoroids").transform;
        meteoroid = FindObjectOfType<Meteroid>().transform.parent.gameObject;
        meteoroid.SetActive(false);
        StartCoroutine(CoSpawnMeteroids(timeBetweenSpawns));
    }
    
    public IEnumerator CoSpawnMeteroids(float timeBetweenSpawns)
    {
        while (true)
        {
            newMeteoroid = Instantiate(meteoroid, transform.position, Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0, 360f)), meteoroids);
            newMeteoroid.SetActive(true);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
}
