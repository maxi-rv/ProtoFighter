using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEverything : MonoBehaviour
{
BoxCollider2D hitBox;

    // Start is called before the first frame update
    void Awake()
    {
        hitBox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        hitBox.enabled = false;
        
        bool boton = Input.GetButton("DamageAll");

        if (boton)
        {
            hitBox.enabled = true;
        }
    }
}
