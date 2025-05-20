using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaPlayer : MonoBehaviour
{

    float vida = 1f, daño = 0.2f;
    public GameObject vidaBarra;
    public GameObject bola;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Daño(){

        float escala = vidaBarra.transform.localScale.y;        
        
        float inicio = Time.time, actual = inicio, duracion = 2f;
        while(actual-inicio < duracion){

            float escalaTemp = Mathf.SmoothStep(escala, escala-daño, (actual-inicio)/duracion);
            actual = actual + Time.deltaTime;
            
            vidaBarra.transform.localScale = new Vector3(vidaBarra.transform.localScale.x, escalaTemp, vidaBarra.transform.localScale.z);
        }
        
        vida = escala-daño;
        vidaBarra.transform.localScale = new Vector3(vidaBarra.transform.localScale.x, vida, vidaBarra.transform.localScale.z);
        
        if(vida-daño <=0){
            bola.GetComponent<BolaRebote>().QuitarVida();
            vida = 1f;
            vidaBarra.transform.localScale = new Vector3(vidaBarra.transform.localScale.x, vida, vidaBarra.transform.localScale.z);
        }
    }


    void GameOver(){
        Debug.Log("Game over");
        string escenaActual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(escenaActual);
    }
}
