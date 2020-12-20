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

public enum LevelState {
    None = 0,
    MissionActive = 1,
    MissionVictory = 2,
    MissionFailed = 3
}
