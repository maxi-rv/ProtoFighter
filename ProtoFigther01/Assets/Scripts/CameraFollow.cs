using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Player;
    private float speed;
    private Vector3 offset;
    private  float StageLimit;

    // Start is called before the first frame update
    void Awake()
    {
        speed = 0.1f;
        StageLimit = 9f;

        offset = transform.position - Player.transform.position;
    }

    //Late update is called after Update every frame
    void FixedUpdate()
    {
        Vector3 playerPosition = Player.transform.position + offset;
        Vector3 delayedPosition = Vector3.Lerp(transform.position, playerPosition, speed);
        
        float xpos = delayedPosition.x;
        float xdif = 0;

        if (xpos > StageLimit)
            xdif = xpos - StageLimit;

        if (xpos < -StageLimit)
            xdif = xpos + StageLimit;

        Vector3 nullifier = new Vector3(-xdif, -(delayedPosition.y),0);

        transform.position = delayedPosition + nullifier;
    }
}
