using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// Script creator Robin, modified by Heimer, Svedlund
/// </summary>

public class GameController : MonoBehaviour
{

    //SINGLETON
    public static GameController instance;

    //Texts
    private Text boxAmountText;
	public Text timeText;
	private Text currentPhaseText;
    private Text creditsText;
    private Text creditsTextUI;

    //Objects
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject miningRig;
    [SerializeField] private GameObject node;
    [SerializeField] private GameObject chunk;
    [SerializeField] private GameObject launchButton;
    [Tooltip("Do NOT touch")] public Material[] metalMaterials;
    private PlayerController player;
    private GameObject gameOverText;
    private GameObject winText;
    private MeteroidSpawner meteroidSpawner;
    private Hexes dysonSphere;
    private Light sunSource;

    //General
    private bool debugMode = false;
    private int boxAmount = 0;

    [HideInInspector] public int phaseBoxAmount = 5;
    [HideInInspector] public bool hijackedTimerText = false;
    public int currentPhase = 0;
    public float totalTimeInPhase;
    public int playerCredits = 0;
    public int boxCreditsReward = 5;
    public int phaseCreditReward = 50;
    public int startingMoney = 0;
    public float lightFadeAmount = 0.2f;

    //Phase Specifics
    [HideInInspector] public PhaseSpecifics[] phaseSpecifics;
    public PhaseSpecifics[] testingSpecifics;

    [Header("NodeKeeper")]
    public int cobaltCount;
    public int tungstenCount;
    public int cinnabarCount;
    public int mixedCount;
    public List<GameObject> nodes;

	//STATE
	public GameControllerState state = GameControllerState.NULL;

    public enum GameControllerState
    {
        NULL = 1,
        MAINMENU,
        GAME,
        PAUSE,
        GAMEOVER,
        STOREWINDOW,
    }

    public enum MetalVarieties
    {
        CINNABAR,
        TUNGSTEN,
        COBALT,
        MIXED,
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            nodes = new List<GameObject>();

            DontDestroyOnLoad(instance);

            instance.enabled = true;
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (SceneManager.GetActiveScene().buildIndex != 0) {
            phaseSpecifics = testingSpecifics;
            StartUp();
            IncrementPhase();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        if (scene.name == "RobinTestScene") {
            StartUp();
            IncrementPhase();
        }
    }

    public void StartUp() 
    {
        state = GameControllerState.GAME;

        Cursor.visible = false; 

        //Update all dependencies in GameController. 
        currentPhase = 0;
        totalTimeInPhase = 0;
        boxAmount = 0;
        timeText = GameObject.Find("TimeLeftInPhase").GetComponent<Text>();
        currentPhaseText = GameObject.Find("CurrentPhaseText").GetComponent<Text>();
        gameOverText = GameObject.Find("GameOverText");
        creditsText = GameObject.Find("CreditsText").GetComponent<Text>();
        creditsTextUI = GameObject.Find("CreditsTextUI").GetComponent<Text>();
        winText = GameObject.Find("WinScreen");
        sunSource = GameObject.Find("SunSourceWithFlare").GetComponent<Light>();

        meteroidSpawner = FindObjectOfType<MeteroidSpawner>();
        dysonSphere = FindObjectOfType<Hexes>();
        player = FindObjectOfType<PlayerController>();
        launchButton = GameObject.Find("LaunchButton");
        AudioManager.instance.Play("Ambience");
        playerCredits = startingMoney;
        creditsText.text = playerCredits.ToString();
        creditsTextUI.text = playerCredits.ToString();
        //AudioManager.instance.Play("Music Long");


        //Update all other instances for their dependencies.
        InputController.instance.StartUp();

        gameOverText.SetActive(false);
        winText.SetActive(false);
    }

    private void Update()
    {
        if (state == GameControllerState.GAME)
        {
            //DO THINGS THAT SHOULD BE DONE IN EVERY PHASE
            PhaseTimer();
            InputController.instance.CheckKeys();

            //phaseSpecifics[currentPhase];
        } 

        else if (state == GameControllerState.MAINMENU)
        {
            //DO MAINMENU THINGS
        }

        else if (state == GameControllerState.PAUSE)
        {
            InputController.instance.CheckMenuKeys();
        }

        else if (state == GameControllerState.GAMEOVER)
        {
            //DO GAME OVER THINGS
        }

        else if (state == GameControllerState.STOREWINDOW)
        {
            //DO STORE WINDOW THINGS
            InputController.instance.CheckMenuKeys();
        }
    }

    public void PhaseTimer()
    {
        
        if (!hijackedTimerText)
        {
            totalTimeInPhase -= Time.deltaTime;
        }

        if (!hijackedTimerText)
        {
            string timer = String.Format("{0:0}:{1:00}", (int)totalTimeInPhase / 60, (int)totalTimeInPhase % 60);
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
        UpdateCredits(boxCreditsReward);
        if (boxAmount == phaseBoxAmount) {
            launchButton.transform.GetChild(1).gameObject.SetActive(true);
        }
	}

	// ============================= PHASE CHANGING STUFF HERE =============================
	public void IncrementPhase()
    {
        launchButton.transform.GetChild(1).gameObject.SetActive(false);
        if (currentPhase != phaseSpecifics.Length - 1)
        {
            boxAmount = 0;
            if (currentPhase != 0)
            {
                iTween.PunchPosition(Camera.main.gameObject, UnityEngine.Random.insideUnitCircle * 3f, 1f);
                FindObjectOfType<ProgressBarScript>().ProgressBarUpdate();
                UpdateCredits(phaseCreditReward + Mathf.FloorToInt(totalTimeInPhase / 5));
                Debug.Log("Bonus: " + Mathf.FloorToInt(totalTimeInPhase / 5));
                StartCoroutine(CoFadeLight(sunSource.intensity, sunSource.intensity - lightFadeAmount));
            }
            currentPhase++;
            //dysonSphere.UpdateHexes() //moved to CoFadeLight for timing purposes
            currentPhaseText.text = "Phase: " + currentPhase + "/" + phaseSpecifics.Length - 1;
            Debug.Log(currentPhase);
            Debug.Log(phaseSpecifics.Length);
            phaseBoxAmount = phaseSpecifics[currentPhase].phaseBoxAmount;
            totalTimeInPhase = totalTimeInPhase/2 + phaseSpecifics[currentPhase].totalTimeInPhase;
            StartCoroutine(meteroidSpawner.CoSpawnMeteroids(phaseSpecifics[currentPhase].timeBetweenMeteroids));

            ProgressBarScript.instance.ProgressBarUpdate();
        }
        else if (currentPhase == phaseSpecifics.Length - 1 && boxAmount == phaseBoxAmount)
        {
            winText.SetActive(true);
            state = GameControllerState.GAMEOVER;
            Cursor.visible = true;
        }
    }

    public void InvokeIncrementPhase(int time)
    {
        Invoke("IncrementPhase", time); 
    }

    public void UpdateCredits(int amount)
    {
        playerCredits += amount;
        //creditsText.text = "Credits: " + playerCredits;
        creditsText.text = playerCredits.ToString();
        creditsTextUI.text = playerCredits.ToString();
        if (amount > 0) StartCoroutine(FindObjectOfType<StoreWindow>().GainCreditsPopUp(Mathf.Abs(amount)));
    }

    public void UpdateNodeTypes() {
        cobaltCount = 0;
        tungstenCount = 0;
        cinnabarCount = 0;
        mixedCount = 0;

        foreach (GameObject node in GameController.instance.nodes) {
            switch (node.GetComponent<MiningNode>().materialType) {
                case MetalVarieties.COBALT:
                    cobaltCount++;
                    break;
                case MetalVarieties.TUNGSTEN:
                    tungstenCount++;
                    break;
                case MetalVarieties.CINNABAR:
                    cinnabarCount++;
                    break;
                case MetalVarieties.MIXED:
                    mixedCount++;
                    break;
            }
        }
    }

    // =======================================================================================

    public void Restart()
    {
        StopAllCoroutines();
        nodes.Clear();
        DestroyAll();
        currentPhase = 0;
        totalTimeInPhase = 0;
        boxAmount = 0;
        playerCredits = startingMoney;
        creditsText.text = playerCredits.ToString();
        creditsTextUI.text = playerCredits.ToString();
        int sceneAtm = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneAtm);
    } 

    public void StartCoMainMenu()
    {
        StopAllCoroutines();
        StartCoroutine(CoMainMenu());
        MusicManager.instance.CrossfadeMusic(MusicManager.instance.musicIntro, MusicManager.instance.menuMusicLoop, true);
    }

    public IEnumerator CoMainMenu()
    {
        nodes.Clear();
        SceneManager.LoadScene(0);
        state = GameControllerState.MAINMENU;
        yield return new WaitForEndOfFrame();
        state = GameControllerState.MAINMENU;
        yield return new WaitForSeconds(0.5f);
        Cursor.visible = true;
        state = GameControllerState.MAINMENU;
        yield return null;
    }

    public IEnumerator CoFadeLight(float currentLight, float targetLight)
    {

        while(currentLight >= targetLight)
        {
            currentLight -= 0.01f;
            yield return new WaitForSeconds(0.2f);
            sunSource.intensity = currentLight;      
        }
        dysonSphere.UpdateHexes();
        yield return null;
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
        Instantiate(box, player.transform.position + player.transform.TransformDirection(Vector3.forward) * 4, transform.rotation, null);
    }

    public void DebugSpawnMiningRig()
    {
        Instantiate(miningRig, player.transform.position + player.transform.TransformDirection(Vector3.forward) * 4, transform.rotation, null);
    }

    public void DebugSpawnNode()
    {
        GameObject myNode = Instantiate(node, player.transform.position + player.transform.TransformDirection(Vector3.forward) * 4, transform.rotation, null);
        nodes.Add(myNode);
    }

    public void DebugSpawnChunk()
    {
        GameObject g = Instantiate(chunk, player.transform.position + player.transform.TransformDirection(Vector3.forward) * 4, transform.rotation, null);
        g.GetComponent<Chunk>().RandomChunkType();
    }

}
