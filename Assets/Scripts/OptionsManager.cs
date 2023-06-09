using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private Button FiveByFive, TenByTen, FifteenByFifteen, Custom;
    [SerializeField] private SizeManager sizeAccessor;
    private void Start()
    {
        FiveByFive.onClick.AddListener(() => SetValues(5, 5));
        TenByTen.onClick.AddListener(() => SetValues(10, 10));
        FifteenByFifteen.onClick.AddListener(() => SetValues(15, 15));
    }

    public void SetValues(int width, int height)
    {
        sizeAccessor.width = width;
        sizeAccessor.height = height;
        SceneManager.LoadScene(2);
    }
}
