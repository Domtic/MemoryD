using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject settingsPannel;
    public Image FadeIn;

    [Header("Variables")]
    public float Volumen;
  

   public void StartGame()
    {
        //idk if we even have starting animated scene
        StartCoroutine(ChangeLevel());
    }

    IEnumerator ChangeLevel()
    {
        FadeIn.gameObject.SetActive(true);
        while (FadeIn.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("FadeIn"))
        {
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
