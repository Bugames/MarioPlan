using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoleText : MonoBehaviour
{
    public GameObject role;

    private void Start()
    {
        Debug.Log(GetComponent<CircleCollider2D>().radius);
    }
}

