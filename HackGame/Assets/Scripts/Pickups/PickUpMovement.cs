using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMovement : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Move());
    }      

    private IEnumerator Move()
    {
        while (true)
        {
            for (int i = 0; i < 100; i++)
            {
                var newY = transform.position.y + i * 0.00005;
                transform.position = new Vector3(transform.position.x, (float)newY, -5);
                yield return new WaitForSeconds(0.001f);
            }

            for (int i = 0; i < 100; i++)
            {
                double newY = transform.position.y - i * 0.00005;
                transform.position = new Vector3(transform.position.x, (float)newY, -5);
                yield return new WaitForSeconds(0.001f);
            }
        }
    }
}
