using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CreditsTime());
    }

    public IEnumerator CreditsTime()
    {
        yield return new WaitForSeconds(22);
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InMenu);
        UIManager.Instance.ActivateLevelMenu();
        SceneManager.LoadScene("Main");
    }
}
