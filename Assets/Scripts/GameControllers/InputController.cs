using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script creator Robin, modified by Svedlund
/// </summary>

public class InputController : MonoBehaviour
{
    //SINGLETON
    public static InputController instance;

    PlayerController player;
	ThrowPowerBarScript throwBar;
    private GameObject storeWindow;
    private GameObject pauseMenuWindow;
    private bool jumpButtonDown = false;
    private bool chargeIncreasing = true;
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
		throwBar = FindObjectOfType<ThrowPowerBarScript>();
        storeWindow = FindObjectOfType<StoreWindowVisuals>().gameObject;
        storeWindow.SetActive(false);
        pauseMenuWindow = FindObjectOfType<PauseMenuWindowVisuals>().gameObject;
        pauseMenuWindow.SetActive(false);
    }

    // ===================== Short about ======================================
    // press F1 to enable disable DebugMode, disabled by default
    // Uses (R)estart, (N)odeSpawn, (M)iningRigSpawn, (B)oxSpawn, (C)hunkSpawn

    // Runs the keys that have any functionality
    public void CheckKeys()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameController.instance.SetDebugMode(!GameController.instance.GetDebugMode()); //inverts the value of DebugMode
            Debug.Log(GameController.instance.GetDebugMode());
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

    //Runs player specific keys
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
            chargeIncreasing = true;
            if (eTime < 0.5f)
            {
				throwBar.ThrowPowerBarUpdate(0, player.holdingItem);
                player.PlayerInteraction();
                ThrowPowerBarScript.instance.SetEnableThrowBackground(false);
            }
            else
            {
				throwBar.ThrowPowerBarUpdate(0, player.holdingItem);
                player.ThrowItem(Mathf.Clamp(eTime, 0, 1f));
                ThrowPowerBarScript.instance.SetEnableThrowBackground(false);
            }
			eTime = 0;
        }
        
        if (Input.GetKey(KeyCode.E))
        {
            if (eTime < 0.51f)
            {
                chargeIncreasing = true;
            }
            else if (eTime > 0.99f)
            {
                chargeIncreasing = false;
            }

            if (chargeIncreasing)
            {
                eTime += Time.deltaTime;
            }
            else
            {
                eTime -= Time.deltaTime;
            }

            if (eTime > 0.5f)
            {
                throwBar.ThrowPowerBarUpdate(Mathf.Clamp(eTime, 0, 1f), player.holdingItem);
            }
            
        }
        
    }

    //Runs menu specific keys
    public void CheckMenuKeys()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ButtonTrigger[] allButtons = FindObjectsOfType<ButtonTrigger>();
            foreach (ButtonTrigger button in allButtons)
            {
                button.ResetScale();
                Debug.Log("resetting a button");
            }
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

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (GameController.instance.state == GameController.GameControllerState.STOREWINDOW)
            {
                ButtonTrigger[] allButtons = FindObjectsOfType<ButtonTrigger>();
                foreach (ButtonTrigger button in allButtons)
                {
                    button.ResetScale();
                    Debug.Log("resetting a button");
                }
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

            if (Input.GetKeyDown(KeyCode.C))
            {
                GameController.instance.DebugSpawnChunk();
                Debug.Log("spawn chunk");
            }
            
            if (Input.GetKeyDown(KeyCode.P) & !(GameController.instance.state == GameController.GameControllerState.PAUSE))
            {
                storeWindow.SetActive(!storeWindow.activeInHierarchy);
            }
        }
    }
}
