using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    private static AudioClip S_land_snaze, S_muddy, lose_sound;

    private static AudioSource sound_data;
    void Start()
    {
        S_muddy = Resources.Load("sound/OtherSound/S_Blood_splash_low") as AudioClip;

        S_land_snaze = Resources.Load("sound/OtherSound/S_land_snaze") as AudioClip;

        sound_data = GetComponent<AudioSource>();

        sound_data.volume = PlayerPrefs.GetFloat("Effects");
    }

    public static void PlaySound(string clip)
    {

        switch (clip)
        {
            case "S_land_snaze":
                if(!sound_data.isPlaying)
                sound_data.PlayOneShot(S_land_snaze);
            break;

            case "S_muddy":
                sound_data.PlayOneShot(S_muddy);
            break;
        }
    }
}
