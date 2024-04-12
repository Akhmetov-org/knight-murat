using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{

    private static AudioClip HeavySword;
    private static AudioClip jump_sound, S_technology, S_essence_found, S_sound_attack, S_witch_ward, S_land_snaze, S_muddy, S_blue_hit, S_claydrain, S_Cold_bark,
    S_desperate, S_chilly_kick, S_rubysun, S_rock_monster, S_Forward_thurder, S_Combo_knock, S_Blood_splash, S_Blue_Hole;

    private static AudioClip S_X_chop, S_fullsight, S_Thunder_rage;

    private static AudioClip win_sound, lose_sound;

    private static AudioSource sound_data;

    void Awake()
    {

        //~~~~~~~Setting Base Sound~~~~~~//
        HeavySword = Resources.Load("Sound/CombatSound/HeavySword") as AudioClip;
        S_technology = Resources.Load("sound/CombatSound/S_technology") as AudioClip;
        S_essence_found = Resources.Load("sound/CombatSound/S_essence_found") as AudioClip;
        S_sound_attack = Resources.Load("Sound/BaseSound/S_sound_attack") as AudioClip;
        S_witch_ward = Resources.Load("Sound/BaseSound/S_witch_ward") as AudioClip;
        jump_sound = Resources.Load("Sound/BaseSound/jump") as AudioClip;
        S_muddy = Resources.Load("Sound/BaseSound/S_muddy") as AudioClip;
        S_claydrain = Resources.Load("Sound/CombatSound/S_claydrain") as AudioClip;
        S_blue_hit = Resources.Load("Sound/BaseSound/S_blue_hit") as AudioClip;
        S_land_snaze = Resources.Load("Sound/BaseSound/S_land_snaze") as AudioClip;

        S_Cold_bark = Resources.Load("Sound/CombatSound/S_Cold_bark") as AudioClip;
        S_desperate = Resources.Load("Sound/CombatSound/S_desperate") as AudioClip;
        S_chilly_kick  = Resources.Load("Sound/CombatSound/S_chilly_kick") as AudioClip;

        S_Blue_Hole = Resources.Load("Sound/CombatSound/S_Blue_Hole") as AudioClip;
        S_rubysun = Resources.Load("Sound/CombatSound/S_rubysun") as AudioClip;
        S_rock_monster = Resources.Load("Sound/CombatSound/S_rock_monster") as AudioClip;
        S_Forward_thurder = Resources.Load("Sound/CombatSound/S_Forward_thurder") as AudioClip;
        S_Combo_knock = Resources.Load("Sound/CombatSound/S_Combo_knock") as AudioClip;
        S_Blood_splash = Resources.Load("Sound/CombatSound/S_Blood_splash") as AudioClip;


        S_X_chop       = Resources.Load("Sound/CombatSound/S_X_chop") as AudioClip;
        S_fullsight    = Resources.Load("Sound/CombatSound/S_fullsight") as AudioClip;
        S_Thunder_rage = Resources.Load("Sound/CombatSound/S_Thunder_rage") as AudioClip;



        //~~~Other sound and Variables~~~//
        win_sound = Resources.Load("Sound/BaseSound/win_sound") as AudioClip;

        lose_sound = Resources.Load("Sound/BaseSound/lose_sound") as AudioClip;

        sound_data = GetComponent<AudioSource>();

        sound_data.volume = PlayerPrefs.GetFloat("Effects");

    }
    public static void PlaySound(string clip)
    {

        switch (clip)
        {
            case "S_X_chop":
                sound_data.PlayOneShot(S_X_chop);
                break;

            case "S_fullsight":
                sound_data.PlayOneShot(S_fullsight);
                break;

            case "S_Thunder_rage":
                sound_data.PlayOneShot(S_Thunder_rage);
                break;

            case "S_Blue_Hole":
                sound_data.PlayOneShot(S_Blue_Hole);
                break;

            case "S_Combo_knock":
                sound_data.PlayOneShot(S_Combo_knock);
                break;

            case "S_Blood_splash":
                sound_data.PlayOneShot(S_Blood_splash);
                break;

            case "S_rubysun":
                sound_data.PlayOneShot(S_rubysun);
                break;

            case "S_rock_monster":
                sound_data.PlayOneShot(S_rock_monster);
                break;

            case "S_Forward_thurder":
                sound_data.PlayOneShot(S_Forward_thurder);
                break;

            case "S_chilly_kick":
                sound_data.PlayOneShot(S_chilly_kick);
                break;

            case "S_Cold_bark":
                sound_data.PlayOneShot(S_Cold_bark);
                break;

            case "S_desperate":
                sound_data.PlayOneShot(S_desperate);
                break;

            case "S_claydrain":
                sound_data.PlayOneShot(S_claydrain);
                break;


            case "S_blue_hit":
                sound_data.PlayOneShot(S_blue_hit);
                break;

            case "S_muddy":
                sound_data.PlayOneShot(S_muddy);
                break;

            case "S_land_snaze":
                sound_data.PlayOneShot(S_land_snaze);
                break;

            case "Jump_sound":
                sound_data.PlayOneShot(jump_sound);
                break;

            case "HeavySword":
                sound_data.PlayOneShot(HeavySword);
                break;

            case "S_technology":
                sound_data.PlayOneShot(S_technology);
                break;

            case "S_sound_attack":
                sound_data.PlayOneShot(S_sound_attack);
                break;

            case "S_witch_ward":
                sound_data.PlayOneShot(S_witch_ward);
                break;

            case "S_essence_found":
                sound_data.PlayOneShot(S_essence_found);
                break;

            case "win_sound":
                sound_data.volume = PlayerPrefs.GetFloat("Music");
                sound_data.PlayOneShot(win_sound);
                break;

            case "lose_sound":
                sound_data.volume = PlayerPrefs.GetFloat("Music");
                sound_data.PlayOneShot(lose_sound);
                break;

        }
    }


}
