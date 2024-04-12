using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private  AudioClip   music;
    private  AudioSource music_data;

    public static float musicTimeDelta = 0;
    void Start()
    {
        music      = GetComponent<AudioSource>().clip;

        music_data = GetComponent<AudioSource>();

        music_data.volume = PlayerPrefs.GetFloat("Music");

        music_data.time   = musicTimeDelta;

    }

    void FixedUpdate()
    {
        //if (PlayerController.lose_win)  // stop music
            //music_data.Stop();

        if (music.length <= musicTimeDelta)
            musicTimeDelta = 0f;
        else
            musicTimeDelta = musicTimeDelta + Time.fixedDeltaTime;
    }
}
