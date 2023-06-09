using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    public int _width, _height;
    [SerializeField] private AnswerTile _tilePrefab;
    [SerializeField] private Transform _cam;
    public List<GameObject> squares;
    public List<GameObject> highlights = new List<GameObject>();
    [SerializeField] private Transform problemParent;
    [SerializeField] private GameObject problemSquares;
    [SerializeField] private WinManager winManager;

    void Awake()
    {
        //Change system later
        _width = WinManager.chosenWidth;
        _height = WinManager.chosenHeight;
        GenerateGrid();
    }
    //EXPERIMENTAL: CHANGE TILE TO ANSWERTILE
    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x-40, y-40), Quaternion.identity);
                spawnedTile.transform.parent = problemParent;
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.myRow = x;
                spawnedTile.myColumn = y;
                squares.Add(spawnedTile.gameObject);
                highlights.Add(spawnedTile.transform.GetChild(0).gameObject);
                spawnedTile.Init();
            }
            
        }
        problemSquares.SetActive(false);
    }

}