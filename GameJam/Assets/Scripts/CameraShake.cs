using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition; // Guardar la posición original de la cámara
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude; // Generar un desplazamiento aleatorio en X
            float y = Random.Range(-1f, 1f) * magnitude; // Generar un desplazamiento aleatorio en Y

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime; // Incrementar el tiempo transcurrido
            yield return null; // Esperar al siguiente frame
        }

        transform.localPosition = originalPosition; // Restaurar la posición original
    }
}
