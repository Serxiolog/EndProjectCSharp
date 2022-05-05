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
    private List<GameObject> Moving = new List<GameObject>();
    public float Speed = 1;
    public GameObject Walls;
    public Text Timer;

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
                Debug.Log("Generate box");
                TimePreGeneration = TimeGeneration;
                if (HardMode)
                {
                    int a, b, c, d; //a:CoordY; b:CoordX; c:StandartCube; d:Side
                    a = rnd.Next(0, 2);
                    b = rnd.Next(0, 6);
                    c = rnd.Next(0, 6);
                    d = rnd.Next(0, 2);
                    var _Cube = Instantiate(RL[d][c], new Vector3(RB[d][b], CoordsY[a], StartZ), Quaternion.identity);
                    Moving.Add(_Cube);
                }
                else
                {
                    int a, b, c; //a:CoordY; b:CoordX; c:StandartCube
                    a = rnd.Next(0, 1);
                    b = rnd.Next(0, 5);
                    c = rnd.Next(0, 5);
                    var _Cube = Instantiate(StandartCubes[c], new Vector3(CoordsX[b], CoordsY[a], StartZ), Quaternion.identity);
                    Moving.Add(_Cube);
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
        else if (Moving.Count == 0)
        {
            Walls.SetActive(true);
        }
        List<GameObject> list = new List<GameObject>();
        foreach (var cube in Moving)
        {
            if (cube.tag != "Destroy")
            {
                list.Add(cube);
            }
        }
        Moving.Clear();
        Moving = list;
        foreach (var cube in Moving)
        {
            cube.transform.position = new Vector3(cube.transform.position.x, cube.transform.position.y, cube.transform.position.z - (Time.deltaTime * Speed));
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
}
