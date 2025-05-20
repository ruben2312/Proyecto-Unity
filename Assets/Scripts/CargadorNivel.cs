using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargadorNivel : MonoBehaviour
{
    public Animator animador;
    public void CargarSiguiente()
    {
        if (ModoJuego.desdeSeleccionNivel == false)
        {
            if (SceneManager.GetActiveScene().name == "Menu")
            {
                StartCoroutine(CargarAnimacion("Intro"));
            }
            if (SceneManager.GetActiveScene().name == "Intro")
            {
                StartCoroutine(CargarAnimacion("Breakout OG"));
            }
            if (SceneManager.GetActiveScene().name == "Breakout OG")
            {
                StartCoroutine(CargarAnimacion("Pong"));
            }
            if (SceneManager.GetActiveScene().name == "Pong")
            {
                StartCoroutine(CargarAnimacion("NivelGallina"));
            }
            if (SceneManager.GetActiveScene().name == "NivelGallina")
            {
                StartCoroutine(CargarAnimacion("Pinball_1"));
            }
            if (SceneManager.GetActiveScene().name == "Pinball_1")
            {
                StartCoroutine(CargarAnimacion("NivelGallina2"));
            }
            if (SceneManager.GetActiveScene().name == "NivelGallina2")
            {
                StartCoroutine(CargarAnimacion("Breakout Tiros"));
            }
            if (SceneManager.GetActiveScene().name == "Breakout Tiros")
            {
                StartCoroutine(CargarAnimacion("NivelGallina3"));
            }
            if (SceneManager.GetActiveScene().name == "NivelGallina3")
            {
                StartCoroutine(CargarAnimacion("Pinball_2"));
            }
            if (SceneManager.GetActiveScene().name == "Pinball_2")
            {
                StartCoroutine(CargarAnimacion("NivelTanque"));
            }
            if (SceneManager.GetActiveScene().name == "NivelTanque")
            {
                StartCoroutine(CargarAnimacion("Final"));
            }
            if (SceneManager.GetActiveScene().name == "Final")
            {
                StartCoroutine(CargarAnimacion("Creditos"));
            }
            if (SceneManager.GetActiveScene().name == "Creditos")
            {
                StartCoroutine(CargarAnimacion("Menu"));
            }
        }else
            StartCoroutine(CargarAnimacion("Selector"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CargarAnimacion(string nivel){
        if (ModoJuego.desdeSeleccionNivel == false){
            
            animador.SetTrigger("start");

            yield return new WaitForSeconds(1f);
        }

        SceneManager.LoadScene(nivel);
    }
}
