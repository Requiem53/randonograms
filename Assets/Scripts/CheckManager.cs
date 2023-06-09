using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CheckManager : MonoBehaviour
{
    //ACCESSORS
    [SerializeField] private RandomizeGrid randomAccessor;
    [SerializeField] private SquareManager squareAccessor;
    [SerializeField] private GridManager gridAccessor;
    //COMPONENTS
    [SerializeField] private Button retryButton, randomizeButton;
    [SerializeField] private TMPro.TextMeshProUGUI counterText, winText;
    [SerializeField] private Button changeSize;
    private int width = 0;
    private int height = 0;
    //FOR COMPARING
    [SerializeField] private List<int> answersX, answersY;
    public List<int> inputX, inputY;
    [SerializeField] private List<GameObject> highlights;
    private int compareCounterX = 0;
    private int compareCounterY = 0;
    private int offsetCounterX = 0;
    private int resetCounterX = 0;
    [SerializeField] private bool isEqualX;
    [SerializeField] private bool isEqualY;
    //FOR SYSTEMS
    public GameObject textAccessor;
    public CheckManager testSystem;
    private int boxCounter = 0;
    [SerializeField] private int totalHighlights = 0;

    private delegate void LimitDetect();
    private event LimitDetect LimitDetected;

    //FOR SUBSCRIBGIN
    //FOR TIMER
    public int finishFlag = 0;

    private void OnEnable()
    {
        LimitDetected += CompareAnswer;
    }
    private void OnDisable()
    {
        LimitDetected -= CompareAnswer;
    }

    void Start()
    {
        counterText = textAccessor.GetComponent<TMPro.TextMeshProUGUI>();
        CountHighlight();
        //REFERENCE TO ANSWERS
        answersX = randomAccessor.compareX;
        answersY = randomAccessor.compareY;
        highlights = squareAccessor.highlights;
        width = gridAccessor._width;
        height = gridAccessor._height;
        //SUBSCRIBERS
        randomizeButton.onClick.AddListener(CountHighlight);
        retryButton.onClick.AddListener(ResetBoxCounter);
        randomizeButton.onClick.AddListener(ResetBoxCounter);
        //FOR UI DISABLING AFTER RESETS
        randomizeButton.onClick.AddListener(() => ResetUI(winText, changeSize));
        retryButton.onClick.AddListener(() => ResetUI(winText, changeSize));
        //TEST SYSTEM
        //testSystem.SpaceDetected += PrintMessage;
        finishFlag = 0;
    }
    //PROCESSES COUNTING
    public void ResetUI(TMPro.TextMeshProUGUI textTBCleared, Button changeButton)
    {
        textTBCleared.gameObject.SetActive(false);
        changeButton.gameObject.SetActive(false);
    }
    public void LimitChecker()
    {
        if(boxCounter == totalHighlights)
        {
            LimitDetected?.Invoke();
        }
    }
    public void AddBoxCounter(int row, int column)
    {
        boxCounter++;
        counterText.text = "Counter: " + boxCounter;
    }

    public void SubtractBoxCounter(int row, int column)
    {
        boxCounter--;
        counterText.text = "Counter: " + boxCounter;
    }

    public void ResetBoxCounter()
    {
        boxCounter = 0;
        counterText.text = "Counter: " + boxCounter;
    }

    //FOR ANSWERS
    public void CountHighlight()
    {
        totalHighlights = randomAccessor.totalHighlights;
    }
    public void CompareAnswer()
    {
        //REWORK LATER
        Debug.Log("Time to check for answer");
        //RESET
        compareCounterY = 0;
        compareCounterX = 0;
        inputY.Clear();
        inputX.Clear();
        offsetCounterX = 0;
        resetCounterX = 0;
        winText.gameObject.SetActive(false);
        changeSize.gameObject.SetActive(false);
        //Y AXIS
        for (int x = 0; x < width*height; x++)
        {
            if (highlights[x].activeInHierarchy)
            {
                compareCounterY++;
            }
            if (highlights[x].activeInHierarchy == false)
            {
                if (compareCounterY > 0)
                {
                    inputY.Add(compareCounterY);
                    compareCounterY = 0;
                }
                compareCounterY = 0;
                inputY.Add(compareCounterY);
            }

            if (x == (width * height) - 1 && highlights[highlights.Count - 1].activeInHierarchy == true)
            {
                inputY.Add(compareCounterY);
                compareCounterY = 0;
            }
        }

        //X AXIS
        for (int x = 0; x < width * height; x += height)
        {
            Debug.Log("on position: " + x);
            offsetCounterX++;
            if (highlights[x].activeInHierarchy)
            {
                compareCounterX++;
            }
            if (highlights[x].activeInHierarchy == false && offsetCounterX <= width * height)
            {
                if (compareCounterX > 0)
                {
                    inputX.Add(compareCounterX);
                    compareCounterX = 0;
                }

                compareCounterX = 0;
                inputX.Add(compareCounterX);
            }

            if (x == (width * height) - 1 && highlights[highlights.Count - 1].activeInHierarchy == true && offsetCounterX <= width * height)
            {
                Debug.Log("Called on: " + x);
                inputX.Add(compareCounterX);
                compareCounterX = 0;
            }

            if (offsetCounterX % width == 0 && offsetCounterX <= width * height)
            {
                resetCounterX++;
                if (resetCounterX >= 1)
                {
                    //COMPENSATED
                    x = resetCounterX - height;
                }
            }

        }
        isEqualX = inputX.SequenceEqual(answersX);
        isEqualY = inputY.SequenceEqual(answersY);
        if(isEqualX && isEqualY)
        {
            Debug.Log("You won!");
            winText.gameObject.SetActive(true);
            changeSize.gameObject.SetActive(true);
            finishFlag = 1;
        }

    }








    //TESTING SYSTEM
    //TRY REFERENCING THE LIST
    //TESTING SYSTEM
    /*
    public delegate void SpaceDetect();
    public event SpaceDetect SpaceDetected;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceDetected?.Invoke();
        }
    }
    private void OnDisable()
    {
        testSystem.SpaceDetected -= PrintMessage;
    }
    void PrintMessage()
    {
        Debug.Log("Message Printed");
    }
    */
}
