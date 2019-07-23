using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public bool isGrounded;
    private Animator animator;

    void Awake()
    {
        //GameObject Sprite = gameObject.transform.Find("Sprite").gameObject;
        //GameObject Sprite = gameObject.GetComponentInParent<Sprite>().gameObject.transform.Find("Sprite").gameObject;
        animator = gameObject.GetComponentInParent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }
}
