using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecuenciaInicio : MonoBehaviour
{
    public float zoomDuration = 0.5f; // Duración del zoom (en segundos)
    public float maxSize = 0;
    public GameObject MainPlayer;
    public Vector3 startPosition;
    public GameObject TextBack;

    private float targetSize;       // Tamaño objetivo del zoom
    private float zoomSpeed = 0.5f;        // Velocidad calculada del zoom
    private bool isZooming = false; // Indica si está en proceso de zoom

    public GameObject CanvasMenuACtion;

    void Start()
    {
        // Inicializar el tamaño objetivo con el tamaño actual de la cámara
        targetSize = GetComponent<Camera>().orthographic ? GetComponent<Camera>().orthographicSize : GetComponent<Camera>().fieldOfView;
        isZooming = true;
        TextBack.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isZooming)
        {

            float currentSize = GetComponent<Camera>().orthographic ? GetComponent<Camera>().orthographicSize : GetComponent<Camera>().fieldOfView;
            float newSize = Mathf.MoveTowards(currentSize, maxSize, zoomSpeed * Time.deltaTime);

            transform.position = Vector3.Lerp(startPosition, new Vector3(0, 0, transform.position.z), newSize / maxSize);

            if (GetComponent<Camera>().orthographic)
                GetComponent<Camera>().orthographicSize = newSize;
            else
                GetComponent<Camera>().fieldOfView = newSize;

            // Detener el zoom si alcanzamos el tamaño objetivo
            if (Mathf.Approximately(newSize, maxSize))
            {
                isZooming = false;
                CanvasMenuACtion.SetActive(true);
            }
        }
    }
}
