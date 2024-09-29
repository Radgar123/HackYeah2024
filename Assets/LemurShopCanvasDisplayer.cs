using System;
using System.Collections;
using System.Collections.Generic;
using _Code.Scripts.LemurSystems;
using Hearings.SaveSystem;
using TMPro;
using UnityEngine;

public class LemurShopCanvasDisplayer : MonoBehaviour
{
    [SerializeField] private LemurManager _lemurManager;

    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private TextMeshProUGUI attackText;


    private void Start()
    {
        energyText.text = _lemurManager.scriptableCharacter.energy.ToString();
        attackText.text = _lemurManager.scriptableCharacter.attack.ToString();
    }
}
