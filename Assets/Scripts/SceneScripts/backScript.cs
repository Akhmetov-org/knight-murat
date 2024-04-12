using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class backScript : MonoBehaviour, IPointerClickHandler
{
    public Canvas settnds, mainMenu;

    public Slider music, effects;

    public TMP_Dropdown drop_down;


    public static void nextSaveLevel(int build)
    {
        PlayerPrefs.SetInt("LevelComplite", build);
        PlayerPrefs.Save();
    }

    private void Awake()
    {
        int   Quality = PlayerPrefs.GetInt(  "Quality", 4);
        float Music   = PlayerPrefs.GetFloat("Music"  , 0.85f);
        float Effects = PlayerPrefs.GetFloat("Effects", 0.75f);

        //~~~~~~~~~~Setting Variables~~~~~~~~~~~//

        drop_down.ClearOptions();
        drop_down.AddOptions(QualitySettings.names.ToList());
        drop_down.value = QualitySettings.GetQualityLevel();

        //~~~~~~~~Setting Player Prefs~~~~~~~~~//

        drop_down.value = Quality;
        music.value     = Music;
        effects.value   = Effects;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        settnds.GetComponent<Canvas>().enabled = false;
        mainMenu.GetComponent<Canvas>().enabled = true;

        //~~~~~~~~~~Drop Down Quality~~~~~~~~~~~//
        PlayerPrefs.SetInt("Quality", drop_down.value);

        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));

        //~~~~~~~~~~Music and Effects~~~~~~~~~~~//
        PlayerPrefs.SetFloat("Music",    music.value);
        PlayerPrefs.SetFloat("Effects", effects.value);

        //~~~~~~~~~~~~Save Options~~~~~~~~~~~~~~//

        PlayerPrefs.Save();

    }

}
