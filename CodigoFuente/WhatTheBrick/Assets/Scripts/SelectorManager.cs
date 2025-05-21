using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectorManager : MonoBehaviour
{
    public Button Boton_Breakout;
    public Button Boton_Pong;
    public Button Boton_Gallina;
    public Button Boton_Pinball;
    public Button Boton_Tanque;
    public Button Boton_Atras;

    public GameObject PanelDificultadGallina;
    public GameObject PanelDificultadBreakout;
    public GameObject PanelDificultadPinball;

    void Start()
    {
        Boton_Breakout.onClick.AddListener(() => PanelDificultadBreakout.SetActive(true));
        Boton_Pong.onClick.AddListener(Pong);
        Boton_Gallina.onClick.AddListener(() => PanelDificultadGallina.SetActive(true));
        Boton_Pinball.onClick.AddListener(() => PanelDificultadPinball.SetActive(true));
        Boton_Tanque.onClick.AddListener(Tanque);
        Boton_Atras.onClick.AddListener(Atras);
    }

    // Estos se llaman desde los botones dentro de cada panel de dificultad
    public void GallinaFacil()
    {
        ModoJuego.desdeSeleccionNivel = true;
        SceneManager.LoadScene("NivelGallina");
    }
    public void GallinaNormal()
    {
        ModoJuego.desdeSeleccionNivel = true;
        SceneManager.LoadScene("NivelGallina2");
    }
    public void GallinaDificil()
    {
        ModoJuego.desdeSeleccionNivel = true;
        SceneManager.LoadScene("NivelGallina3");
    }

    public void Breakout1()
    {
        ModoJuego.desdeSeleccionNivel = true;
        SceneManager.LoadScene("Breakout OG");
    }
    public void Breakout2()
    {
        ModoJuego.desdeSeleccionNivel = true;
        SceneManager.LoadScene("Breakout Tiros");
    }

    public void Pinball1()
    {
        ModoJuego.desdeSeleccionNivel = true;
        SceneManager.LoadScene("Pinball_1");
    }
    public void Pinball2()
    {
        ModoJuego.desdeSeleccionNivel = true;
        SceneManager.LoadScene("Pinball_2");
    }

    public void Pong()
    {
        ModoJuego.desdeSeleccionNivel = true;
        SceneManager.LoadScene("Pong");
    }
    public void Tanque()
    {
        ModoJuego.desdeSeleccionNivel = true;
        SceneManager.LoadScene("NivelTanque");
    }
    public void Atras()
    {
        ModoJuego.desdeSeleccionNivel = true;
        SceneManager.LoadScene("Menu");
    }

    // Método para cerrar paneles si el usuario se arrepiente
    public void CerrarPanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
