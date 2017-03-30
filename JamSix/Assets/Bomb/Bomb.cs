using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    public float exploreTime;
    public int exploreType;
    public GameObject[] exploreEffect;
    public Vector3 dir;
    public float speed;

	// Use this for initialization
	void Start () {
        if (exploreType == 0)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (exploreType == 1) {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (exploreType == 2)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }

    }
	
	// Update is called once per frame
	void Update () {
        transform.position += (dir - transform.position).normalized*speed*Time.deltaTime;
        exploreTime -= Time.deltaTime;
        if (exploreTime <= 0) {
            Instantiate(exploreEffect[exploreType], transform.position, exploreEffect[exploreType].transform.rotation);
            Destroy(gameObject);
        }
	}
    public void InitBomb(int type,Vector3 direction) {
        exploreType = type;
        dir = direction;
    }
}
