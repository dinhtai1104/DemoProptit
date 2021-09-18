using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Utility
public class Utils
{
    const string GAME = "GAME";
    const string MUSIC_KEY = "MUSIC";
    const string SOUND_KEY = "SOUND";
    const string BALL_KEY = "BALL";
    const string CUP_KEY = "CUP";
    const string BALL_UNLOCK = "BALL UNLOCKED";
    const string CUP_UNLOCK = "CUP UNLOCKED";
    const string LEVEL_UNLOCK = "LEVEL UNLOCKED";
    const string LEVEL_COMPLETE = "LEVEL"; // Key: LEVEL_COMPLETE + "level" - Value: 0-3 stars

    #region AUDIO

    private static bool music = true;
    public static bool MUSIC
    {
        get
        {
            return music;
        }
        set
        {
            music = value;
            PlayerPrefs.SetInt(MUSIC_KEY, music ? 1 : 0);
            GameMaster.MusicClick?.Invoke();
        }
    }

    private static bool sound = true;
    public static bool SOUND
    {
        get
        {
            return sound;
        }
        set
        {
            sound = value;
            PlayerPrefs.SetInt(SOUND_KEY, sound ? 1 : 0);
            GameMaster.SoundClick?.Invoke();

        }
    }
    #endregion

    #region SKIN

    private static int ballSkin = 0;
    public static int BALL
    {
        get
        {
            return ballSkin;
        }
        set
        {
            ballSkin = value;
            PlayerPrefs.SetInt(BALL_KEY, ballSkin); // bong duoc chon 0-10
            PlayerPrefs.SetInt(BALL_UNLOCK + ballSkin, 1); // Unlock this ball
        }
    }

  

    private static int cupSkin = 0;
    public static int CUP
    {
        get { return cupSkin; }
        set
        {
            cupSkin = value;
            PlayerPrefs.SetInt(CUP_KEY, cupSkin);
            PlayerPrefs.SetInt(CUP_UNLOCK + cupSkin, 1); // Unlock this cup
        }
    }

    #endregion

    #region LEVEL_DATA
    private static int level = 1;
    public static int LEVEL
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            PlayerPrefs.SetInt(LEVEL_UNLOCK, level);
            if (!PlayerPrefs.HasKey(LEVEL_COMPLETE + level)) // Check star - Unlock
            {
                PlayerPrefs.SetInt(LEVEL_COMPLETE + level, 0); // set 0 star
            }
        }
    }

    private static int levelcurrent = 1;
    public static int LEVELCURRENT
    {
        set
        {
            levelcurrent = value;
            if (levelcurrent > MyGame.Pong.Object.GameManager.TotalLevel)
            {
                levelcurrent = MyGame.Pong.Object.GameManager.TotalLevel;
            }
            if (levelcurrent > LEVEL)
            {
                LEVEL = levelcurrent;
            }
        }
        get
        {
            return levelcurrent;
        }
    }
    public static int GetStarLevel(int pLevel)
    {
        return PlayerPrefs.GetInt(LEVEL_COMPLETE + pLevel, 0);
    }

    public static void SaveLevel(int lEVELCURRENT, int star)
    {
        int oldStar = GetStarLevel(lEVELCURRENT);
        if (oldStar < star)
        {
            PlayerPrefs.SetInt(LEVEL_COMPLETE + lEVELCURRENT, star); // set x star=
        }
    }
    #endregion

    public static void LoadData()
    {
        if (!PlayerPrefs.HasKey(GAME))
        {
            SOUND = true;
            MUSIC = true;
            PlayerPrefs.SetInt(GAME, 1);
        }
        
        SOUND = PlayerPrefs.GetInt(SOUND_KEY) == 1 ? true : false;
        MUSIC = PlayerPrefs.GetInt(MUSIC_KEY) == 1 ? true : false;
        
        if (!PlayerPrefs.HasKey(BALL_KEY))
        {
            BALL = 0;
        }
        if (!PlayerPrefs.HasKey(CUP_KEY))
        {
            CUP = 0;
        }

        if (!PlayerPrefs.HasKey(LEVEL_UNLOCK))
        {
            LEVEL = 1;
        }
        LEVEL = PlayerPrefs.GetInt(LEVEL_UNLOCK);
        LEVELCURRENT = LEVEL;
        BALL = PlayerPrefs.GetInt(BALL_KEY);
        CUP = PlayerPrefs.GetInt(CUP_KEY);
    }
}
