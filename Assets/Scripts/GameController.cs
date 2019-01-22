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
	private Text timeText;
	private Text currentPhaseText;

	//Objects
	[SerializeField] private GameObject box;
    [SerializeField] private GameObject miningRig;
    [SerializeField] private GameObject node;
    private PlayerController player;
    private GameObject gameOverText;

    //General
    private int boxAmount = 0;
    [SerializeField] private int phaseAmount = 5;
    public int currentPhase = 1;
    private int sceneAtm = 0; //Current scene
    private bool debugMode = false;
    public float totalTimeInPhase;

	//Phase Specifics
	[Header("Phase Specifics")]
	[SerializeField] private int phase1BoxAmount;
	[SerializeField] private int phase2BoxAmount;
	[SerializeField] private int phase3BoxAmount;
	[SerializeField] private int phase4BoxAmount;
	[SerializeField] private int phase5BoxAmount;

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
        timeText = GameObject.Find("TimeLeftInPhase").GetComponent<Text>();
		currentPhaseText = GameObject.Find("CurrentPhaseText").GetComponent<Text>();
		boxAmountText = GameObject.Find("BoxAmountText").GetComponent<Text>();
		gameOverText = GameObject.Find("GameOverText");
        gameOverText.SetActive(false);
		SetBoxAmountText();
	}

    private void Update()
    {
        if (state == GameControllerState.GAME)
        {
            //DO THINGS THAT SHOULD BE DONE IN EVERY PHASE
            PhaseTimer();
			currentPhaseText.text = "Current Phase: " + currentPhase;

			if (currentPhase == 1)
            {
                
            }

            else if (currentPhase == 2)
            {

            }

            else if (currentPhase == 3)
            {

            }

            else if (currentPhase == 4)
            {

            }

            else if (currentPhase == 5)
            {

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
        string timer = String.Format("Time Remaining: {0:0}:{1:00}", (int)totalTimeInPhase / 60, (int)totalTimeInPhase % 60);
        timeText.text = timer;
        totalTimeInPhase -= Time.deltaTime;

        if (totalTimeInPhase <= 30)
        {
            timeText.color = Color.red;
        }
        else
        {
            timeText.color = Color.black;
        }

        if (totalTimeInPhase <= 0)
        {
            gameOverText.SetActive(true);
            //GameOver Condition!
        }
    }

    public void BoxDelivered()
	{
		boxAmount++;
		//Debug.Log(boxAmount + "/" + phaseAmount + " Boxes delivered");
		if (boxAmount >= phaseAmount)
		{
            StartCoroutine(FindObjectOfType<Sled>().CoLaunch());
			Invoke("IncrementPhase", 2f);
			Invoke("SetBoxAmountText", 2f);
		}
		SetBoxAmountText();
		// Set UI text instead
	}

	private void SetBoxAmountText()
	{
		boxAmountText.text = "Boxes Delivered: " + boxAmount + "/" + phaseAmount;
	}

	// ============================= PHASE CHANGING STUFF HERE =============================
	private void IncrementPhase()
    {
		currentPhase++;
		boxAmount = 0;

		if (currentPhase == 1)
        {
			//DO PHASE SPECIFIC THINGS (EG. Set phase specific time, boxAmount, building dyson, etc.)
			phaseAmount = phase1BoxAmount;
		}

		else if (currentPhase == 2)
        {
			phaseAmount = phase2BoxAmount;
			totalTimeInPhase = 180f;
		}

        else if (currentPhase == 3)
        {
			phaseAmount = phase3BoxAmount;
        }

        else if (currentPhase == 4)
        {
			phaseAmount = phase4BoxAmount;
        }

        else if (currentPhase == 5)
        {
			phaseAmount = phase5BoxAmount;
        }
    }

    public void Restart()
    {
        DestroyAll();
        boxAmount = 0;
        SceneManager.LoadScene(sceneAtm);
        state = GameControllerState.GAME;
    }

    public void MainMenu()
    {
        instance.state = GameControllerState.MAINMENU;
        SceneManager.LoadScene(0);
    }

    private void DestroyAll()
    {
        foreach (PlayerController p in GameObject.FindObjectsOfType<PlayerController>())
        {
            Destroy(p.transform);
        }

        foreach (Box b in GameObject.FindObjectsOfType<Box>())
        {
            Destroy(b.transform);
        }

        foreach (MiningRig mr in GameObject.FindObjectsOfType<MiningRig>())
        {
            Destroy(mr.transform);
        }

        foreach (MiningNode mn in GameObject.FindObjectsOfType<MiningNode>())
        {
            Destroy(mn.transform);
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
