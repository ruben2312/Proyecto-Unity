using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image barraVida;  // La imagen que se rellena (tipo Filled o ajustar tamaño)
    public float vidaMaxima = 100f;
    public float vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarBarra();
    }

    public void QuitarVida(float cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual < 0) vidaActual = 0;
        ActualizarBarra();
    }

    void ActualizarBarra()
    {
        barraVida.fillAmount = vidaActual / vidaMaxima;
        // O si usas escala, algo como:
        // barraVida.transform.localScale = new Vector3(vidaActual / vidaMaxima, 1, 1);
    }
}