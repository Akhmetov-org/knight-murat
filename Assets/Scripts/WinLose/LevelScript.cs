using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    public static int level_complete;
    public Color enable, disable;
    public void DeletleAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void nextSaveLevel( int build)
    {
        PlayerPrefs.SetInt("LevelComplite", build);
        PlayerPrefs.Save();
    }

    void Start()
    {
        //DeletleAll();
        level_complete = PlayerPrefs.GetInt("LevelComplite",2);
        Debug.Log(level_complete);
        for (int i = 0;i< transform.childCount-1; i++)
        {
            if(level_complete >= i+2)
            {
                transform.GetChild(i).GetComponent<Image>().color = enable;
                transform.GetChild(i).GetComponent<Button>().interactable = true;
            }
            else
            {
                transform.GetChild(i).GetComponent<Image>().color = disable;
                transform.GetChild(i).GetComponent<Button>().interactable = false;
            }
        }
    }
}
