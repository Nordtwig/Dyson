using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    //General
    private int boxAmount = 0;
    [SerializeField] private int phaseAmount = 5;
    public int currentPhase = 1;
    private int sceneAtm; //Current scene

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
        sceneAtm = 0;
    }

    void Update()
    {

    }

    private void Restart()
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
        // Set UI text instead
    }

    public void MainMenu()
    {
        instance.state = GameControllerState.MAINMENU;
        SceneManager.LoadScene(0);
    }

    private void DebugSpawnBox()
    {
        Instantiate(box, transform.position + transform.TransformDirection(Vector3.forward) * 2, transform.rotation, null);
    }

    private void DebugSpawnMiningRig()
    {
        Instantiate(miningRig, transform.position + transform.TransformDirection(Vector3.forward) * 2, transform.rotation, null);
    }

    private void DebugSpawnNode()
    {
        Instantiate(node, transform.position + transform.TransformDirection(Vector3.forward) * 2 + transform.TransformDirection(Vector3.down) * 2, transform.rotation, null);
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

}
