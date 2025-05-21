using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PuntuacionTracker : MonoBehaviour
{
    int ladrillos;

    public GameObject cargadorNivel = null;
    public GameObject reproductorMusica;

    public GameObject imagenBase;
    public GameObject ladrilloImg;
    public TextMeshProUGUI numLadrillos;
    public Gradient gradient;
    float velocidad = 0.75f;
    float anguloOG = 15f;

    public int puntosSeguidos = 0;

    public bool sombra;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ladrillos = FindFirstObjectByType<CrearNivel>().transform.childCount;
        numLadrillos.text = "x" + ladrillos;
        if(sombra){
            ladrilloImg.GetComponent<Image>().color = new UnityEngine.Color(0,0,0,0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float angulo = Mathf.SmoothStep(-anguloOG,anguloOG,Mathf.PingPong(Time.time * velocidad,1));
        imagenBase.transform.rotation = Quaternion.Euler(0,0,angulo);

        if(!sombra){
            ladrilloImg.GetComponent<Image>().color = gradient.Evaluate(Mathf.SmoothStep(0,1,Mathf.PingPong(Time.time * velocidad,1)));
        }
        
    }

    public void Decrementar(){
        ladrillos--;
        numLadrillos.text = "x" + ladrillos;
        //Debug.Log(ladrillos);
        if(ladrillos <= 0){
                Debug.Log("VICTORIA");
                if(reproductorMusica != null){
                    reproductorMusica.SetActive(false);
                }
                if(cargadorNivel != null){
                    cargadorNivel.GetComponent<CargadorNivel>().CargarSiguiente();
                }
                
            }
    }

    public int GetPuntosSeguidos(){
        return puntosSeguidos;
    }

    public void SetPuntosSeguidos(int s){
        puntosSeguidos = s;
    }

    public void sumarUno(){
        puntosSeguidos++;
    }
}
