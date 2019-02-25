using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Petter, modified by Heimer
/// </summary>

public class MeteroidSpawner : MonoBehaviour
{
    private GameObject meteoroid;
    private GameObject newMeteoroid;
    private Transform meteoroids;
    
    // Start is called before the first frame update
    void Awake()
    {
        meteoroids = GameObject.Find("Meteoroids").transform;
        meteoroid = FindObjectOfType<Meteroid>().transform.parent.gameObject;
        Debug.Log(meteoroids);
        Debug.Log(meteoroid);
        meteoroid.SetActive(false);
    }
    
    public IEnumerator CoSpawnMeteroids(float timeBetweenSpawns)
    {
        while (true)
        {
            if (GameController.instance.state == GameController.GameControllerState.GAME)
            {
                newMeteoroid = Instantiate(meteoroid, transform.position, Random.rotation, meteoroids);
                newMeteoroid.SetActive(true);
            }

            yield return new WaitForSeconds(Random.Range(timeBetweenSpawns/2, timeBetweenSpawns*2));
        }
    }
}
