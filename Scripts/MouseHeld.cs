using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHeld : MonoBehaviour
{
    public float clickTimer;
    public int eventOccur = 1;
    public int eventOccurRight = 1;
    public bool isHolding, isErasing;
    public bool isUnhighlighting, isCrossing, isCrossDeleting;

    private void Start()
    {
        eventOccur = 1;
        eventOccurRight = 1;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            clickTimer += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            eventOccur = 1;
            isHolding = false; 
            isErasing = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            eventOccurRight = 1;
            isUnhighlighting = false;
            isCrossing = false;
            isCrossDeleting = false;
        }
    }
}
