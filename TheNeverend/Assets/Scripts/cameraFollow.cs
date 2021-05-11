using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform player;
    public float cameraDistance = 3.0f;
    

    void Avake ()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height/ 2) / cameraDistance);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y + 1f, transform.position.z);
    }
}
