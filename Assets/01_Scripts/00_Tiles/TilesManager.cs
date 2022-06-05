using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TilesManager : SingletonManager<TilesManager>
{
    public LevelList thisLevel;
    private bool isSelected;
    public GameStates actState;
    public float startTime;
    public float deathTime;
    public float endTime;
    public float timeScale;
    private TileController first;
    public bool won;

    public CinemachineVirtualCamera[] LevelCameras;

    public int Score;
    public GameObject ScoreCanvas;
    public Image[] FlyIcons;


    public CarMovement[] LevelCars;
    public bool B_debug;

    private void Awake()
    {

        init();
    }

    private void Start()
    {
        actState = GameStates.initPuzzle;
        StartCoroutine(StartGame());
    }
    IEnumerator StartGame()
    {
        yield return ScriptsTools.GetWait(startTime);
        LevelCameras[0].gameObject.SetActive(false);
        yield return ScriptsTools.GetWait(startTime);
        actState = GameStates.think;
    }

    public IEnumerator EndGame()
    {
        actState = GameStates.end;
        yield return ScriptsTools.GetWait(deathTime);
        LevelCameras[1].gameObject.SetActive(false);
        yield return ScriptsTools.GetWait(endTime);
        ScoreScreen();
    }

    public void StartCars()
    {
        foreach (CarMovement c in LevelCars)
        {
            c.StartMovement();
        }
    }

    private void ScoreScreen()
    {
        ScoreCanvas.SetActive(true);
        if (Score > 0)
        {
            LevelsManager.current.Levels[thisLevel].I_Score = Score;
            LevelsManager.current.Levels[thisLevel].B_WasCompleted = true;
        }

    
}


    private void Update()
    {

        if (actState == GameStates.think)
        {
            Move();
            Rotate();
        }
    }


    void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;

            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            TileController tc;
            if (!Physics.Raycast(r, out hit))
            {
                Debug.Log("no hit");
                return;
            }

            if (!hit.transform.TryGetComponent(out tc))
            {
                Debug.Log("no tile");
                return;
            }

            if (!tc.PosibleMoves.Contains(TileMoves.Move))
            {
                if (first != null)
                {
                    first.Swap(first.StartPos);
                    first = null;
                    isSelected = false;
                }

                tc.WrongFeedback.PlayFeedbacks();
                return;
            }

            if (!isSelected)
            {
                tc.Move();
                first = tc;
                isSelected = true;
            }
            else if (isSelected)
            {
                Vector3 temp = tc.StartPos;
                tc.Swap(first.StartPos);
                first.Swap(temp);
                isSelected = false;
                first = null;
            }
        }
    }

    void Rotate()
    {

        if (Input.GetMouseButtonDown(1))
        {

            RaycastHit hit2;

            Ray r2 = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(r2, out hit2))
            {
                return;
            }

            if (!hit2.transform.TryGetComponent(out TileController tc))
            {
                return;
            }

            if (tc.PosibleMoves.Contains(TileMoves.Rotate))
            {
                tc.Rotate();
            }
            else
            {
                tc.WrongFeedback.PlayFeedbacks();
            }

        }

    }


    public void AddScore()
    {
        Score++;
        for (int i = 0; i < Score; i++)
        {
            FlyIcons[i].color = new Color(255, 255, 255, 1);
        }


    }
}
    

