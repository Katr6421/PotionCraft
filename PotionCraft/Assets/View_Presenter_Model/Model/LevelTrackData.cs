// Used to save data that we need to track for each level

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrackData
{

    public int LevelNumber {get; set;}
    public int WrongClickCounter {get; set;}
    public float TimeForCompletingLevel {get; set;}
    public int ClickOnRulesButton {get; set;}
    public bool HasCompletedLevel{get; set;}

    public LevelTrackData(int levelNumber){
        LevelNumber = levelNumber;
        WrongClickCounter = 0;
        TimeForCompletingLevel = 0f;
        ClickOnRulesButton = 0;
        HasCompletedLevel = true;
    }







}