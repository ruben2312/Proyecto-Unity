using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public List<Cesta> cestas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject cargadorNivel = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int cestasLlenas = 0;
        
        foreach (Cesta cesta in cestas)
        {
            if (cesta != null && cesta.CestaLlena) // <- revisa si cada cesta está llena
            {
                cestasLlenas++;
            }
        }
        if (cestasLlenas >= 3)
        {
            Debug.Log("¡Hay tres cestas llenas! 🎉");
            cargadorNivel.GetComponent<CargadorNivel>().CargarSiguiente();
        }
    }
}
