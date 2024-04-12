using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PortalScript : MonoBehaviour
{
    private PlayerScript playerScript;
    private bool    is_avtivate;
    private float   size = 1f;
    private Vector2 portal_pos;
    private void Start()
    {
        portal_pos = transform.position;
        playerScript = FindObjectOfType<PlayerScript>();
    }

    public void setActive( bool setActive)
    {
        gameObject.SetActive(setActive);
    }

    public void SetActivatePortal()
    {
        is_avtivate = true;
    }

    public void setCanvasWin()
    {
        playerScript.Win.enabled = true;
    }

    void Update()
    {
        if (is_avtivate)
        {
            if (size <= 0f)
            {
                is_avtivate = false;
                Invoke("setCanvasWin", 0.5f);
                Instantiate(Resources.Load("Effects/S_blue_hit") as GameObject, new Vector3(playerScript.transform.position.x,playerScript.transform.position.y,-1f), Quaternion.identity);
                Sound.PlaySound("S_blue_hit");
            }
            else
            {
                size = size - 1.5f * Time.deltaTime;
                playerScript.transform.Rotate(new Vector3(0, 0, -1.5f));
                playerScript.transform.localScale = new Vector3(size, size, size);
                playerScript.transform.position = Vector2.Lerp(playerScript.transform.position, portal_pos, 3 * Time.deltaTime);
            }
        }
    }



}
