using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class WinScript : MonoBehaviour, IPointerClickHandler
{
    public bool is_home;
    public bool next_level;

    public void OnPointerClick(PointerEventData eventData)
    {
        int current_level   = PlayerPrefs.GetInt("LevelComplite");
        int build_index     = SceneManager.GetActiveScene().buildIndex + 1;

        if (current_level < build_index)
            LevelScript.nextSaveLevel(build_index);
        Debug.Log(PlayerPrefs.GetInt("LevelComplite"));

        if (is_home)
            SceneManager.LoadScene(1);
        else if (next_level)
        {
            if (build_index != 5)
            {
                SceneManager.LoadScene(build_index);
            }
            else
            {
                GetComponent<Button>().interactable = false;
            }
        }
    }
}
