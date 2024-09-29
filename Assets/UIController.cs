using System;
using System.Collections;
using System.Collections.Generic;
using Hearings.SaveSystem;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cashText;
    
    private void Update()
    {
        if (cashText && SaveManager.Instance)
        {
            cashText.text = SaveManager.Instance.playerData.cash.ToString();
        }
    }
}
