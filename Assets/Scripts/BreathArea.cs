using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathArea : MonoBehaviour
{
    public float decreaseSpeed;
    private void OnTriggerStay2D(Collider2D collision)
    {

        if(collision.CompareTag("Player"))
        {
            
            transform.localScale += -(Vector3.one * decreaseSpeed * Time.deltaTime);
            if (transform.localScale.x <= 1)
            {
                //collision.gameObject.GetComponent<PlayerControls>().myBreathBar.SetNewTarget(1.0f, 0.1f);
                Destroy(this.gameObject);
            }
        }
       
    }
}
