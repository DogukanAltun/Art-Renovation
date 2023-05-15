using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int[] wallLength;
    public int[] nodeLength;
    public GameObject nodes; 
    public Level[] levels;
    public Level level;
    public Material art;
    public  GameObject arts;
    public int LevelIndex;
    public int countLimit;
    public GameObject walls;
    public CanvasManager canvas;
    void Start()
    {
        arts = GameObject.FindGameObjectWithTag("Arts");
        nodes = GameObject.FindGameObjectWithTag("Nodes");
        canvas = FindObjectOfType<CanvasManager>();
        LevelIndex =  PlayerPrefs.GetInt("LevelIndex");
        if(LevelIndex >= 3)
        {
            LevelIndex = 0;
            PlayerPrefs.SetInt("LevelIndex", 0);
        }
        level = levels[LevelIndex];
        countLimit = level.CounterLimit;
        art = level.art;
        walls = GameObject.FindGameObjectWithTag("Walls");
        wallLength = level.WallLength;
        nodeLength = level.nodes;
        SetWalls();
        SetNodes();
        Cleaner.instance.CreateTexture(art);
    }

    public void SetWalls()
    {
        arts.transform.GetChild(LevelIndex).transform.gameObject.SetActive(true);
        for (int i = 0; i < wallLength.Length; i++)
        {
            int j = wallLength[i];
            GameObject wall = walls.transform.GetChild(j).gameObject;
            wall.SetActive(true);
        }
    }

    public void SetNodes()
    {
        for (int i = 0; i < nodeLength.Length; i++)
        {
            int j = nodeLength[i];
            GameObject node = nodes.transform.GetChild(j).gameObject;
            node.SetActive(true);
        }
    }
}
