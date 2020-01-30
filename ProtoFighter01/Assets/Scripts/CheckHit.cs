using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHit : MonoBehaviour
{
    public bool isHurt;
    public string thisCharName;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Compara el tag del hitbox, y tambien si pertenece a otra entidad.
        if(other.gameObject.CompareTag("HitBox") && !(other.gameObject.GetComponentInParent<SpriteRenderer>().gameObject.name==thisCharName))
        {
            isHurt = true;
        }
    }
}
