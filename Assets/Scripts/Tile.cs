using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

//EVENT ARGS FOR ROW AND COLUMN
[System.Serializable]
public class CalculateEvent : UnityEvent<int, int>
{

}

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _hoverColor;
    [SerializeField] private GameObject _crossed;
    [SerializeField] private GameObject _square;
    public int myRow, myColumn;

    public CalculateEvent m_squareAdded;
    public CalculateEvent m_squareSubtracted;
    public CheckManager clickAdder;

    //TESTING
    public MouseHeld mouseAccessor;
    private void Start()
    {
        //SUBSCRIBERS FROM CHECKMANAGER
        m_squareAdded.AddListener(clickAdder.AddBoxCounter);
        m_squareSubtracted.AddListener(clickAdder.SubtractBoxCounter);
    }
    //FOR HOVERING MOUSE OVER TILE
    void OnMouseEnter()
    {
        _hoverColor.SetActive(true);
    }

    void OnMouseExit()
    {
        _hoverColor.SetActive(false);
    }

    //CLICK SYSTEM
    public void OnMouseOver()
    {
        //FIX THE INPUTS TOMORROW
        //LEFT CLICK
        if (Input.GetMouseButton(0))
        {
            //FOR TURNING ON HIGHLIGHTS
            if (_highlight.activeInHierarchy != true && _crossed.activeInHierarchy != true && mouseAccessor.eventOccur == 1)
            {
                _highlight.SetActive(true);
                m_squareAdded?.Invoke(myRow, myColumn);
                clickAdder.LimitChecker();
                mouseAccessor.eventOccur = 0;
                if(mouseAccessor.eventOccur == 0)
                {
                    mouseAccessor.isHolding = true;
                    mouseAccessor.isErasing = false;
                }
            }
            //FOR HOLDING WHEN USING HIGHLIGHTS
            else if(mouseAccessor.eventOccur == 0 && mouseAccessor.isHolding && _highlight.activeInHierarchy != true && _crossed.activeInHierarchy != true)
            {
                _highlight.SetActive(true);
                m_squareAdded?.Invoke(myRow, myColumn);
                clickAdder.LimitChecker();
            } 

            //FOR ERASING ON HIGHLIGHTS
            if(_highlight.activeInHierarchy && mouseAccessor.eventOccur == 1)
            {
                _highlight.SetActive(false);
                mouseAccessor.eventOccur = 0;
                if (_crossed.activeInHierarchy != true)
                {
                    
                    m_squareSubtracted?.Invoke(myRow, myColumn);
                    mouseAccessor.eventOccur = 0;
                }
                if (mouseAccessor.eventOccur == 0)
                {
                    mouseAccessor.isHolding = false;
                    mouseAccessor.isErasing = true;
                }

            }
            //FOR HOLDING WHEN USING ERASER
            else if(mouseAccessor.eventOccur == 0 && mouseAccessor.isErasing && _highlight.activeInHierarchy)
            {
                _highlight.SetActive(false);
                if (_crossed.activeInHierarchy != true)
                {

                    m_squareSubtracted?.Invoke(myRow, myColumn);
                    mouseAccessor.eventOccur = 0;
                }
            }
            
            //FOR AVOIDING THE CROSSES
            if (_crossed.activeInHierarchy && mouseAccessor.eventOccur == 1)
            {
                _crossed.SetActive(false);
                mouseAccessor.eventOccur = 0;
            }

        }

        //RIGHT CLICK
        if (Input.GetMouseButton(1))
        {
            //FOR USING THE CROSSES
            if (_crossed.activeInHierarchy != true && _highlight.activeInHierarchy != true && mouseAccessor.eventOccurRight == 1)
            {
                _crossed.SetActive(true);
                _highlight.SetActive(false);
                mouseAccessor.eventOccurRight = 0;
                if(mouseAccessor.eventOccurRight == 0)
                {
                    mouseAccessor.isCrossDeleting = false;
                    mouseAccessor.isUnhighlighting = false;
                    mouseAccessor.isCrossing = true;
                }
            }
            //FOR HOLDING INPUT CROSSES
            else if(_crossed.activeInHierarchy != true && mouseAccessor.isCrossing && mouseAccessor.eventOccurRight == 0)
            {
                _crossed.SetActive(true);
                _highlight.SetActive(false);
            }
            
            //FOR DELETING THE CROSSES
            if (_crossed.activeInHierarchy && mouseAccessor.eventOccurRight == 1)
            {
                _crossed.SetActive(false);
                mouseAccessor.eventOccurRight = 0;
                if(mouseAccessor.eventOccurRight == 0)
                {
                    mouseAccessor.isCrossDeleting = true;
                    mouseAccessor.isUnhighlighting = false;
                    mouseAccessor.isCrossing = false;
                }
            }
            //FOR HOLDING DELETING CROSSES
            else if (_crossed.activeInHierarchy && mouseAccessor.eventOccurRight == 0 && mouseAccessor.isCrossDeleting)
            {
                _crossed.SetActive(false);
            }

            //FOR UNHIGHLITING THE SQUARES
            if (_highlight.activeInHierarchy && mouseAccessor.eventOccurRight == 1)
            {
                _highlight.SetActive(false);
                m_squareSubtracted?.Invoke(myRow, myColumn);
                mouseAccessor.eventOccurRight = 0;
                if(mouseAccessor.eventOccurRight == 0)
                {
                    mouseAccessor.isCrossDeleting = false;
                    mouseAccessor.isUnhighlighting = true;
                    mouseAccessor.isCrossing = false;
                }
            }
            else if (_highlight.activeInHierarchy && mouseAccessor.eventOccurRight == 0 && mouseAccessor.isUnhighlighting)
            {
                _highlight.SetActive(false);
                m_squareSubtracted?.Invoke(myRow, myColumn);
            }
        }

    }
}
/*
 * if (Input.GetMouseButtonDown(1))
            {
                if (_crossed.activeInHierarchy != true && _highlight.activeInHierarchy != true)
                {
                    _crossed.SetActive(true);
                    _highlight.SetActive(false);
                }
                else
                {
                    _crossed.SetActive(false);
                }
                if (_highlight.activeInHierarchy)
                {
                    _highlight.SetActive(false);
                    m_squareSubtracted?.Invoke(myRow, myColumn);
                }

            }
 * 
 *      if (Input.GetMouseButtonDown(0))
        {
            if (_highlight.activeInHierarchy != true && _crossed.activeInHierarchy != true)
            {
                _highlight.SetActive(true);
                m_squareAdded?.Invoke(myRow, myColumn);
                clickAdder.LimitChecker();
            }
            else
            {
                _highlight.SetActive(false);
                if (_crossed.activeInHierarchy != true)
                {
                    m_squareSubtracted?.Invoke(myRow, myColumn);
                }

            }

            if (_crossed.activeInHierarchy)
            {
                _crossed.SetActive(false);
            }

        }
true
 *             if (Input.GetMouseButton(0))
            {
                //HELD
                if (_highlight.activeInHierarchy != true && _crossed.activeInHierarchy != true)
                {
                    _highlight.SetActive(true);
                    m_squareAdded?.Invoke(myRow, myColumn);
                    clickAdder.LimitChecker();
                }
            }



*/