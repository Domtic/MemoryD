using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BreathBar : MonoBehaviour
{
    public Image fillBar;
    public Sprite blue, green, red;
  
    private float targetProgress = 1;
    public float fillSpeed = 0.1f;
    public float decreaseSpeed = 0.01f;
    public GameManager gameManager;
    public AudioSource filing;
    bool imPlaying;
    private void Start()
    {
        imPlaying = true;
    }

    public void Update()
    {
      
        if(imPlaying)
        {
            if (targetProgress == 0)
            {
                if (fillBar.fillAmount > targetProgress)
                {
                    fillBar.fillAmount -= decreaseSpeed * Time.deltaTime;
                 
                }
            }
            else
            {
                if (fillBar.fillAmount < targetProgress)
                {
                   
                    if(!filing.isPlaying)
                    {
                        filing.Play();
                    }
                    fillBar.fillAmount += fillSpeed * Time.deltaTime;
                   
                }
               
            }

            if (fillBar.fillAmount > 0.26f && fillBar.fillAmount < 0.75f)
            {
                fillBar.sprite = green;
            }else if(fillBar.fillAmount <= 0.26f)
            {
                fillBar.sprite = red;
            }
            else
            {
                fillBar.sprite = blue;
            }


                if (fillBar.fillAmount == 0)
            {
                gameManager.LoseGame("You DIED because you ran out of oxigen");
                imPlaying = false;
            }
        }
        
    }

    public void SetNewTarget(float newProgress, float filling)
    {
        fillSpeed = filling;
       targetProgress = newProgress;
    }
}
