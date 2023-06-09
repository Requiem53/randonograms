using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField] private GameObject _highlight;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _highlight.SetActive(true);

        if (Input.GetMouseButtonDown(1))
            _highlight.SetActive(false);
    }
}
