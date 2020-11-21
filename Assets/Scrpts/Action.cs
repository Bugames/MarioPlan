using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Action : MonoBehaviour
{
    // Start is called before the first frame update

    bool a;//attack
    bool b;//quickattack
    bool c;
    bool d;
    int count = 0;
    float start;
    float end;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            start = Time.time;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            count++;
            if (count > 30)
            {
                position.y = (float)-0.45;
                a = true;
                animator.SetBool("Attack", a);
                Debug.Log("长按");
            }
            
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            end = Time.time;
            count = 0;
            if ((end - start) < 0.3)
            {
                position.y = (float)-0.45;
                b = true;
                animator.SetBool("QuickAttack", b);
                Debug.Log("短按");
            }

        }
        transform.position = position;
    }
    
}
