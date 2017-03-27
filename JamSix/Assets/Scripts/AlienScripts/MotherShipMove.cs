using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipMove : MonoBehaviour
{
    public Vector3 motherPosi;

	// Use this for initialization
	void Start ()
    {
        motherPosi = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void FixedUpdate()
    {
        motherPosi.z -= 0.005f;
        transform.position = motherPosi;
    }
}
