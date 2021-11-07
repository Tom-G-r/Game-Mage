using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haste : MonoBehaviour
{
    public float MovementBuff = 0.8f;
    public float DurationInSeconds = 1.5f;
 
    protected MovementBehavior Character;

    // Start is called before the first frame update
    void Start()
    {
        Character = gameObject.GetComponent<MovementBehavior>();
        if (Character != null)
        {
            Character.AddSpeed(MovementBuff);
        }

        StartCoroutine(Effect());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Effect()
    {
        yield return new WaitForSeconds(DurationInSeconds);

        if (Character != null)
        {
            Character.AddSpeed(-MovementBuff);
        }

        Destroy(this);
    }
}
