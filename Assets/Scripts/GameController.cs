using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// Script creator Robin
/// </summary>

public class GameController : MonoBehaviour
{

    //SINGLETON
    public static GameController instance;

    //Texts
    private Text boxAmountText;
	public Text timeText;
	private Text currentPhaseText;

	//Objects
	[SerializeField] private GameObject box;
    [SerializeField] private GameObject miningRig;
    [SerializeField] private GameObject node;
    private PlayerController player;
    private GameObject gameOverText;
    private MeteroidSpawner meteroidSpawner;

    //General
    private int boxAmount = 0;
    public int phaseAmount = 5;
    public int currentPhase = 0;
    private int sceneAtm = 0; //Current scene
    private bool debugMode = false;
    public bool hijackedTimerText = false;
    public float totalTimeInPhase;

    //Phase Specifics
    public PhaseSpecifics[] phaseSpecifics;

	//STATE
	public GameControllerState state = GameControllerState.NULL;

    public enum GameControllerState
    {
        NULL = 1,
        MAINMENU,
        GAME,
        PAUSE,
        GAMEOVER,
    }

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
        StartUp();
        IncrementPhase();
    }

    private void StartUp()
    {
        Cursor.visible = false; // TODO SHOULD NOT BE HERE ONCE WE HAVE MAIN MENU

        timeText = GameObject.Find("TimeLeftInPhase").GetComponent<Text>();
        currentPhaseText = GameObject.Find("CurrentPhaseText").GetComponent<Text>();
        meteroidSpawner = FindObjectOfType<MeteroidSpawner>();
        gameOverText = GameObject.Find("GameOverText");

        gameOverText.SetActive(false);
    }

    private void Update()
    {
        if (state == GameControllerState.GAME)
        {
            //DO THINGS THAT SHOULD BE DONE IN EVERY PHASE
            PhaseTimer();
            InputController.instance.CheckKeys();

            for (int i = 0; i < phaseSpecifics.Length; i++)
            {
                if (i == currentPhase)
                {
                    
                }
            }
        }

        else if (state == GameControllerState.MAINMENU)
        {
            //DO MAINMENU THINGS
        }

        else if (state == GameControllerState.PAUSE)
        {
            //DO PAUSE THINGS
        }

        else if (state == GameControllerState.GAMEOVER)
        {
            //DO GAME OVER THINGS
        }
    }

    public void PhaseTimer()
    {
        
        if (!gameOverText.activeInHierarchy)
        {
            totalTimeInPhase -= Time.deltaTime;
        }

        if (!hijackedTimerText)
        {
            string timer = String.Format("Time Remaining: {0:0}:{1:00}", (int)totalTimeInPhase / 60, (int)totalTimeInPhase % 60);
            timeText.text = timer;
            if (totalTimeInPhase <= 30)
            {
                timeText.color = Color.red;
            }
            else
            {
                timeText.color = Color.green;
            }
        }

        if (totalTimeInPhase <= 0)
        {
            gameOverText.SetActive(true);
            Cursor.visible = true;
            state = GameControllerState.GAMEOVER; 
            //GameOver Condition!
        }
    }

    public void BoxDelivered()
	{
		boxAmount++;
        FindObjectOfType<ProgressBarScript>().ProgressBarUpdate();
	}

	// ============================= PHASE CHANGING STUFF HERE =============================
	public void IncrementPhase()
    {
        if (currentPhase != phaseSpecifics.Length - 1)
        {
            boxAmount = 0;
            if (currentPhase != 0)
                FindObjectOfType<ProgressBarScript>().ProgressBarUpdate();
            currentPhase++;
            currentPhaseText.text = "Current Phase: " + currentPhase;
            phaseAmount = phaseSpecifics[currentPhase].phaseBoxAmount;
            totalTimeInPhase = phaseSpecifics[currentPhase].totalTimeInPhase;
            StartCoroutine(meteroidSpawner.CoSpawnMeteroids(phaseSpecifics[currentPhase].timeBetweenMeteroids));
        }
        else if (currentPhase == phaseSpecifics.Length - 1 && boxAmount == phaseAmount)
        {
            Debug.Log("Test");
            gameOverText.GetComponent<Text>().fontSize = 10;
            gameOverText.GetComponent<Text>().color = Color.white;
            gameOverText.GetComponent<Text>().text = "Congrats, you created a Dyson Sphere!";
            gameOverText.SetActive(true);
        }
    }

    public void InvokeIncrementPhase(int time)
    {
        Invoke("IncrementPhase", time);
    }
    // =======================================================================================

    public IEnumerator Restart()
    {
        DestroyAll();
        SceneManager.LoadScene(sceneAtm);
        yield return new WaitForSeconds(1);
        state = GameControllerState.GAME;
        boxAmount = 0;
        currentPhase = 0;
        StartUp();
        IncrementPhase();
    }

    public void MainMenu()
    {
        instance.state = GameControllerState.MAINMENU;
        SceneManager.LoadScene(0);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
    

    private void DestroyAll()
    {
        foreach (PlayerController p in GameObject.FindObjectsOfType<PlayerController>())
        {
            Destroy(p.gameObject);
        }

        foreach (Box b in GameObject.FindObjectsOfType<Box>())
        {
            Destroy(b.gameObject);
        }

        foreach (MiningRig mr in GameObject.FindObjectsOfType<MiningRig>())
        {
            Destroy(mr.gameObject);
        }

        foreach (MiningNode mn in GameObject.FindObjectsOfType<MiningNode>())
        {
            Destroy(mn.gameObject);
        }
    }

    public int GetAmountOfDeliveredBoxes()
    {
        return boxAmount;
    }

    // ============================== EXTERNAL TOOLS ===========================
    public bool GetDebugMode()
    {
        return debugMode;
    }

    public void SetDebugMode(bool value)
    {
        debugMode = value;
    }

    public void DebugSpawnBox()
    {
        if (player)
            Instantiate(box, player.transform.position + player.transform.TransformDirection(Vector3.forward) * 4, transform.rotation, null);
        else
            player = FindObjectOfType<PlayerController>();
    }

    public void DebugSpawnMiningRig()
    {
        if (player)
            Instantiate(miningRig, player.transform.position + player.transform.TransformDirection(Vector3.forward) * 4, transform.rotation, null);
        else
            player = FindObjectOfType<PlayerController>();
    }

    public void DebugSpawnNode()
    {
        if (player)
            Instantiate(node, player.transform.position + player.transform.TransformDirection(Vector3.forward) * 4, transform.rotation, null);
        else
            player = FindObjectOfType<PlayerController>();
    }

}
