using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlinBehavior : MonoBehaviour
{
    public float attackDelay;
    public SpriteRenderer marker;
    public SelectWeapon attackPattern;
    public GameObject player;
    public GameObject turret;
    
    public bool isAttacking;
    public GameObject turretOne;
    public GameObject turretTwo;
    public GameObject turretThree;
    public GameObject turretFour;
    public Vector3 newRotation;

    public float patternZeroSwitchTime;
    public float patternZeroRotationAngle;
    public float patternTwoSwitchTime;
    public float patternTwoRotationAngle;
    public bool patternTwoSwitch;
    public int patternTwoAngleCounter;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        isAttacking = false;

        attackPattern = GetComponentInParent<SelectWeapon>();

        if (attackPattern.attackPattern == 0) //Three turret
        {
            marker.color = Color.red;
            patternZeroRotationAngle = 0.5f;

            turretOne = Instantiate(turret, transform.position, transform.rotation, transform);

            Quaternion twoRotation = new Quaternion();
            twoRotation.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y + 120, transform.rotation.z);
            turretTwo = Instantiate(turret, transform.position, twoRotation, transform);

            Quaternion threeRotation = new Quaternion();
            threeRotation.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y + 240, transform.rotation.z);
            turretThree = Instantiate(turret, transform.position, threeRotation, transform);

            patternZeroSwitchTime = Time.time;
            patternTwoSwitchTime = Time.time;
        }

        else if (attackPattern.attackPattern == 1) //Four turret
        {
            marker.color = Color.blue;

            turretOne = Instantiate(turret, transform.position, transform.rotation, transform);

            Quaternion twoRotation = new Quaternion();
            twoRotation.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y + 90, transform.rotation.z);
            turretTwo = Instantiate(turret, transform.position, transform.rotation, transform);

            Quaternion threeRotation = new Quaternion();
            threeRotation.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
            turretThree = Instantiate(turret, transform.position, transform.rotation, transform);

            Quaternion fourRotation = new Quaternion();
            fourRotation.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y + 270, transform.rotation.z);
            turretFour = Instantiate(turret, transform.position, transform.rotation, transform);
        }

        else if (attackPattern.attackPattern == 2) //Two turret
        {
            marker.color = Color.yellow;
            patternTwoRotationAngle = 0.2f;
            patternTwoAngleCounter = 0;
            patternTwoSwitch = true;

            turretOne = Instantiate(turret, transform.position, transform.rotation, transform);
            turretTwo = Instantiate(turret, transform.position, transform.rotation, transform);
        }

        else if (attackPattern.attackPattern == 3) //One turret
        {
            marker.color = Color.green;
            turretOne = Instantiate(turret, transform.position, transform.rotation, transform);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(attackPattern.attackPattern == 3)
        {
            turretOne.transform.LookAt(player.transform);
        }

        else if(attackPattern.attackPattern == 1)
        {
            newRotation = turretOne.transform.eulerAngles;
            newRotation.y += 0.2f;
            turretOne.transform.eulerAngles = newRotation;
            newRotation.y += 90f;
            turretTwo.transform.eulerAngles = newRotation;
            newRotation.y += 90f;
            turretThree.transform.eulerAngles = newRotation;
            newRotation.y += 90f;
            turretFour.transform.eulerAngles = newRotation;
        }

        else if(attackPattern.attackPattern == 0)
        {

            if(Time.time - patternZeroSwitchTime >= 1.5f)
            {
                patternZeroSwitchTime = Time.time;
                patternZeroRotationAngle = -patternZeroRotationAngle;
            }

            newRotation = turretOne.transform.eulerAngles;
            newRotation.y += patternZeroRotationAngle;
            turretOne.transform.eulerAngles = newRotation;
            newRotation.y += 120f;
            turretTwo.transform.eulerAngles = newRotation;
            newRotation.y += 120f;
            turretThree.transform.eulerAngles = newRotation;
        }

        else if(attackPattern.attackPattern == 2)
        {
            if(Time.time - patternTwoSwitchTime >= 3)
            {
                patternTwoSwitchTime = Time.time;
                //patternTwoAngleCounter = 0;
                patternTwoSwitch = !patternTwoSwitch;
            }

            if (!patternTwoSwitch)
            {
                patternTwoAngleCounter++;
            }

            else
            {
                patternTwoAngleCounter--;
            }

            turretOne.transform.LookAt(player.transform);
            turretOne.transform.eulerAngles = new Vector3(
                turretOne.transform.eulerAngles.x, 
                turretOne.transform.eulerAngles.y + (patternTwoAngleCounter * patternTwoRotationAngle), 
                turretOne.transform.eulerAngles.z);

            turretTwo.transform.LookAt(player.transform);
            turretTwo.transform.eulerAngles = new Vector3(
                turretTwo.transform.eulerAngles.x,
                turretTwo.transform.eulerAngles.y - (patternTwoAngleCounter * patternTwoRotationAngle),
                turretTwo.transform.eulerAngles.z);
        }
    }
}
