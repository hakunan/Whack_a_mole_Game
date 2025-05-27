using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] UiManager uiManager;
    [SerializeField] float timeLimit = 60;
    [SerializeField] float interval = 1;
    
    [SerializeField] MoleSystem[] moles;

    private float timer;
    private float popUpTimer;
    private int score =0;
    private bool isInGame;

    public float moleSpeedLevel = 0.001f;
    

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        isInGame = true;
        timer = timeLimit;

        if (moles.Length == 0)
        {
            var molesGameObjects = GameObject.FindGameObjectsWithTag("Mole");
            moles = new MoleSystem[molesGameObjects.Length];
            for (int i = 0; i < molesGameObjects.Length; i++)
            {
                moles[i] = molesGameObjects[i].GetComponent<MoleSystem>();
            }
        }
    }
    private void Update()
    {
        if(!isInGame) { return; }

        AdvanceTime();
        SelectLevelByTime();

        popUpTimer += Time.deltaTime;
        if (popUpTimer >= interval)
        {
            RandomPopupMole();
            popUpTimer = 0;
        }
    }
    private void SelectLevelByTime()
    {
        float percentLeft = timer / timeLimit;

        if (percentLeft > 0.7f)
        {
            moleSpeedLevel = 0.001f;
            uiManager.SetCurrentLevelText("slow");
        }
        else if (percentLeft > 0.3f)
        {
            moleSpeedLevel = 0.003f;
            uiManager.SetCurrentLevelText("normal");
        }
        else
        {
            moleSpeedLevel = 0.006f;
            uiManager.SetCurrentLevelText("rapid");
        }

    }
    private void RandomPopupMole()
    {
        moles[Random.Range(0, moles.Length)].PopUp();
    }

    private void AdvanceTime()
    {
        if(timer <= 0) { GameEnd(); }
        timer -= Time.deltaTime;
        uiManager.SetTimerText(timer);
    }

    public void AddScore(int score)
    {
        this.score += score;
        uiManager.SetScoreText(this.score);
    }
    private void GameEnd()
    {
        isInGame = false;
        uiManager.GameoverScreenActive();
    }

}
