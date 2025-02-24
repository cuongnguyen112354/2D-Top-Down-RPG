using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button homeButton;

    private GameObject pauseMenu;
    private Image blurImage;
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        pauseMenu = transform.Find("Pause Panel").gameObject;
        blurImage = transform.Find("Blur Image").GetComponent<Image>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        homeButton.onClick.AddListener(BackToMainMenu);
        
        playerControls.Gameplay.Pause.performed += _ => PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseButton.gameObject.SetActive(false);
        blurImage.gameObject.SetActive(true);
        pauseMenu.SetActive(true);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        pauseButton.gameObject.SetActive(true);
        blurImage.gameObject.SetActive(false);
        pauseMenu.SetActive(false);
    }

    private void BackToMainMenu()
    {
        Time.timeScale = 1;
        GameManager.Instance.BackToMainMenu();
    }
}
