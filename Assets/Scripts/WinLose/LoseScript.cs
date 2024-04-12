using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LoseScript : MonoBehaviour, IPointerClickHandler
{
    public bool isHome    = false;
    public bool isRestart = false;

    public UiPause ui_script;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isHome)
            SceneManager.LoadScene(1);
        else if(isRestart)
            Application.LoadLevel(Application.loadedLevel);
    }
}
