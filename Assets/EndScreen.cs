using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{

    public void GoToMainMenu()
    {
        GameController.instance.StartCoMainMenu();
    }

    public void Restart()
    {
        GameController.instance.Restart();
    }

    public void Quit()
    {
        GameController.instance.Quit();
    }

}
