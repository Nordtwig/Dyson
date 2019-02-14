using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexes : MonoBehaviour
{
    public GameObject Phase2Hexes;
    public GameObject Phase3Hexes;
    public GameObject Phase4Hexes;

    void Start()
    {
        Phase2Hexes.SetActive(false);
        Phase3Hexes.SetActive(false);
        Phase4Hexes.SetActive(false);
    }

    void Update()
    {
        if (GetComponent<GameController>().currentPhase == 2)
        {
            Phase2Hexes.SetActive(true);
        }

        if (GetComponent<GameController>().currentPhase == 3)
        {
            Phase2Hexes.SetActive(true);
            Phase3Hexes.SetActive(true);
        }

        if (GetComponent<GameController>().currentPhase == 4)
        {
            Phase2Hexes.SetActive(true);
            Phase3Hexes.SetActive(true);
            Phase4Hexes.SetActive(true);
        }
    }
}
