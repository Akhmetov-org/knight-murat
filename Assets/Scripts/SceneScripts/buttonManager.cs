using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonManager : MonoBehaviour, IPointerUpHandler
{
    public int lockLevel = 0;

    public void OnPointerUp(PointerEventData eventData)
    {
        if(GetComponent<Button>().interactable == true)
           SceneManager.LoadScene(lockLevel);
    }

}
