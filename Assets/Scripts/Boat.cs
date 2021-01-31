using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public float speed;
    public Transform targetPosition;
    public void Update()
    {

        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, targetPosition.position, step);
        if (transform.position == targetPosition.position)
        {
          //Game ends!!
        }
    }
}
