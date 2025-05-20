using UnityEngine;

public class Escudo : MonoBehaviour
{
    public float distanciaMovimiento = 1f;     // Distancia vertical del movimiento
    public float duracionMovimiento = 0.5f;      // Duración del movimiento hacia arriba o abajo

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
        StartCoroutine(MoverEscudo());
    }

    System.Collections.IEnumerator MoverEscudo()
    {
        while (true)
        {
            // Subir
            yield return StartCoroutine(MoverA(posicionInicial + Vector3.up * distanciaMovimiento, duracionMovimiento));

            // Bajar
            yield return StartCoroutine(MoverA(posicionInicial, duracionMovimiento));
        }
    }

    System.Collections.IEnumerator MoverA(Vector3 destino, float duracion)
    {
        Vector3 inicio = transform.position;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            transform.position = Vector3.Lerp(inicio, destino, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        transform.position = destino;
    }
}

