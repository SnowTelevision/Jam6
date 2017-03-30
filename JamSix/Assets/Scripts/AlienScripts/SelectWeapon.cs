using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;

public class SelectWeapon : MonoBehaviour
{
    public GameObject[] weaponTypes;
    public SpriteRenderer[] weaponMarkers;
    public Color[] patternColors;
    public int weaponHP;

    public int weaponType;
    public int attackPattern;
    public bool weaponGenerated;
    public bool isInvinsible;
    public GameObject thisWeapon;
    public SpriteRenderer weaponSymbol;

	// Use this for initialization
	void Start ()
    {
        weaponType = betterRandom(0, weaponTypes.Length - 1);
        //weaponType = 2;//Change weaponType to test different weapon types
        attackPattern = betterRandom(0, 3);

        thisWeapon = Instantiate(weaponTypes[weaponType], new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation, transform);

        if(weaponType == 2)
        {
            thisWeapon.GetComponent<BombBehavior>().bombPattern = attackPattern % 3;
            attackPattern = thisWeapon.GetComponent<BombBehavior>().bombPattern;
        }

        Quaternion symbolRotation = new Quaternion();
        symbolRotation.eulerAngles = new Vector3(90, 0, 0);
        weaponSymbol = Instantiate(weaponMarkers[weaponType], new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), symbolRotation, transform);

        weaponSymbol.color = patternColors[attackPattern];

        weaponGenerated = false;
        isInvinsible = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //print(transform.position.z);

        if(transform.position.z <= -240.4 && !weaponGenerated)
        {
            weaponGenerated = true;
            thisWeapon.SetActive(true);
            isInvinsible = false;
        }

        if(weaponHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    #region Better random number generator

    public readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();

    public int betterRandom(int minimumValue, int maximumValue)
    {
        byte[] randomNumber = new byte[1];

        _generator.GetBytes(randomNumber);

        double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

        // We are using Math.Max, and substracting 0.00000000001, 
        // to ensure "multiplier" will always be between 0.0 and .99999999999
        // Otherwise, it's possible for it to be "1", which causes problems in our rounding.
        double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

        // We need to add one to the range, to allow for the rounding done with Math.Floor
        int range = maximumValue - minimumValue + 1;

        double randomValueInRange = Math.Floor(multiplier * range);

        return (int)(minimumValue + randomValueInRange);
    }
    #endregion
}
