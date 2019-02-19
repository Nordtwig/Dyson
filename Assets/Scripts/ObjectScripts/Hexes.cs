using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexes : MonoBehaviour
{
    public GameObject Phase2Hexes;
    public GameObject Phase3Hexes;
    public GameObject Phase4Hexes;
    public GameObject Phase5Hexes;

    void Start()
    {
        Phase2Hexes.SetActive(false);
        Phase3Hexes.SetActive(false);
        Phase4Hexes.SetActive(false);
        Phase5Hexes.SetActive(false);
    }

    public void UpdateHexes()
    {
        if (GameController.instance.currentPhase == 2)
        {
            Phase2Hexes.SetActive(true);
        }

        if (GameController.instance.currentPhase == 3)
        {
            Phase3Hexes.SetActive(true);
        }

        if (GameController.instance.currentPhase == 4)
        {
            Phase4Hexes.SetActive(true);
        }

        if (GameController.instance.currentPhase == 5)
        {
            Phase5Hexes.SetActive(true);
        }
    }
}
