using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOff : MonoBehaviour
{
    [SerializeField] private Button m_TurnOffButton;
    [SerializeField] private SquareManager listAccessor;
    private List<GameObject> highlights = new List<GameObject>();
    private List<GameObject> crosses = new List<GameObject>();

    void Awake()
    {
        highlights = listAccessor.highlights;
        crosses = listAccessor.crosses;
    }

    private void Start()
    {
      
        m_TurnOffButton.onClick.AddListener(() => TaskOnClick(highlights, crosses));
    }

    void TaskOnClick(List<GameObject> highlights, List<GameObject> crosses)
    {
        Debug.Log("Grid deactivated");
        foreach (GameObject i in highlights)
        {
            i.SetActive(false);
        }
        foreach (GameObject i in crosses)
        {
            i.SetActive(false);
        }

    }      
}
