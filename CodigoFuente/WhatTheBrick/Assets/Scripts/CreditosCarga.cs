using System.Collections;
using UnityEngine;

public class CreditosCarga : MonoBehaviour
{
    Animator animador;
    public GameObject cargadorNivel = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animador = this.GetComponent<Animator>();
        StartCoroutine(EsperarAQueAcabe());
    }

    IEnumerator EsperarAQueAcabe()
    {
        yield return new WaitForSeconds(30f);
        cargadorNivel.GetComponent<CargadorNivel>().CargarSiguiente();
    }
}
