using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private Image filledIMG;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag( "PlayerParent"))
        {
            filledIMG.DOFillAmount(1, Configs.UI.UpgradePopUpTimer).SetEase(Ease.Linear).OnComplete(() =>
            {
                UIManager.I.OpenUpgradePopup();
            });
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag( "PlayerParent"))
        {
            filledIMG.DOKill();
            filledIMG.fillAmount = 0;
            
            UIManager.I.CloseUpgradePopup();
        }   
    }
}
