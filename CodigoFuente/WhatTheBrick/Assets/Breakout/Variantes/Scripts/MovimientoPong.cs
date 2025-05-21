using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class MovimientoPong : MonoBehaviour
{

    public float maximo = 3.7f;

    float originalX;

    private Vector3 mousePosition;
	public float moveSpeed = 0.1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //movimientoHorizontal = Input.GetAxis("Horizontal");
        //transform.position += Vector3.right * movimientoHorizontal * velocidad * Time.deltaTime;
        mousePosition = Input.mousePosition;
        mousePosition = new Vector2(originalX, Camera.main.ScreenToWorldPoint(mousePosition).y);
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);

        if(Mathf.Abs(transform.position.y) > maximo){
            var pos = transform.position;
            pos.y = Mathf.Clamp(transform.position.y, -maximo, maximo);
            transform.position = pos;
        }


        
    }

    
}
