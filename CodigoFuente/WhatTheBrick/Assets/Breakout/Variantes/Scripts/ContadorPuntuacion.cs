using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ContadorPuntuacion : MonoBehaviour
{
    public TextMeshProUGUI textoJ1, textoCPU;
    public GameObject cargadorNivel = null;

    public AudioSource muerte;
    public GameObject reproductorMusica;

    int puntosJ1=0, puntosCPU=0;
    float limite = 10f;

    float velocidad = 5f;

    public GameObject bola;

    public AudioSource golpe;
    Rigidbody2D rbBola;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbBola = bola.GetComponent<Rigidbody2D>();
        StartCoroutine(LanzarBola(1, 5.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if(bola.transform.position.x < -limite){
            puntosCPU++;
            textoCPU.text = puntosCPU.ToString();
            muerte.Play();
            bola.transform.position = new Vector2(-limite+2.8f, 0f);
            StartCoroutine(LanzarBola(1, 2f));
        }
        if(bola.transform.position.x > limite){
            puntosJ1++;
            textoJ1.text = puntosJ1.ToString();
            muerte.Play();
            bola.transform.position = new Vector2(limite-2.8f, 0f);
            StartCoroutine(LanzarBola(-1, 2f));
        }

        if(puntosCPU >= 6){
            Debug.Log("DERROTA");
        }
        if(puntosJ1 >= 6){
            Debug.Log("VICTORIA");
            if(reproductorMusica != null)
            {
                reproductorMusica.SetActive(false);
                cargadorNivel.GetComponent<CargadorNivel>().CargarSiguiente();
            }
        }
    }

    IEnumerator LanzarBola(int direccion, float wait){

        rbBola.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(wait);
        golpe.Play();
        int aux;
        if(Random.Range(0,2) == 1) aux = 1 ;else aux = -1;
        rbBola.linearVelocity = Vector2.up * aux * velocidad + Vector2.right * direccion * velocidad;
    }
}
