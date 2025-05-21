using UnityEngine;

public class ActivarTransicion : MonoBehaviour
{
    public Animator animador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (ModoJuego.desdeSeleccionNivel == false)
        {
            animador.SetTrigger("modo historia");
        }
    }

    
}
