using System;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public List<GameObject> flife = new List<GameObject>();
    public List<GameObject> hlife = new List<GameObject>();
    public List<GameObject> elife = new List<GameObject>();

    public GameObject fullHeart;
    public GameObject halfHeart;
    public GameObject emptyHeart;
    //private static Vector3 camPos;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            CharacterStats cs = player.GetComponent<CharacterStats>();

            int j = 0;
            for (int i = 2; i <= cs.Health; i = i + 2)
            {
                newFullHeartContainer(j++);
            }

            if (cs.Health % 2 > 0)
            {
                newHalfHeartContainer(j++);
            }

            for (int i = cs.Health; i < cs.MaxHealth; i = i + 2)
            {
                newEmptyHeartContainer(j++);
            }
        }
    }

    public void Death_HeartContainer()
    {

        for (int i = 0; i < flife.Count; i++)
        {
            flife[i].SetActive(false);
            hlife[i].SetActive(false);
            elife[i].SetActive(false);
            /*Destroy(flife[i]);
            Destroy(hlife[i]);
            Destroy(elife[i]);*/
        } 
    }

    public void Extent_MaxHeartcontainer()
    {
        if (player != null)
        {
            CharacterStats cs = player.GetComponent<CharacterStats>();

            if (cs.HudHearts <= 20)
            {
                int j = 0;

                for (int i = 2; i < cs.MaxHealth; i = i + 2)
                {
                    j++;
                }
                newEmptyHeartContainer(j);
            }
            
            Heal_HeartContainer();
        }

    }

    public void Damage_HeartContainer()
    {
        if (player != null)
        {
            CharacterStats cs = player.GetComponent<CharacterStats>();

            if (cs.Health < 40)
            {

                int damaged;
                if (cs.MaxHealth > 40)
                {
                    damaged = 40 - cs.Health;
                } else
                {
                    damaged = cs.MaxHealth - cs.Health;
                }

                int heartPos = (hlife.Count - 1);
                for (int i = 0; i < damaged; i++)
                {
                    if (i % 2 == 0)
                    {
                        flife[heartPos].SetActive(false);
                        hlife[heartPos].SetActive(true);
                    } else
                    {
                        hlife[heartPos].SetActive(false);
                        heartPos--;
                    }
                }
            }
        }
    }

    public void Heal_HeartContainer()
    {
        CharacterStats cs = player.GetComponent<CharacterStats>();
        Debug.Log($"player hp: {cs.Health}");


        for (int i = 0; i < flife.Count; i++)
        {
            hlife[i].SetActive(false);
            flife[i].SetActive(false);
        }


        int heartPos = 0;
        for (int i = 1; i < cs.Health; i++)
        {
            if (i % 2 == 0)
            {
                hlife[heartPos].SetActive(true);
            } else
            {
                flife[heartPos].SetActive(true);
                heartPos++;
            }
        }

    }

    private void newFullHeartContainer(int j)
    {
        /*
         * j is the number of HeartContainer already instantiate.
         */

        GameObject fheartContainer = Instantiate(fullHeart);//, camPos, Quaternion.identity);
        GameObject hheartContainer = Instantiate(halfHeart);//, camPos, Quaternion.identity);
        GameObject eheartContainer = Instantiate(emptyHeart);//, camPos, Quaternion.identity);

        var height = 2 * Camera.main.orthographicSize;
        var width = height * Camera.main.aspect;

        eheartContainer.transform.parent = Camera.main.transform;
        eheartContainer.transform.transform.localPosition = new Vector3((-(width / 2) + 0.5f) + j, (height / 2) - 0.5f, 10);

        hheartContainer.transform.parent = Camera.main.transform;
        hheartContainer.transform.transform.localPosition = new Vector3((-(width / 2) + 0.5f) + j, (height / 2) - 0.5f, 10);

        fheartContainer.transform.parent = Camera.main.transform;
        fheartContainer.transform.transform.localPosition = new Vector3((-(width / 2) + 0.5f) + j, (height / 2) - 0.5f, 10);

        flife.Add(fheartContainer);
        hlife.Add(hheartContainer);
        elife.Add(eheartContainer);

    }

    private void newEmptyHeartContainer(int j)
    {
        /*
         * j is the number of HeartContainer already instantiate.
         */

        GameObject fheartContainer = Instantiate(fullHeart);//, camPos, Quaternion.identity);
        GameObject hheartContainer = Instantiate(halfHeart);//, camPos, Quaternion.identity);
        GameObject eheartContainer = Instantiate(emptyHeart);//, camPos, Quaternion.identity);

        var height = 2 * Camera.main.orthographicSize;
        var width = height * Camera.main.aspect;


        eheartContainer.transform.parent = Camera.main.transform;
        eheartContainer.transform.transform.localPosition = new Vector3((-(width / 2) + 0.5f) + j, (height / 2) - 0.5f, 10);


        hheartContainer.transform.parent = Camera.main.transform;
        hheartContainer.transform.transform.localPosition = new Vector3((-(width / 2) + 0.5f) + j, (height / 2) - 0.5f, 10);
        hheartContainer.SetActive(false);


        fheartContainer.transform.parent = Camera.main.transform;
        fheartContainer.transform.transform.localPosition = new Vector3((-(width / 2) + 0.5f) + j, (height / 2) - 0.5f, 10);
        fheartContainer.SetActive(false);


        flife.Add(fheartContainer);
        hlife.Add(hheartContainer);
        elife.Add(eheartContainer);
    }

    private void newHalfHeartContainer(int j)
    {
        /*
         * j is the number of HeartContainer already instantiate.
         */

        GameObject fheartContainer = Instantiate(fullHeart);//, camPos, Quaternion.identity);
        GameObject hheartContainer = Instantiate(halfHeart);//, camPos, Quaternion.identity);
        GameObject eheartContainer = Instantiate(emptyHeart);//, camPos, Quaternion.identity);

        var height = 2 * Camera.main.orthographicSize;
        var width = height * Camera.main.aspect;

        eheartContainer.transform.parent = Camera.main.transform;
        eheartContainer.transform.transform.localPosition = new Vector3((-(width / 2) + 0.5f) + j, (height / 2) - 0.5f, 10);


        hheartContainer.transform.parent = Camera.main.transform;
        hheartContainer.transform.transform.localPosition = new Vector3((-(width / 2) + 0.5f) + j, (height / 2) - 0.5f, 10);


        fheartContainer.transform.parent = Camera.main.transform;
        fheartContainer.transform.transform.localPosition = new Vector3((-(width / 2) + 0.5f) + j, (height / 2) - 0.5f, 10);
        fheartContainer.SetActive(false);


        flife.Add(fheartContainer);
        hlife.Add(hheartContainer);
        elife.Add(eheartContainer);
    }

}
