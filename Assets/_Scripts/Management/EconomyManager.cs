using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManager : Singleton<EconomyManager>
{
    public int CurrentGold
    {
        get => _currentGold;
        set
        {
            if (value <= 0)
                _currentGold = 0;
            else
            _currentGold = value;
            
            UpdateCurrentGold();
        }
    }

    private TMP_Text goldText;
    private int _currentGold = 0;

    const string COIN_AMOUNT_TEXT = "Gold Amount Text";

    public void UpdateIncreaseGold(int amount = 1)
    {
        _currentGold += amount;

        UpdateCurrentGold();
    }

    private void UpdateCurrentGold()
    {
        if (goldText == null)
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();

        goldText.text = _currentGold.ToString("D3");
    }
}
