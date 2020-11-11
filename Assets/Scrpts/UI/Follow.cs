using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

	public Transform target;

    private void Update()
    {
        float sameX = target.position.x;
        Vector3 position = transform.position;
        position.x = sameX;
        transform.position = position;
    }

}