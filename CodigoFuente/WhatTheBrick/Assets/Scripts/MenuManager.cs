using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    public Button Boton1;
    public Button Boton2;
    public Button Boton3;
    public GameObject opcionesPanel;
    public TextMeshProUGUI controlesText;
    public AudioMixer audioMixer;
    public Slider volumenSlider;
    public TextMeshProUGUI volumenTexto;

    public void Start()
    {
        Boton1.onClick.AddListener(Jugar);
        Boton2.onClick.AddListener(Opciones);
        Boton3.onClick.AddListener(SalirDelJuego);

        float volumenGuardado = PlayerPrefs.GetFloat("Volume", -30f);
        volumenSlider.value = volumenGuardado;
        CambiarVolumen(volumenGuardado);

        volumenSlider.onValueChanged.AddListener(CambiarVolumen);
    }
    public void Jugar()
    {

        Boton1.GetComponentInChildren<TextMeshProUGUI>().text = "Modo Historia";
        Boton2.GetComponentInChildren<TextMeshProUGUI>().text = "Seleccion de Niveles";
        var boton1Text = Boton1.GetComponentInChildren<TextMeshProUGUI>();
        var boton2Text = Boton2.GetComponentInChildren<TextMeshProUGUI>();
        boton1Text.fontSize = 18;
        boton2Text.fontSize = 16;
        Boton3.GetComponentInChildren<TextMeshProUGUI>().text = "ATRAS";

        Boton1.onClick.RemoveAllListeners();
        Boton2.onClick.RemoveAllListeners();
        Boton3.onClick.RemoveAllListeners();

        // Asignar nuevas funciones
        Boton1.onClick.AddListener(ModoHistoria);
        Boton2.onClick.AddListener(SeleccionarNivel);
        Boton3.onClick.AddListener(VolverAlMenuPrincipal);
    }
    private void ModoHistoria()
    {
        ModoJuego.desdeSeleccionNivel = false;
        SceneManager.LoadScene("Intro");
    }

    private void VolverAlMenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SeleccionarNivel()
    {
        SceneManager.LoadScene("Selector");
    }

    public void Opciones()
    {
        opcionesPanel.SetActive(true);       // Activar el panel de opciones
    }

     public void CambiarVolumen(float valor)
    {
        float volumenDB = Mathf.Lerp(-80f, 20f, valor);
        // De 0-1 lo pasamos a dB (-80 a 0)
        audioMixer.SetFloat("Volume", volumenDB);
        PlayerPrefs.SetFloat("Volume", valor);

        int porcentaje = Mathf.RoundToInt(((volumenDB + 80f) / 100f) * 100f);
        volumenTexto.text = $"{porcentaje}%";
    }

    public void DesactivarOpciones()
    {
        Debug.Log("Desactivando opciones...");
        opcionesPanel.SetActive(false); 
    }
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }


}