using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace DialogoDeltarune{
    public class SistemaDialogo : MonoBehaviour
    {

        public bool finished {get; private set;}
        public bool started {get; private set;}
        public bool next {get; private set;}
        protected IEnumerator EscribirTexto(string input, TextMeshProUGUI textoObjeto, Image fotoObjeto, TMP_FontAsset fuenteTexto, AudioClip sonidoSusie, AudioClip sonidoNarrador, AudioClip efectoSonido, Sprite cara, string voz, float autoskip, bool siguiente){

            textoObjeto.font = fuenteTexto;
            finished = false;
            started = false;
            next = siguiente;

            if(voz == "susie"){
                fotoObjeto.gameObject.SetActive(true);
                textoObjeto.rectTransform.offsetMin = new Vector2(-3.009996f, textoObjeto.rectTransform.offsetMin.y);
                fotoObjeto.sprite = cara;
            }else{
                fotoObjeto.gameObject.SetActive(false);
                textoObjeto.rectTransform.offsetMin = new Vector2(-60f, textoObjeto.rectTransform.offsetMin.y);
            }

            textoObjeto.text = "";

            if(efectoSonido != null){
                ManagerSonido.instance.Reproducir(efectoSonido);
            }

            for(int i=0; i<input.Length; i++){
                if(started && autoskip == 0f && (Input.GetMouseButton(0) || Input.GetButton("Jump"))){
                    textoObjeto.text += input.Substring(i);
                    i = input.Length;

                }else{
                    if(input[i] == '\\' && input[i+1] == 'n'){
                        if(voz == "gaster"){
                            yield return new WaitForSeconds(0.24f);
                        }
                        textoObjeto.text += "\n";
                        i++;
                    }else{
                        if(input[i] != ' '){
                            if(voz == "susie") ManagerSonido.instance.Reproducir(sonidoSusie);
                            else ManagerSonido.instance.Reproducir(sonidoNarrador);
                        }
                        textoObjeto.text += input[i];
                    }
                }
                
                
                yield return new WaitForSeconds(0.066f);
                started = true;
            }
            if(autoskip>0f){
                yield return new WaitForSeconds(autoskip);
            }
            else{
                yield return new WaitUntil(() => Input.GetMouseButton(0) || Input.GetButton("Jump"));
            }
            
            finished = true;
        }
    }
}

