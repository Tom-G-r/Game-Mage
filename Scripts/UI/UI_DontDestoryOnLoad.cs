using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DontDestoryOnLoad : MonoBehaviour
{
    public static GameObject Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
