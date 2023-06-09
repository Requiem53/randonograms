using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjustment : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private int manipulateUI;
    void Start()
    {
        manipulateUI = 1;
        mainCamera.orthographicSize = ((gridManager._width + gridManager._height)/2 + manipulateUI);
    }

}
