using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : Singleton<PlayerStamina>
{
    public int CurrentStamina
    { 
        get => _currentStamina; 
        set
        {
            if (value > startingStamina)
                _currentStamina = startingStamina;
            else if (value < 0)
                _currentStamina = 0;
            else
                _currentStamina = value;

            UpdateStaminaImages();
        }
    }

    [SerializeField] private Sprite fullStaminaImage, emptyStaminaImage;
    [SerializeField] private int timeBetweenStaminaRefresh = 3;

    const string STAMINA_CONTAINER_TEXT = "Stamina Container";

    private int _currentStamina;
    private Transform staminaContainer;
    private int startingStamina = 3;
    private int maxStamina;

    protected override void Awake()
    {
        base.Awake();
        maxStamina = startingStamina;
        _currentStamina = startingStamina;
    }

    private void Start()
    {
        staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }

    public void UseStamina()
    {
        _currentStamina--;
        UpdateStaminaImages();
    }

    public void RefreshStamina()
    {
        if (_currentStamina < maxStamina)
            _currentStamina++;

        UpdateStaminaImages();
    }

    private void UpdateStaminaImages()
    {
        for (int i = 0; i < maxStamina; i++)
            if (i <= _currentStamina - 1)
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = fullStaminaImage;
            else
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = emptyStaminaImage;

        if (_currentStamina < maxStamina)
        {
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
        }
    }

    private IEnumerator RefreshStaminaRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStaminaRefresh);
            RefreshStamina();
        }
    }
}