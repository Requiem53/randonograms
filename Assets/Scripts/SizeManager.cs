using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SizeManager : MonoBehaviour
{
    public int width, height;
    public CalculateEvent valuesGiven;

    void Start()
    {
        DontDestroyOnLoad(this);
    }
    void OnSceneLoaded()
    {
        valuesGiven?.Invoke(width, height);
    }



}
