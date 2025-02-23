using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        newGameButton.onClick.AddListener(StartNewGame);
        continueButton.onClick.AddListener(ContinueGame);
        quitButton.onClick.AddListener(QuitGame);

        AudioManager.Instance.PlayBackgroundMusic();

        if (UIFade.Instance)
            UIFade.Instance.FadeToClear();
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void StartNewGame()
    {
        UpdateSorting();

        if (UIFade.Instance)
            UIFade.Instance.FadeToBlack();
            
        StartCoroutine(WaitToLoadScene());
    }

    private void ContinueGame()
    {
        return;
    }

    private IEnumerator WaitToLoadScene()
    {
        yield return new WaitForSeconds(1f);
        ActiveGameObjects();
        AudioManager.Instance.StopBackgroundMusic();
        SceneManager.LoadScene("Scene 1");
    }

    private void UpdateSorting()
    {
        GetComponent<Canvas>().sortingOrder = -2;
    }

    private void ActiveGameObjects()
    {
        UIFade.Instance.transform.Find("Gameplay Info UI")?.gameObject.SetActive(true);
    }
}
