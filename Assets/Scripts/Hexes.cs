using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexes : MonoBehaviour
{
    private GameController gameController;
    public GameObject Phase2Hexes;
    public GameObject Phase3Hexes;
    public GameObject Phase4Hexes;
    public GameObject Phase5Hexes;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        Phase2Hexes.SetActive(false);
        Phase3Hexes.SetActive(false);
        Phase4Hexes.SetActive(false);
        Phase5Hexes.SetActive(false);
    }

    void Update()
    {
        if (gameController.currentPhase == 2)
        {
            Phase2Hexes.SetActive(true);
        }

        if (gameController.currentPhase == 3)
        {
            Phase2Hexes.SetActive(true);
            Phase3Hexes.SetActive(true);
        }

        if (gameController.currentPhase == 4)
        {
            Phase2Hexes.SetActive(true);
            Phase3Hexes.SetActive(true);
            Phase4Hexes.SetActive(true);
        }

        if (gameController.currentPhase == 5)
        {
            Phase2Hexes.SetActive(true);
            Phase3Hexes.SetActive(true);
            Phase4Hexes.SetActive(true);
            Phase5Hexes.SetActive(true);
        }
    }
}
