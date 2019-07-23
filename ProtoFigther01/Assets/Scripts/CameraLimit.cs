using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimit : MonoBehaviour
{
    public bool onLimit;

    // Start is called before the first frame update
    void Awake()
    {
        onLimit = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Compara el tag del hitbox, y tambien si pertenece a otra entidad.
        if(other.gameObject.CompareTag("Wall"))
        {
            onLimit = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Compara el tag del hitbox, y tambien si pertenece a otra entidad.
        if(other.gameObject.CompareTag("Wall"))
        {
            onLimit = false;
        }
    }
}
