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

    //General
    private int boxAmount = 0;
    [SerializeField] private int phaseAmount = 5;
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

    private void DebugSpawnNode()
    {

    }

    private void DebugSpawnBox()
    {

    }

    private void DestroyAll()
    {
        //for (Player p in GameObject.FindObjectOfType<Player>())
        //{
        //    Destroy(p);
        //}

        //for (Box b in GameObject.FindObjectOfType<Box>())
        //{
        //    Destroy(b);
        //}

        //for (MiningRig mr in GameObject.FindObjectOfType<MiningRig>())
        //{
        //    Destroy(mr);
        //}

        //for (MiningNode mn in GameObject.FindObjectOfType<MiningNode>())
        //{
        //    Destroy(mn);
        //}
    }

}
