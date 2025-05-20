using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Manager2 : MonoBehaviour
{
    public GameObject cargadorNivel = null;
    public List<Cesta> cestas;
    public GameObject suelo;
    private List<Collider> collidersSuelo = new List<Collider>();
    private Cesta cesta1;
    private Cesta cesta2;
    private Cesta cesta3;
     private float tiempoPasado = 0f; // Temporizador para controlar el tiempo
    private bool moverIzquierda = false; 
    private bool primeraVez = true;    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collidersSuelo.AddRange(suelo.GetComponents<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        tiempoPasado += Time.deltaTime;
        int cestasLlenas = 0;
        if(primeraVez && tiempoPasado >= 2f){
            tiempoPasado = 0;
            moverIzquierda = true;
            primeraVez = false;
        }

        if (tiempoPasado >= 4f && moverIzquierda == false){
            
            tiempoPasado = 0;
            moverIzquierda = true;
        }else
            if(tiempoPasado >= 4f && moverIzquierda == true){
                moverIzquierda = false;
                tiempoPasado = 0;
            }
        
        foreach (Cesta cesta in cestas)
        {
            if(cesta.transform.parent.name == "Cesta1")
                cesta1 = cesta;
            else if(cesta.transform.parent.name == "Cesta2")
                cesta2 = cesta;
            else
                cesta3 = cesta;
            
            if (cesta != null && cesta.CestaLlena) // <- revisa si cada cesta está llena
            {
                cestasLlenas++;
            }
        }
        if (cestasLlenas >= 3)
        {
            cargadorNivel.GetComponent<CargadorNivel>().CargarSiguiente();
        }

        if(moverIzquierda){
            primeraVez = false;
            suelo.transform.Translate(Vector3.left * Time.deltaTime);

            cesta1.transform.parent.Translate(Vector3.left * Time.deltaTime);
            cesta2.transform.parent.Translate(Vector3.left * Time.deltaTime);
            cesta3.transform.parent.Translate(Vector3.left * Time.deltaTime);
        }else{
            suelo.transform.Translate(Vector3.right * Time.deltaTime);

            cesta1.transform.parent.Translate(Vector3.right * Time.deltaTime);
            cesta2.transform.parent.Translate(Vector3.right * Time.deltaTime);
            cesta3.transform.parent.Translate(Vector3.right * Time.deltaTime);
        }

    }
}
