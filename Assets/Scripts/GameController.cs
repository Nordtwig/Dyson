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

    //Objects
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject miningRig;
    [SerializeField] private GameObject node;
    private PlayerController player;
    private Text timeText;
    private GameObject gameOverText;

    //General
    private int boxAmount = 0;
    [SerializeField] private int phaseAmount = 5;
    public int currentPhase = 1;
    private int sceneAtm = 0; //Current scene
    private bool debugMode = false;
    public float totalTimeInPhase;

    //STATE
    public GameControllerState state = GameControllerState.NULL;

    public enum GameControllerState
    {
        NULL = 1,
        MAINMENU,
        GAME,
        PAUSE,
        END,
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
        gameOverText = GameObject.Find("GameOverText");
        gameOverText.SetActive(false);
    }

    private void Update()
    {
        PhaseTimer();
    }

    public void Restart()
    {
        DestroyAll();
        boxAmount = 0;
        SceneManager.LoadScene(sceneAtm);
        state = GameControllerState.GAME;
    }

    public void BoxDelivered() 
    {
        boxAmount++;
        Debug.Log(boxAmount + "/" + phaseAmount + " Boxes delivered");
        if (boxAmount >= phaseAmount)
        {
            StartCoroutine(FindObjectOfType<Sled>().Launch());
            boxAmount = 0;
        }
        // Set UI text instead
    }

    public int GetAmountOfDeliveredBoxes()
    {
        return boxAmount;
    }

    public void MainMenu()
    {
        instance.state = GameControllerState.MAINMENU;
        SceneManager.LoadScene(0);
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

    private void DestroyAll()
    {
        //foreach (Player p in GameObject.FindObjectsOfType<Player>())
        //{
        //    Destroy(p);
        //}

        foreach (Box b in GameObject.FindObjectsOfType<Box>())
        {
            Destroy(b);
        }

        //foreach (MiningRig mr in GameObject.FindObjectsOfType<MiningRig>())
        //{
        //    Destroy(mr);
        //}

        foreach (MiningNode mn in GameObject.FindObjectsOfType<MiningNode>())
        {
            Destroy(mn);
        }
    }

    public bool GetDebugMode()
    {
        return debugMode;
    }

    public void SetDebugMode(bool value)
    {
        debugMode = value;
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

}
