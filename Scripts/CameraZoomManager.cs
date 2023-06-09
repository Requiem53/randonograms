using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomManager : MonoBehaviour
{
    //CHANGE TOMORROW
    public GridManager gridManager;    
    private Camera mainCam;

    private float gridWidth, gridHeight;

    private float targetZoom;

    [SerializeField] private float zoomFactor;
    [SerializeField] private float zoomLerpSpeed; 
    private float scrollData;

    [SerializeField]private Vector3 currentCamPosition;

    [SerializeField] private float arrowFactor;

    private int onceUI;

    // Start is called before the first frame update
    private void Awake()
    {
        mainCam = Camera.main;
    }
    void Start()
    {
        onceUI = WinManager.onceUI;
        gridWidth = gridManager._width;
        gridHeight = gridManager._height;

        targetZoom = mainCam.orthographicSize;

        currentCamPosition = mainCam.transform.position;
        

    }

    // Update is called once per frame
    void Update()
    {
        CameraScrollMovement();
        WASDCameraMovement();       
    }
    private void WASDCameraMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(arrowFactor * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-arrowFactor * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, -arrowFactor * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, arrowFactor * Time.deltaTime, 0));
        }

        if (Input.GetKey(KeyCode.Space))
        {
            mainCam.transform.position = currentCamPosition;
        }
    }
   private void CameraScrollMovement()
    {
        scrollData = Input.GetAxis("Mouse ScrollWheel");

        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, (gridWidth + gridHeight) / 4, gridWidth + gridHeight);

        if (onceUI == 0)
        {
            mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
        }

    }
}
