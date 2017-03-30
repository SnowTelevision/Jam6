using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explore : MonoBehaviour {
    private float timer;

    public PlayerController player;
    public GameProcess gameManager;

    // Use this for initialization
    void Start () {
        timer = 0;
        player = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameProcess>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 2) {
            Destroy(gameObject);
        }
	}
    void OnTriggerEnter(Collider other) {
        //hit
        if (other.name == "AirBody")
        {
            if (!player.isInvincible)
            {
                print("Bomb hit player");
                gameManager.score--;

            }
        }
    }
}

