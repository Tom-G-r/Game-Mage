using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverCanvas : MonoBehaviour
{
    public void GameOver()
    {

        gameObject.SetActive(true);
    }

    void Update()
    {   

        if (Input.anyKeyDown)
        {
            // Destory GameManager
            GameObject obj = GameObject.FindGameObjectWithTag("GameManager");

            if (obj != null)
            {
                GameManager gm = obj.GetComponent<GameManager>();
                if (gm != null)
                {
                    Destroy(gm.gameObject);
                }
            }

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Destroy(player);
            }


            GameObject ui = GameObject.FindGameObjectWithTag("UI");
            if (ui != null)
            {
                Destroy(ui);
            }

            // Load main menu
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
