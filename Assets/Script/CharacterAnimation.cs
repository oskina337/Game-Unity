using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("is run", true);      
              }
        else
    {
        anim.SetBool("is run", false);  
    }
    if (Input.GetKey(KeyCode.UpArrow)){
        anim.SetTrigger("jump");
    }
}
}
  