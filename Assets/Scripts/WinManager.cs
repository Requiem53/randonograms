using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEditor.SearchService;

public class WinManager : MonoBehaviour
{
    [SerializeField] private Button changeSize;
    //[SerializeField] private GameObject settings;
    [SerializeField] private GameObject generalUI, chooseUI;
    [SerializeField] private Button FiveByFive, TenByTen, FifteenByFifteen, Custom;
    [SerializeField] private GridManager gridAccessor;
    [SerializeField] private DimensionsControl dimensionAccessor;
    public static int chosenWidth, chosenHeight;
    public static int onceUI = 1;
    private int customX, customY;

    //private UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();

    private void Awake()
    {
        /*
        if (chosenWidth == 0 || chosenHeight == 0)
        {
            chosenWidth = 2;
            chosenHeight = 4;
        }
        */
    }
    private void Start()
    {
        chooseUI.SetActive(false);
        Debug.Log("Before Start" + onceUI);
        if (onceUI >= 1)
        {
            chooseUI.SetActive(true);
            onceUI--;
            Debug.Log("After Start" + onceUI);
        }

        changeSize.onClick.AddListener(ChangeSize);

        FiveByFive.onClick.AddListener(() => SetValues(5, 5));
        TenByTen.onClick.AddListener(() => SetValues(10, 10));
        FifteenByFifteen.onClick.AddListener(() => SetValues(15, 15));
        Custom.onClick.AddListener(() => SetValues(StringParserX(dimensionAccessor.xInput), StringParserY(dimensionAccessor.yInput)));
        

    }

    private void ChangeSize()
    {
        //generalUI.SetActive(false);
        chooseUI.SetActive(true);
    }
    public void SetValues(int width, int height)
    {
        Debug.Log("Button pressed for " + width + "x"+ height);
        //DontDestroyOnLoad(this);
        chosenWidth = width;
        chosenHeight = height;
        chooseUI.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public int StringParserX(string x)
    {
        //x = dimensionAccessor.xInput;
        customX = int.Parse(x);
        return customX;
    }
    public int StringParserY(string y)
    {
        //y = dimensionAccessor.yInput;
        customY = int.Parse(y);
        return customY;
    }
}
