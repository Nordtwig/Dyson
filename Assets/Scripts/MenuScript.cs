using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Christoffer Brandt
/// Noah Nordqvist
/// </summary>
public class MenuScript : MonoBehaviour
{
    public GameObject[] views;
    public GameObject currView;

    // Start is called before the first frame update
    void Start()
    {
        currView = views[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*================================== Main Menu ======================================*/

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("BrandtTestScene");
    }
    /*================================== Play Menu ======================================*/

    public void GoToView(int view) {
        currView.SetActive(false);
        currView = views[view];
        currView.SetActive(true);
    } 

    //public void ReturnToView(int view) {
    //    currView.SetActive(false);
    //    currView = views[view];
    //    currView.SetActive(true);
    //}
}
