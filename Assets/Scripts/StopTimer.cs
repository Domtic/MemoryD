using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StopTimer : MonoBehaviour
{
    public float delayTime;
    public GameManager gameManager;


    IEnumerator stopTiming()
    {
        while(delayTime >= 0)
        {
            delayTime -= Time.deltaTime;
            yield return null;
        }
        gameManager.timeIsStopped = false;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameManager.timeIsStopped = true;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //Add visual effect so it looks like the timebar stopped
            StartCoroutine(stopTiming());
        }
    }
}
