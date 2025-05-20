using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace DialogoDeltarune{
    public class DialogoSecuencia : MonoBehaviour
    {
        public Image cajaTexto;
        public AudioSource audioSource, musica;
        public AudioClip latigazo, puerta, maquinaRuido;

        public Animator animador;

        public GameObject cargadorNivel;
        private void Awake()
        {
            cajaTexto = transform.GetComponent<Image>();
            StartCoroutine(secuenciaDialogo());
        }

        private IEnumerator secuenciaDialogo(){
            int contAnim = 0;
            
            

            for (int i = 0; i < transform.childCount - 1; i++)
            {
                
                desactivar();
                if (i == 0)
                {
                    cajaTexto.enabled = false;
                    yield return new WaitForSeconds(3.5f);
                    cajaTexto.enabled = true;
                }
                transform.GetChild(i).gameObject.SetActive(true);
                if (transform.GetChild(i).gameObject.CompareTag("Imagen"))
                {
                    cajaTexto.enabled = false;
                    yield return new WaitForSeconds(1f);
                    cajaTexto.enabled = true;
                }
                else
                {
                    yield return new WaitUntil(() => transform.GetChild(i).GetComponent<SistemaDialogo>().finished);

                    if (transform.GetChild(i).GetComponent<SistemaDialogo>().next)
                    {
                        contAnim++;
                        animador.SetInteger("Contador", contAnim);
                        if (contAnim == 1)
                        {
                            audioSource.PlayOneShot(latigazo);
                            musica.Stop();
                        }
                        if (contAnim == 2)
                        {

                            musica.clip = puerta;
                            musica.volume = 0.2f;
                            musica.Play();
                        }
                        if (contAnim == 3)
                        {
                            audioSource.PlayOneShot(maquinaRuido, 0.5f);
                        }
                    }
                }

            }
            
            cajaTexto.enabled = false;
            desactivar();
            yield return new WaitForSeconds(2f);

            cargadorNivel.GetComponent<CargadorNivel>().CargarSiguiente();
            
            gameObject.SetActive(false);
        }

        private void desactivar(){
            for(int i=0; i < transform.childCount; i++){
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}