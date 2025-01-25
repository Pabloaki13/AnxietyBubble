using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuenciaInicio : MonoBehaviour
{
    public float zoomDuration = 0.5f; // Duraci�n del zoom (en segundos)
    public float maxSize = 0;
    public GameObject MainPlayer;
    public Vector3 startPosition;
    public GameObject TextBack;

    private float targetSize;       // Tama�o objetivo del zoom
    private float zoomSpeed = 0.5f;        // Velocidad calculada del zoom
    private bool isZooming = false; // Indica si est� en proceso de zoom
    // Start is called before the first frame update
    void Start()
    {
        // Inicializar el tama�o objetivo con el tama�o actual de la c�mara
        targetSize = GetComponent<Camera>().orthographic ? GetComponent<Camera>().orthographicSize : GetComponent<Camera>().fieldOfView;
        isZooming = true;
        TextBack.SetActive(false);
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

            // Detener el zoom si alcanzamos el tama�o objetivo
            if (Mathf.Approximately(newSize, maxSize))
            {
                isZooming = false;
                this.GetComponent<Dialogs>().type();
                TextBack.SetActive(true);
            }
        }
    }
}
