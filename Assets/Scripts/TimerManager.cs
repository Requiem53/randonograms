using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class TimerManager : MonoBehaviour
{
    private float seconds;
    private int minutes = 0;
    [SerializeField] private TMPro.TextMeshProUGUI timer;
    private int onceUI;
    [SerializeField] private CheckManager checkAccesor;
    //private int finishFlag = 0;
    // Start is called before the first frame update
    void Start()
    {
        //finishFlag = checkAccesor.finishFlag;
        onceUI = WinManager.onceUI;
        if (onceUI == 1)
        {
            timer.text = "00:00";
        }
        else
        {
            timer.text = minutes + ":" + seconds.ToString("F0");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        //minutes = (int)seconds / 60 +  ((int)seconds % 60);
        if (onceUI == 0 && checkAccesor.finishFlag != 1)
        {
            seconds += Time.deltaTime;
            if (seconds % 60 < 10)
            {
                timer.text = (int)seconds / 60 + ":" + (seconds % 60).ToString("0,0");
            }
            else
            {
                timer.text = (int)seconds / 60 + ":" + (seconds % 60).ToString("F0");
            }
        }
        
    }
}
