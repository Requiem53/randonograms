using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquareManager : MonoBehaviour
{
    //ACCESSORS
    [SerializeField] private GridManager accessorGrid;
    [SerializeField] private RandomizeGrid accessorRandomize;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private MouseHeld mouseAccessor;
    [SerializeField] private GameObject _nonogramNumbers;
    [SerializeField] private int _width, _height;

    //LIST OF GRID INFO
    public List<Tile> indivData;
    public List<GameObject> squares; 
    public List<GameObject> highlights;
    public List<GameObject> numberList; 
    public List<GameObject> crosses;
    public List<TMPro.TextMeshPro> textList;
    public TMPro.TextMeshPro textMeshPro;
    public List<Tile> squareTiles;

    //FOR TILE ORGANIZATION
    [SerializeField] private Transform solutionParent;
    [SerializeField] private Transform nonogramParent;
    [SerializeField] private Vector3 tilePosition;

    //FOR UI
    [SerializeField] private Button randomizeButton;

    //FOR CHECKS
    [SerializeField] private CheckManager checker;

    //FOR CAMERA
    [SerializeField] private Transform _cam;

    //UNUSED LISTS
    [SerializeField] private List<int> splitUIX;
    [SerializeField] private List<int> splitUIY;
    [SerializeField] private List<int> _rowValue;
    [SerializeField] private List<int> _columnValue;

    private void Awake()
    {
        _width = accessorGrid._width;
        _height = accessorGrid._height;
    }

    void Start()
    {
        GenerateGrid();
        randomizeButton.onClick.AddListener(UIValueSetter);

    }
    void UIValueSetter()
    {
        splitUIX = accessorRandomize.splitterX;
        splitUIY = accessorRandomize.splitterY;
        _rowValue = accessorRandomize.rowValue;
        _columnValue = accessorRandomize.columnValue;
    }


    public void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                //TILES
                var spawnedTile = Instantiate(_tilePrefab, new Vector2(x, y), Quaternion.identity);
                spawnedTile.transform.parent = solutionParent;
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.myRow = x;
                spawnedTile.myColumn = y;

                indivData.Add(spawnedTile);
                squares.Add(spawnedTile.gameObject);
                highlights.Add(spawnedTile.transform.GetChild(0).gameObject);
                crosses.Add(spawnedTile.transform.GetChild(1).gameObject);
                squareTiles.Add(spawnedTile.GetComponent<Tile>());

                spawnedTile.GetComponent<Tile>().clickAdder = checker;
                spawnedTile.GetComponent<Tile>().mouseAccessor = mouseAccessor;

            }

        }
        //SETTING CAM POSITION
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }
}
