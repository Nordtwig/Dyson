using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexes : MonoBehaviour
{
    public GameObject Phase2Hexes;
    // Start is called before the first frame update
    void Start()
    {
        Phase2Hexes.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<GameController>().currentPhase == 2)
        {
            Phase2Hexes.SetActive(true);
        }
    }
}
