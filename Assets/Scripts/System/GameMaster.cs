using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMaster
{
    //List<Function>
    public delegate void GameEvent();
    
    public static GameEvent AddNewBall;
    public static GameEvent JoinGame;
    public static GameEvent ReplayGame;
    public static GameEvent SoundClick;
    public static GameEvent MusicClick;

    public delegate void GameLoad(int pLevel);
    public static GameLoad LoadLevel; // Click vao button Level o Level Select

    public delegate void SelectChapter(int pChapter);
    public static SelectChapter SelectChapterLevel;
}
