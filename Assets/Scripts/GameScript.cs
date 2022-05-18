using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public List<GameObject> RightCubes = new List<GameObject>();
    public List<GameObject> LeftCubes = new List<GameObject>();
    public List<GameObject> StandartCubes = new List<GameObject>();
    public List<GameObject> DifficulityImages = new List<GameObject>();
    private List<List<GameObject>> RL = new List<List<GameObject>>();
    private List<float[]> RB= new List<float[]>();
    private float[] CoordsX = new float[6] {-0.7f, -0.45f, -0.2f, 0.2f, 0.45f, 0.7f };
    private float[] CoordsXRed = new float[3] { 0.2f, 0.45f, 0.7f };
    private float[] CoordsXBlue = new float[3] { -0.7f, -0.45f, -0.2f };
    private float[] CoordsY = new float[2] { 1f, 0.75f };
    private float StartZ = 10;
    public int Score;
    public float GameTime;
    public bool HardMode;
    private System.Random rnd = new System.Random();
    public float Speed = 1;
    public GameObject Walls;
    public Text Timer;
    public Text ScoreText;
    public Text Speedometer;
    public CubeBreaker Sword1;
    public CubeBreaker Sword2;
    public CubeBreaker Wall;

    public float TimeGeneration = 1;
    private float TimePreGeneration = 0;

    void Start()
    {
        Score = 0;
        GameTime = 0;
        HardMode = false;
        RL.Add(RightCubes);
        RL.Add(LeftCubes);
        RB.Add(CoordsXRed);
        RB.Add(CoordsXBlue);
    }

    void Update()
    {
        if (GameTime > 0)
        {
            if (TimePreGeneration <= 0)
            {
                TimePreGeneration = TimeGeneration;
                if (GameTime > 10) // добавить разделение на мечи
                {
                    if (HardMode)
                    {
                        int a, b, c, d; //a:CoordY; b:CoordX; c:StandartCube; d:Side
                        a = rnd.Next(0, 2);
                        b = rnd.Next(0, 6);
                        c = rnd.Next(0, 6);
                        d = rnd.Next(0, 2);
                        var _Cube = Instantiate(RL[d][c], new Vector3(RB[d][b], CoordsY[a], StartZ), Quaternion.identity);
                        _Cube.AddComponent<CubeMoving>();
                        CubeMoving _Move = _Cube.gameObject.GetComponent<CubeMoving>();
                        _Move.speed = Speed;
                        _Move.Color = d;
                        
                    }
                    else
                    {
                        int a, b, c; //a:CoordY; b:CoordX; c:StandartCube
                        a = rnd.Next(0, 2);
                        b = rnd.Next(0, 6);
                        c = rnd.Next(0, 6);
                        var _Cube = Instantiate(StandartCubes[c], new Vector3(CoordsX[b], CoordsY[a], StartZ), Quaternion.identity);
                        _Cube.AddComponent<CubeMoving>();
                        CubeMoving _Move = _Cube.gameObject.GetComponent<CubeMoving>();
                        _Move.speed = Speed;
                        _Move.Color = 2;

                    }
                }
            }
            else
            {
                TimePreGeneration -= Time.deltaTime;
            }
            GameTime -= Time.deltaTime;
        }
        if (GameTime > 0)
        {
            Walls.SetActive(false);
        }
        else
        {
            Walls.SetActive(true);
            ScoreChanger();
        }

        
    }


    public void GameStart()
    {
        GameTime = 100;
    }

    public void TimeChange()
    {
        TimeGeneration += 1;
        TimeGeneration %= 5;
        if (TimeGeneration == 0)
            TimeGeneration = 1;
        Timer.text = $"{TimeGeneration} sec";
        
    }

    public void DifficulityChange()
    {
        if (HardMode)
        {
            HardMode = false;
            DifficulityImages[0].SetActive(true);
            DifficulityImages[1].SetActive(false);
        }
        else
        {
            HardMode = true;
            DifficulityImages[0].SetActive(false);
            DifficulityImages[1].SetActive(true);
        }
    }

    public void Checker()
    {
        Debug.Log("Changed");
    }
    
    public void ScoreChanger()
    {
        ScoreText.text = $"Points:\n" +
            $"\tLeft hand: {Sword1.Score} \n" +
            $"\tRight hand: {Sword2.Score} \n" +
            $"\tLose: {Wall.Score}";
    }

    public void SpeedChanger()
    {
        Speed += 0.5f;
        if (Speed > 2)
        {
            Speed = 1;
        }
        Speedometer.text = $"{Speed}";
    }
}
