using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script creator Robin
/// </summary>

public class InputController : MonoBehaviour
{
    //SINGLETON
    public static InputController instance;

    PlayerController player;
    Text helpText;
    private bool jumpButtonDown = false;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }

        else
        {
            if (!instance)
            {
                instance = this;
                DontDestroyOnLoad(instance);

                instance.enabled = true;
            }
        }
    }

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        helpText = GameObject.Find("HelpText").GetComponent<Text>();
        helpText.enabled = false;
    }

    // ===================== Short about ======================================
    // press F1 to enable disable DebugMode, disabled by default
    // Uses (R)estart, (N)odeSpawn, (M)iningRigSpawn, (B)oxSpawn

    public void CheckKeys()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameController.instance.SetDebugMode(!GameController.instance.GetDebugMode()); //inverts the value of DebugMode
            Debug.Log(GameController.instance.GetDebugMode());
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            helpText.enabled = !helpText.enabled;
        }

        CheckAndRunPlayerKeys();

        CheckAndRunDebugKeys();
    }

    private void CheckAndRunPlayerKeys()
    {
        player.PlayerMove(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetButtonDown("Jump"))
        {
            player.PlayerJump();
            jumpButtonDown = true;
        }

        if (Input.GetButton("Jump") && jumpButtonDown)
        {
            player.ContinuedJump();
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumpButtonDown = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            player.PlayerInteraction();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            player.ThrowItem();
        }

    }

    //Checks if any debug key has been pressed and executes that command is DebugMode is true
    private void CheckAndRunDebugKeys()
    {
        if (GameController.instance.GetDebugMode())
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(GameController.instance.CoRestart());
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
