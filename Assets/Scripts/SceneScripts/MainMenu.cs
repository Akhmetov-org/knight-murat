using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour, IPointerClickHandler
{

    public bool play_game = false;
    public bool settinds  = false;
    public bool is_exit   = false;

    public Canvas setting,mainMenu;


    private void Awake()
    {
        setting.GetComponent<Canvas>().enabled  = false;
        mainMenu.GetComponent<Canvas>().enabled = true;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (play_game)
            SceneManager.LoadScene(1); // level menu
        else if (settinds)
        {
            setting.GetComponent<Canvas>().enabled = true;
            mainMenu.GetComponent<Canvas>().enabled = false;
        }
        else if (is_exit)
            Application.Quit();
    }
}
