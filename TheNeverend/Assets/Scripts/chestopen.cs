using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestopen : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
       
    }
    // Start is called before the first frame update
    void OnTriggerStay2D (Collider2D col)
    {
        if (col.gameObject.tag == "Player" && Input.GetKeyDown("e") && playerMove.hasKey)
        {
            anim.SetBool("chesty", true);
            playerMove.hasKey = false;
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
