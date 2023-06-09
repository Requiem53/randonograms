using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class AnswerTile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor, _highlightColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _hoverColor;
    [SerializeField] private GameObject _crossed;
    [SerializeField] private GameObject _square;
    public int myRow;
    public int myColumn;

    public void Init()
    {
        _renderer.color = _baseColor;
    }

    void OnMouseEnter()
    {
        _hoverColor.SetActive(true);
    }

    void OnMouseExit()
    {
        _hoverColor.SetActive(false);
    }


    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_highlight.activeInHierarchy != true && _crossed.activeInHierarchy != true)
            {
                //Debug.Log("Turned on: x: " + myRow + " y: " + myColumn);
                _highlight.SetActive(true);
            }

            else
            {
                //Debug.Log("Turned off: x: " + myRow + " y: " + myColumn);
                _highlight.SetActive(false);
            }

            if (_crossed.activeInHierarchy)
            {
                _crossed.SetActive(false);
            }

            Debug.Log("Left Clicks");

        }


        if (Input.GetMouseButtonDown(1))
        {
            //CHANGE IF CONDITIONS ON EVERY SITUATION
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
            }

            Debug.Log("Right Clicks");

        }
    }
}



/*
 public void DisableAllHighlights(bool OnOrOff)
 {
     if (OnOrOff)
     {
         _highlight.SetActive(false);
     }

     else
     {
         _highlight.SetActive(true);
     }
 }
 */
/*
         if (Input.GetMouseButtonDown(0))
        {

            Debug.Log("Left Clicks");

            if (_highlight.activeInHierarchy)
            {
                _highlight.SetActive(false);
            }

            else
            {
                _highlight.SetActive(true);
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
        
            Debug.Log("Right Clicks");
            if (_highlight.activeInHierarchy)
            {
                _highlight.SetActive(false);
            }
        }
        */