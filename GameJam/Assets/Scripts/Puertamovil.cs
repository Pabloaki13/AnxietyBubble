using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puertamovil : MonoBehaviour
{
    Vector3 StartPos;
    Vector3 OpenPos;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
        OpenPos = new Vector3(-17.78f, 12.51202f,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // Presionar Espacio para activar el shake
        {
            StartCoroutine(OpenDoor());
        }
    }

    public void OpenDoorSequence()
    {
        StartCoroutine(OpenDoor());
    }

    public IEnumerator OpenDoor()
    {
        Vector3 originalPosition = transform.localPosition; // Guardar la posición original de la cámara
        float elapsed = 0f;

        Audio.instance.PlaySFX("doorbell");

        while (transform.position.x < OpenPos.x)
        {

            transform.position = transform.position + new Vector3(0.01f, 0, 0);

            elapsed += Time.deltaTime; // Incrementar el tiempo transcurrido
            yield return null; // Esperar al siguiente frame
        }
        while ( elapsed < duration)
        {
            elapsed += Time.deltaTime; // Incrementar el tiempo transcurrido
            yield return null; // Esperar al siguiente frame
        }
        while (transform.position.x > StartPos.x)
        {

            transform.position = transform.position - new Vector3(0.01f, 0, 0);
            elapsed += Time.deltaTime; // Incrementar el tiempo transcurrido
            yield return null; // Esperar al siguiente frame
        }


    }
    
}
