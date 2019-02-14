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
	ThrowPowerBarScript throwBar;
    Text helpText;
    private GameObject storeWindow;
    private GameObject pauseMenuWindow;
    private bool jumpButtonDown = false;
    [HideInInspector] public float eTime = 0;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(instance);

            instance.enabled = true;
        }
    }

    public void StartUp()
    {
        player = FindObjectOfType<PlayerController>();
        helpText = GameObject.Find("HelpText").GetComponent<Text>();
		throwBar = FindObjectOfType<ThrowPowerBarScript>();
		helpText.enabled = false;
        storeWindow = FindObjectOfType<StoreWindowVisuals>().gameObject;
        storeWindow.SetActive(false);
        pauseMenuWindow = FindObjectOfType<PauseMenuWindowVisuals>().gameObject;
        pauseMenuWindow.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.P) &! (GameController.instance.state == GameController.GameControllerState.PAUSE))
        {
            storeWindow.SetActive(!storeWindow.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameController.instance.state == GameController.GameControllerState.STOREWINDOW)
            {
                storeWindow.SetActive(false);
            }
            else if (GameController.instance.state == GameController.GameControllerState.PAUSE)
            {
                pauseMenuWindow.SetActive(false);
            }
            else
            {
                pauseMenuWindow.SetActive(true);
            }
        }

        if (GameController.instance.state == GameController.GameControllerState.GAME)
        {
            CheckAndRunPlayerKeys();
            CheckAndRunDebugKeys();
        }

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

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (eTime < 0.5f)
            {
				throwBar.ThrowPowerBarUpdate(0, player.holdingItem);
                player.PlayerInteraction();
			}
            else
            {
				throwBar.ThrowPowerBarUpdate(0, player.holdingItem);
                player.ThrowItem(Mathf.Clamp(eTime, 0, 1.5f));
                ThrowPowerBarScript.instance.SetEnableThrowBackground(false);
            }
			eTime = 0;
        }
        
        if (Input.GetKey(KeyCode.E))
        {
            eTime += Time.deltaTime;

			if (eTime > 0.5f)
			{
				throwBar.ThrowPowerBarUpdate(Mathf.Clamp(eTime, 0, 1.5f), player.holdingItem);
			}
        }
        
    }

    public void CheckMenuKeys()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameController.instance.state == GameController.GameControllerState.STOREWINDOW)
            {
                storeWindow.SetActive(false);
            }
            else if (GameController.instance.state == GameController.GameControllerState.PAUSE)
            {
                if (!FindObjectOfType<PauseMenuWindow>().PromptPanelDeactivated())
                {
                    pauseMenuWindow.SetActive(false);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameController.instance.state == GameController.GameControllerState.STOREWINDOW)
            {
                storeWindow.SetActive(false);
            }
        }
    }

    //Checks if any debug key has been pressed and executes that command is DebugMode is true
    private void CheckAndRunDebugKeys()
    {
        if (GameController.instance.GetDebugMode())
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameController.instance.StartCoRestart();
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

            if (Input.GetKeyDown(KeyCode.C))
            {
                GameController.instance.DebugSpawnChunk();
                Debug.Log("spawn chunk");
            }
        }
    }
}
