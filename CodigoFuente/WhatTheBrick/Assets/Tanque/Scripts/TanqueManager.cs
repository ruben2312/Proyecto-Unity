using UnityEngine;
using System.Collections;

public class TanqueManager : MonoBehaviour
{

    public GameObject Barra_Vida;
    public GameObject Barra_Vida2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (ModoJuego.desdeSeleccionNivel == false)
            StartCoroutine(CongelarCoroutine(4f));
        else
        {
            Barra_Vida.SetActive(true);
            Barra_Vida2.SetActive(true);
        }
    }

    // Update is called once per frame
    private IEnumerator CongelarCoroutine(float duracion)
    {
        yield return new WaitForSeconds(duracion);  
        Barra_Vida.SetActive(true);
        Barra_Vida2.SetActive(true);
    }
}
