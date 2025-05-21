using UnityEngine;

public class CrearNivel : MonoBehaviour
{

    public Vector2Int tamano = new Vector2Int(14,5);
    public Vector2 offset;
    public GameObject prefabLadrillo;
    public GameObject prefabLadrilloPowerup;

    public Gradient gradient;

    public int numPremios = 20;
    
    public Color colorEspecial = new UnityEngine.Color(1, 0.7825264f, 0.07075471f, 1);

    private void Awake()
    {
        bool[,] ladrilloPremiado = new bool[tamano.x, tamano.y];

        for(int a=0;a<numPremios;a++){
            int x = Random.Range(0, tamano.x-1), y = Random.Range(0, tamano.y-1);
            while(ladrilloPremiado[x, y]){
                x = Random.Range(0, tamano.x-1);
                y = Random.Range(0, tamano.y-1);
            }
            ladrilloPremiado[x, y] = true;
        }

        for(int i=0; i<tamano.x; i++){
            for(int j=0; j<tamano.y; j++){
                if(ladrilloPremiado[i, j]){
                    GameObject ladrilloNuevo = Instantiate(prefabLadrilloPowerup, transform);
                    ladrilloNuevo.transform.position = transform.position + new Vector3( (float) ((tamano.x-1)* 0.5f - i ) * offset.x, j*offset.y, 0);
                    ladrilloNuevo.GetComponent<SpriteRenderer>().color = colorEspecial;
                }
                else{
                    GameObject ladrilloNuevo = Instantiate(prefabLadrillo, transform);
                    ladrilloNuevo.transform.position = transform.position + new Vector3( (float) ((tamano.x-1)* 0.5f - i ) * offset.x, j*offset.y, 0);
                    ladrilloNuevo.GetComponent<SpriteRenderer>().color = gradient.Evaluate((float) j / (tamano.y - 1));
                }
                
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
