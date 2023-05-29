using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private ElementManager elementManager;
    private UIManager uiManager;
    private GameStates gameState;
    private float totalTime;

    public ElementManager ElementManager { get => elementManager; }
    public UIManager UiManager { get => uiManager; }

    private enum GameStates
    {
        playing,
        paused,
        finished
    }
    private void Awake()
    {
        instance = this;
        elementManager = GetComponent<ElementManager>();
        uiManager = GetComponent<UIManager>();
        gameState = GameStates.paused;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        if(gameState == GameStates.playing)
        {
            totalTime += Time.deltaTime;
            UiManager.SetTotalTimeText(totalTime);
        }
    }
    public void NewGame()
    {
        gameState = GameStates.playing;
        elementManager.Initialize();
        totalTime = 0;
    }
    public bool isPlaying()
    {
        return gameState == GameStates.playing;
    }    
}
