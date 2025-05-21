using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace DialogoDeltarune{
    public class DialogoLinea : SistemaDialogo
    {
        [Header("Datos diálogo")]
        [SerializeField]
        [TextArea(8,10)]private string input;
        
        [SerializeField] private Sprite talkSprite;
        [SerializeField]private string voz;
        [SerializeField]private float autoskip = 0f;
        [SerializeField]public bool siguiente = false;


        [Header("Datos gráficos")]
        [SerializeField]private Image fotoObjeto;

        [SerializeField]private TMP_FontAsset fuenteTexto;

        [SerializeField]private AudioClip sonidoSusie, sonidoNarrador, efectoSonido = null;
        private TextMeshProUGUI textoObjeto;

        private void Awake()
        {
            textoObjeto = GetComponent<TextMeshProUGUI>();

            
        }

        private void Start()
        {
            CajaDeTexto(input, talkSprite, voz);
        }

        private void CajaDeTexto(string inputTexto, Sprite talkSprite, string voz){
            StartCoroutine(EscribirTexto(inputTexto, textoObjeto, fotoObjeto, fuenteTexto, sonidoSusie, sonidoNarrador, efectoSonido, talkSprite, voz, autoskip, siguiente));
        }
    }
}