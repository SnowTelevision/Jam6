using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    public float attackDelay;
    public float laserRange;
    public float attackInterval;
    public GameObject bomb;
    public SpriteRenderer marker;
    public SelectWeapon attackPattern;

    public GameObject player;
    public int bombPattern;

    // Use this for initialization
    void Start()
    {

        attackPattern = GetComponentInParent<SelectWeapon>();
        //bombPattern = attackPattern.attackPattern % 3;
        //attackPattern.attackPattern = bombPattern;
        //attackPattern.weaponSymbol.color = attackPattern.patternColors[bombPattern];

        player = FindObjectOfType<PlayerController>().gameObject;

        if (bombPattern == 0) //+
        {
            marker.color = Color.red;
        }

        else if (bombPattern == 1) //X
        {
            marker.color = Color.blue;
        }

        else if (bombPattern == 2) //Circle
        {
            marker.color = Color.yellow;
        }

        StartCoroutine(shootBomb(attackDelay, attackInterval));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator shootBomb(float delay, float interval)
    {
        yield return new WaitForSeconds(delay);
        

        GameObject newBomb = Instantiate(bomb, transform.position, transform.rotation);

        newBomb.GetComponent<Bomb>().InitBomb(bombPattern, player.transform.position);
    }
}
