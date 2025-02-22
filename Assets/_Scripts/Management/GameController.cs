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

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        newGameButton.onClick.AddListener(StartGame);
        // continueButton.onClick.AddListener(() => SceneManager.LoadScene("Scene 1"));
        quitButton.onClick.AddListener(() => QuitGame());

        AudioManager.Instance.PlayBackgroundMusic();
        playerControls.Gameplay.Quit.performed += _ => QuitGame();
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scene 1");
    }
}
