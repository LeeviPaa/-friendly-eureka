using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SixDirections {
    Left,
    Right,
    Up,
    Down,
    Forward,
    Backward
}

[System.Serializable]
public enum LevelState {
    MainMenu = 0,
    MissionActive = 1,
    MissionVictory = 2,
    MissionFailed = 3,
    Credits = 4
}
