using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // ===================== Short about ======================================
    // press F1 to enable disable DebugMode, disabled by default
    // Uses (R)estart, (N)odeSpawn, (M)iningRigSpawn, (B)oxSpawn

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameController.instance.SetDebugMode(!GameController.instance.GetDebugMode()); //inverts the value of DebugMode
            Debug.Log(GameController.instance.GetDebugMode());
        }

        CheckAndRunDebugKeys();
    }

    //Checks if any debug key has been pressed and executes that command is DebugMode is true
    private void CheckAndRunDebugKeys()
    {
        if (GameController.instance.GetDebugMode())
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameController.instance.Restart();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                GameController.instance.DebugSpawnNode();
                Debug.Log("spawn node");
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                GameController.instance.DebugSpawnMiningRig();
                Debug.Log("spawn miningrig");
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                GameController.instance.DebugSpawnBox();
                Debug.Log("spawn box");
            }
        }
    }
}
