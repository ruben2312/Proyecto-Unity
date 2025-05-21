using UnityEngine;

public class ManagerSonido : MonoBehaviour
{
    public static ManagerSonido instance{get; private set;}

    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    public void Reproducir(AudioClip sonido){
        audioSource.PlayOneShot(sonido);
    }

}
