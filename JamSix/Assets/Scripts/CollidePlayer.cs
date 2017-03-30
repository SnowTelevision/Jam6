using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidePlayer : MonoBehaviour
{
    public PlayerController player;
    public GameProcess gameManager;

	// Use this for initialization
	void Start ()
    {
        player = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameProcess>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameManager.isGameOver)
        {
            StopAllCoroutines();
            
            if (transform.parent != null)
            {

                if (transform.parent.parent != null)
                {
                    Destroy(transform.parent.parent.gameObject);
                }

                Destroy(transform.parent.gameObject);
            }

            else
            {
                Destroy(gameObject);
            }

            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "AirBody")
        {
            if (!player.isInvincible)
            {
                print("Gatlin hit player");
                gameManager.score--;
                //gameManager.scoreText.text = gameManager.score.ToString();
            }
        }
    }
}
