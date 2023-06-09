using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeGrid : MonoBehaviour
{
    //Access
    [SerializeField] private GridManager listAccessor;
    [SerializeField] private SquareManager listAccessor2;
    [SerializeField] private CheckManager checkAccessor;
    //Accessed Variables
    //List
    [SerializeField] private List<GameObject> highlights = new List<GameObject>();
    [SerializeField] private List<GameObject> highlights2 = new List<GameObject>();

    [SerializeField] private List<Tile> tileCoords;
    [SerializeField] private List<GameObject> crosses;
    public List<int> rowValue, columnValue;

    //Ints
    private int width, height;
    private int xAxisTotal = 0, yAxisTotal = 0;

    //Extra
    [SerializeField] private Button m_RandomizeButton;
    [SerializeField] private int reshuffles;
    private int columnLocation = 0;
    private int rowLocation = 0;

    //FOR OFFSETS
    private int offsetCounterY = 0;
    private int offsetCounterX = 0;
    private int resetCounterX = 0;

    //FOR DUPLICATE NUMBERS IN UI
    public List<int> splitterX = new List<int>();
    private int splitterNumbersX;

    public List<int> splitterY = new List<int>();
    private int splitterNumbersY;

    public List<int> compareY = new List<int>();
    private int compareCounterY;

    public List<int> compareX = new List<int>();
    private int compareCounterX;

    public List<int> inputX = new List<int>();
    public List<int> inputY = new List<int>();
    //FOR UI
    [SerializeField] private GameObject _nonogramNumbers;
    [SerializeField] private Transform nonogramParentX;
    [SerializeField] private Transform nonogramParentY;
    [SerializeField] private List<GameObject> numberList;
    [SerializeField] private List<TMPro.TextMeshPro> textListX;
    [SerializeField] private List<TMPro.TextMeshPro> textListY;
    [SerializeField] private GameObject problemSquares;
    [SerializeField] private float largeFont;
    public int totalHighlights = 0;

    //UNUSED LISTS
    private List<GameObject> grids = new List<GameObject>();
    private List<GameObject> numbers = new List<GameObject>();
    private List<TMPro.TextMeshPro> textStorage = new List<TMPro.TextMeshPro>();

    void Awake()
    {
        //GRIDMANAGER = 1 
        //SQUAREMANAGER = 2
        tileCoords = listAccessor2.indivData;
        highlights = listAccessor.highlights;
        highlights2 = listAccessor2.highlights;
        width = listAccessor._width;
        height = listAccessor._height;
        crosses = listAccessor2.crosses;
        inputX = checkAccessor.inputX;
        inputY = checkAccessor.inputY;
        grids = listAccessor.squares;
        numbers = listAccessor2.numberList;
        textStorage = listAccessor2.textList;

        //ADD MORE OPTIONS
        reshuffles = width * height;
    }


    void Start()
    {
        GridRandomizer(highlights, reshuffles);
        SizeUpdater(width, height, tileCoords, rowValue, columnValue, highlights);
        m_RandomizeButton.onClick.AddListener(() => GridRandomizer(highlights, reshuffles));
        m_RandomizeButton.onClick.AddListener(() => SizeUpdater(width, height, tileCoords, rowValue, columnValue, highlights));
    }

    void GridRandomizer(List<GameObject> list, int shuffles)
    {
        inputX.Clear();
        inputY.Clear();
        //RANDOMIZES PUZZLES
        foreach (GameObject i in list)
        {
            i.SetActive(false);
        }
        //TEST CHANGE FALSE TO TRUE LATER
        Debug.Log("Randomized");
        for (int i = 0; i < shuffles; i++)
        {
            list[UnityEngine.Random.Range(0, list.Count)].SetActive(true);
        }

    }
    //REWORK THE ENTIRE LOGIC
    void SizeUpdater(int width, int height, List<Tile> list, List<int> xTotal, List<int> yTotal, List<GameObject> highlightList)
    {
        Debug.Log("Randomized on Start");

        problemSquares.SetActive(true);

        //Reset
        xTotal.Clear();
        yTotal.Clear();
        yAxisTotal = 0;
        xAxisTotal = 0;
        resetCounterX = 0;
        offsetCounterX = 0;
        offsetCounterY = 0;

        splitterNumbersX = 0;
        splitterX.Clear();

        splitterNumbersY = 0;
        splitterY.Clear();

        textListX.Clear();
        textListY.Clear();

        compareCounterY = 0;
        compareY.Clear();

        compareCounterX = 0;
        compareX.Clear();

        columnLocation = 0;
        rowLocation = 0;

        totalHighlights = 0;

        inputX.Clear();
        inputY.Clear();
        
        //CLEARS HIGHLIGHTS
        foreach (GameObject i in highlights2)
        {
            i.SetActive(false);
        }
        //CLEARS CROSSES
        foreach (GameObject i in crosses)
        {
            i.SetActive(false);
        }
        //DELETES TEXT TO BE REPLACED
        for (int i = 0; i < numberList.Count; i++)
        {
            Destroy(numberList[i]);
        }

        //Y AXIS TRY PUTTING IN A FUNCTION
        for (int x = 0; x < width * height; x++)
        {
            offsetCounterY++;
            //FOR COUNTING ENABLED ELEMENTS
            if (highlights[x].activeInHierarchy)
            {
                yAxisTotal++;
                compareCounterY++;
            }
            //TO COMPARE WITH PROBLEM
            if (highlights[x].activeInHierarchy == false)
            {
                
                if(compareCounterY > 0)
                {
                    compareY.Add(compareCounterY);
                    compareCounterY = 0;
                }
                
                compareCounterY = 0;
                compareY.Add(compareCounterY);
            }

            if (x == (width*height) - 1 && highlights[highlights.Count - 1].activeInHierarchy == true)
            {
                //Debug.Log("Called on: " + x);
                compareY.Add(compareCounterY);
                compareCounterY = 0;
            }
            //END
            //FOR COUNTING HIGHLIGHTS ON COLUMN
            if (highlights[x].activeInHierarchy == false && yAxisTotal != 0)
            {
                yTotal.Add(yAxisTotal);
                yAxisTotal = 0;
                splitterNumbersY++;
            }
            //FOR COUNTING HIGHLIGHTS ON COLUMN SEPARATION
            if (offsetCounterY % height == 0)
            {
                if (yAxisTotal != 0)
                {
                    yTotal.Add(yAxisTotal);
                    yAxisTotal = 0;
                    splitterNumbersY++;
                }
                splitterY.Add(splitterNumbersY);
                //FOR COUNTING ZEROES TO LIST
                if (yAxisTotal == 0 && splitterY[columnLocation] == 0)
                {
                    yTotal.Add(yAxisTotal);
                }
                splitterNumbersY = 0;
                columnLocation++;
            }
        }

        //X AXIS
        for (int x = 0; x < width * height; x += height)
        {
            offsetCounterX++;
            //FOR COUNTING ENABLED ELEMENTS
            if (highlights[x].activeInHierarchy)
            {
                xAxisTotal++;
                compareCounterX++;
            }
            //TO COMPARE WITH PROBLEM    NOTE: "offsetCounterX <= width * height" makes sure loop doesn't repeat on end
            if (highlights[x].activeInHierarchy == false && offsetCounterX <= width * height)
            {
                if (compareCounterX > 0)
                {
                    compareX.Add(compareCounterX);
                    compareCounterX = 0;
                }

                compareCounterX = 0;
                compareX.Add(compareCounterX);
            }

            if (x == (width * height) - 1 && highlights[highlights.Count - 1].activeInHierarchy == true && offsetCounterX <= width * height)
            {
                Debug.Log("Called on: " + x);
                compareX.Add(compareCounterX);
                compareCounterX = 0;
            }
            //END
            //FOR COUNTING HIGHLIGHTS ON ROW
            if (highlights[x].activeInHierarchy == false && xAxisTotal != 0 && offsetCounterX <= width * height)
            {
                xTotal.Add(xAxisTotal);
                xAxisTotal = 0;

                splitterNumbersX++;

            }
            //FOR COUNTING HIGHLIGHTS ON ROW SEPARATION
            if (offsetCounterX % width == 0 && offsetCounterX <= width * height)
            {
                if (xAxisTotal != 0)
                {
                    xTotal.Add(xAxisTotal);
                    xAxisTotal = 0;
                    splitterNumbersX++;
                }
                resetCounterX++;
                if (resetCounterX >= 1)
                {
                    //COMPENSATED
                    x = resetCounterX - height;
                }
                splitterX.Add(splitterNumbersX);
                //FOR ADDING ZEROES TO LIST
                if (xAxisTotal == 0 && splitterX[rowLocation] == 0)
                {
                    xTotal.Add(xAxisTotal);
                }
                splitterNumbersX = 0;
                rowLocation++;
            }

        }

        //Y AXIS TEXT PROCESSION
        for (int outerY = 0; outerY < splitterY.Count; outerY++)
        {
            //IF COLUMN IS BLANK
            if (splitterY[outerY] == 0 )
            {
                var spawnedNumber = Instantiate(_nonogramNumbers, new Vector2(outerY, height + 0.2f), Quaternion.identity, nonogramParentY);
                spawnedNumber.name = $"Text {outerY} 0 Number 0";
                numberList.Add(spawnedNumber);
                textListY.Add(spawnedNumber.GetComponent<TMPro.TextMeshPro>());
            }
            //PLACES TEXTS
            for (int innerY = 0; innerY < splitterY[outerY]; innerY++)
            {
                var spawnedNumber = Instantiate(_nonogramNumbers, new Vector2(outerY, height + innerY + 0.2f), Quaternion.identity, nonogramParentY);
                spawnedNumber.name = $"Text {outerY} {innerY} Number {innerY}";
                numberList.Add(spawnedNumber);
                textListY.Add(spawnedNumber.GetComponent<TMPro.TextMeshPro>());
            }
        }

        //X AXIS TEXT PROCESSION
        for (int outerX = 0; outerX < splitterX.Count; outerX++)
        {
            if (splitterX[outerX] == 0)
            {
                var spawnedNumber = Instantiate(_nonogramNumbers, new Vector2(0 - splitterX[outerX] - 0.01f - 1, outerX), Quaternion.identity, nonogramParentX);
                spawnedNumber.name = $"Text {outerX} 0 Number 0";
                numberList.Add(spawnedNumber);
                textListX.Add(spawnedNumber.GetComponent<TMPro.TextMeshPro>());
            }
            for (int innerX = 0; innerX < splitterX[outerX]; innerX++)
            {
                var spawnedNumber = Instantiate(_nonogramNumbers, new Vector2(innerX - splitterX[outerX] - 0.01f, outerX), Quaternion.identity, nonogramParentX);
                spawnedNumber.name = $"Text {outerX} {innerX} Number {innerX}";
                numberList.Add(spawnedNumber);
                textListX.Add(spawnedNumber.GetComponent<TMPro.TextMeshPro>());

            }
        }
        //X AXIS CHANGE FONTSIZE - ADD 100
        for (int i = 0; i < textListX.Count; i++)
        {
            textListX[i].text = "" + rowValue[i];
            if (rowValue[i] >= 10)
            {
                textListX[i].fontSize = largeFont;
            }
        }

        //Y AXIS CHANGE FONTSIZE - ADD 100
        for (int i = 0; i < textListY.Count; i++)
        {
            textListY[i].text = "" + columnValue[i];
            if (columnValue[i] >= 10)
            {
                textListY[i].fontSize = largeFont;
            }
        }
        //ADDING ALL HIGHLIGHTS
        for(int x = 0; x < width*height; x++)
        {
            if (highlights[x].activeInHierarchy)
            {
                totalHighlights++;
            }
        }
        //NEEDS TO BE THE VERY END
        //problemSquares.SetActive(false);

    }
    }



