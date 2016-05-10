using UnityEngine;
using System.Collections;

public interface IGameState {
    void start();
    void stop();
    void run();
}

public enum GameStateEnum
{
    ROOT, 
    MAIN_MENU, 
    LOADING, 
    IN_GAME, 
    PAUSE_MENU,
    SKILL_MENU, 
    MAP_MENU
}
