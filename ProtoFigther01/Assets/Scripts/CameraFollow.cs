using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public List<Transform> Players;
    public Vector3 offset;
    private float speed;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Awake()
    {
        speed = 0.1f;
    }

    //Late update is called after Update every frame
    void FixedUpdate()
    {
        var boundsMove = new Bounds(Players[0].position, Vector3.zero);

        for (int i=0; i<Players.Count; i++)
        {
            boundsMove.Encapsulate(Players[i].position);
        }

        Vector3 centerPosition = boundsMove.center;
        Vector3 desiredPosition = centerPosition+offset;

        //Moves the camera
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, speed);
    }
}
