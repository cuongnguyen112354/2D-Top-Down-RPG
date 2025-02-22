using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        ActiveButtons();

        newGameButton.onClick.AddListener(StartGame);
        // continueButton.onClick.AddListener(() => SceneManager.LoadScene("Scene 1"));
        quitButton.onClick.AddListener(() => QuitGame());

        AudioManager.Instance.PlayBackgroundMusic();
        if (UIFade.Instance)
            UIFade.Instance.FadeToClear();
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        DisableButtons();
        if (UIFade.Instance)
            UIFade.Instance.FadeToBlack();
        StartCoroutine(WaitToLoadScene());
    }

    private IEnumerator WaitToLoadScene()
    {
        yield return new WaitForSeconds(1f);
        // UIFade.Instance.DestroyGameObject();
        AudioManager.Instance.StopBackgroundMusic();
        SceneManager.LoadScene("Scene 1");
    }

    private void ActiveButtons()
    {
        newGameButton.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    private void DisableButtons()
    {
        newGameButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false); 
    }
}
