using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("GameVariables")]
    public GameObject levelDoor;
    public List<PickeableMemory> levelPickeables;
    public int MemoriesPicked;
    public int ObjectiveMemories;
    public float LevelTime;
    bool finishedTime;
    public bool timeIsStopped;
    public GameObject arrow;
    [Header("UI Elements")]
    public Image FadeOut, FadeIn;
    public Image memortToShow;
    public Slider timeBar;
    public GameObject playerUI;
    public GameObject ResumeButton,PauseButon, deathObject;

    void Start()
    {
        timeIsStopped = false;
        finishedTime = false;
        MemoriesPicked = 0;
        ObjectiveMemories = levelPickeables.Count;

    }

    public void TakeMemory(GameObject memory)
    {
        //Play animation of the memory being taken with a trigger, after this, the memory is no longer touchable and remains
        //iddle
        //memory.GetComponent<PickeableMemory>().memoryAnimation.SetTrigger();
        Debug.Log("PickedMemory");
        MemoriesPicked++;
        memortToShow.gameObject.SetActive(true);
        arrow.SetActive(true);
        if(MemoriesPicked == ObjectiveMemories)
        {
            levelDoor.SetActive(false);
            //play end animation and pass to the enxt level
        }
    }

    public void Update()
    {
        if(!finishedTime)
        {
            if(!timeIsStopped)
            {
                if (timeBar.value > 0)
                {
                    timeBar.value -= LevelTime * Time.deltaTime;
                }
                else
                {
                    Debug.Log("Se acabo el tiempo");
                    finishedTime = true;
                    //Mostrar la UI para repetir el nivel
                }
            }
            
        }
     
        if(!FadeOut.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("FadeOut"))
        {
            FadeOut.gameObject.SetActive(false);
        }
       
    }

    public void LoseGame(string causeOfDeath)
    {
        Debug.Log(causeOfDeath);
        PauseButon.gameObject.SetActive(false);

        deathObject.gameObject.SetActive(true);
        playerUI.SetActive(false);
    }



    public void WinGame()
    {
        Debug.Log("Has ganado");
      
        StartCoroutine(ChangeLevel());
        PauseButon.gameObject.SetActive(false);

        //ResumeButton.gameObject.SetActive(true);
        playerUI.SetActive(false);

    }

    

    IEnumerator ChangeLevel()
    {
        FadeIn.gameObject.SetActive(true);
        while (FadeIn.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("FadeIn"))
        {
            yield return null;
        }
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WinGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseButon.gameObject.SetActive(false);

        ResumeButton.gameObject.SetActive(true);
        playerUI.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseButon.gameObject.SetActive(true);

        ResumeButton.gameObject.SetActive(false);
        playerUI.SetActive(true);
    }


    public void GoBackToMENU()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    IEnumerator GoBackTo()
    {
        FadeIn.gameObject.SetActive(true);
       
        while (FadeIn.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("FadeIn"))
        {
            yield return null;
        }
        SceneManager.LoadScene(0);
    }
}
