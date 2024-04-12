using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UiPause : MonoBehaviour, IPointerClickHandler
{
    public GameObject BG;
    public bool is_pause = false;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (button.interactable)
        {
            if (!is_pause)
            {
                PlayerScript.allPause = false;
                is_pause = true;
                BG.SetActive(false);
            }
            else
            {
                PlayerScript.allPause = true;
                is_pause = false;
                BG.SetActive(true);
            }
        }
    }

    void LateUpdate()
    {
        if (PlayerScript.lose_win && button.interactable)
        {
            button.interactable = false;
            BG.SetActive(false);
        }
    }
}

