using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camerazoom : MonoBehaviour
{
    public float zoomDuration = 0.5f; // Duraci�n del zoom (en segundos)
    public GameObject MainPlayer;

    private float targetSize;       // Tama�o objetivo del zoom
    private float zoomSpeed;        // Velocidad calculada del zoom
    private bool isZooming = false; // Indica si est� en proceso de zoom

    void Start()
    {
        // Inicializar el tama�o objetivo con el tama�o actual de la c�mara
        targetSize = GetComponent<Camera>().orthographic ? GetComponent<Camera>().orthographicSize : GetComponent<Camera>().fieldOfView;
    }

    void Update()
    {
        transform.position = new Vector3(MainPlayer.transform.position.x, MainPlayer.transform.position.y, transform.position.z);

        // Continuar el proceso de zoom si est� activo
        if (isZooming)
        {
            float currentSize = GetComponent<Camera>().orthographic ? GetComponent<Camera>().orthographicSize : GetComponent<Camera>().fieldOfView;
            float newSize = Mathf.MoveTowards(currentSize, targetSize, zoomSpeed * Time.deltaTime);

            if (newSize <= 1)
                SceneManager.LoadScene(4);

            if (GetComponent<Camera>().orthographic)
                GetComponent<Camera>().orthographicSize = newSize;
            else
                GetComponent<Camera>().fieldOfView = newSize;

            // Detener el zoom si alcanzamos el tama�o objetivo
            if (Mathf.Approximately(newSize, targetSize))
            {
                isZooming = false;
            }
        }
    }

    public void StartZoomIn(float newTargetSize)
    {
        if(!isZooming) {
        targetSize = targetSize + newTargetSize;
        float currentSize = GetComponent<Camera>().orthographic ? GetComponent<Camera>().orthographicSize : GetComponent<Camera>().fieldOfView;
        zoomSpeed = Mathf.Abs(currentSize - targetSize) / zoomDuration;
        isZooming = true;
            
        }
    }
    public void StartZoomOut(float newTargetSize)
    {
        targetSize = newTargetSize;
        float currentSize = GetComponent<Camera>().orthographic ? GetComponent<Camera>().orthographicSize : GetComponent<Camera>().fieldOfView;
        zoomSpeed = Mathf.Abs(currentSize - targetSize) / zoomDuration;
        isZooming = true;
    }
}
