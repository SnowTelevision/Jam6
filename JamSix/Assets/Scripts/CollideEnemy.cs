using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideEnemy : MonoBehaviour
{
    public GameProcess gameManager;

	// Use this for initialization
	void Start ()
    {
        gameManager = FindObjectOfType<GameProcess>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameManager.isGameOver)
        {
            if (transform.parent != null)
            {

                if (transform.parent.parent != null)
                {
                    Destroy(transform.parent.parent.gameObject);
                }

                Destroy(transform.parent.gameObject);
            }

            StopAllCoroutines();
            Destroy(gameObject);
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("Collide: " + other.name + " " + transform.name);

        if((other.name == "Weapon" || other.name == "SmallWeapon") && transform.name == "Bullet")
        {
            if (!other.GetComponent<SelectWeapon>().isInvinsible)
            {
                other.GetComponent<SelectWeapon>().weaponHP--;
            }

            Destroy(transform.parent.gameObject);
        }
    }
}
