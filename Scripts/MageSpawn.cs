using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSpawn : MonoBehaviour
{
    public GameObject MainCharacterPrefabFire;
    public GameObject MainCharacterPrefabStone;
    public GameObject MainCharacterPrefabEnergy;
    public GameObject MainCharacterPrefabNecromancer;

    public bool Debug = false;
    public MageType debugMage = MageType.FireMage;
    
    private GameObject MainCharacter;

    void Awake()
    {
        MainCharacter = GameObject.FindGameObjectWithTag("Player");
        if (MainCharacter != null)
        {
            MainCharacter.transform.position = gameObject.transform.position;
        }
        else
        { 
            // Read PlayerPrefs file
            string strMagePrefab = PlayerPrefs.GetString("Mage", "FireMage");

            // Get prefab from string
            Enum.TryParse(strMagePrefab, out MageType mageTypePrefab);

            GameObject magePrefab = GetMagePrefab(mageTypePrefab);
            if (Debug)
            {
                magePrefab = GetMagePrefab(debugMage);
            }


            // Instantiate prefab
            MainCharacter = Instantiate(magePrefab, gameObject.transform.position, gameObject.transform.rotation);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (MainCharacter != null)
        {
            MainCharacter.transform.position = gameObject.transform.position;
        }

        Destroy(gameObject);
    }

    private GameObject GetMagePrefab(MageType mageType)
    {
        switch (mageType)
        {
            case MageType.FireMage:
                return MainCharacterPrefabFire;
            case MageType.StoneMage:
                return MainCharacterPrefabStone;
            case MageType.EnergyMage:
                return MainCharacterPrefabEnergy;
            case MageType.Necromancer:
                return MainCharacterPrefabNecromancer;
            default:
                return MainCharacterPrefabFire;
        }
    }

    public enum MageType
    {
        FireMage = 1,
        StoneMage,
        EnergyMage,
        Necromancer
    }
}
