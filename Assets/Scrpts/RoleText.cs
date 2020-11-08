using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoleText : MonoBehaviour
{
    public GameObject role;

    private void Update()
    {
        role.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, 0.0f);
        Debug.Log(role.GetComponent<Rigidbody2D>().velocity);
    }
}
