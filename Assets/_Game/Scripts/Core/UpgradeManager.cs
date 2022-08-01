using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI[] txt;
    
    [Header("UI")] 
    [SerializeField] private Image filledIMG;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag( "PlayerParent"))
        {
            filledIMG.DOFillAmount(1, Configs.UI.PopUpTimer).SetEase(Ease.Linear).OnComplete(() =>
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
    
    public void UPG_HP()
    {
        if (SaveLoadManager.GetCoin() >= Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetHPLevel()])
        {
            SaveLoadManager.AddCoin(- Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetHPLevel()]);
            SaveLoadManager.IncrementHP();
            txt[0].SetText("$" + Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetHPLevel()]);
        }
    }

    public void UPG_DMG() 
    {
        if (SaveLoadManager.GetCoin() >= Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetDamageLevel()])
        {
            SaveLoadManager.AddCoin(- Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetDamageLevel()]);
            SaveLoadManager.IncrementDamage();
            txt[1].SetText("$" + Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetDamageLevel()]);
        }
    } 
   
    public void UPG_AS() 
    {
        if (SaveLoadManager.GetCoin() >= Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetAttackSpeedLevel()])
        {
            SaveLoadManager.AddCoin(- Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetAttackSpeedLevel()]);
            SaveLoadManager.IncrementAttackSpeed();
            txt[2].SetText("$" + Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetAttackSpeedLevel()]);

        }
    } 
    
    public void UPG_RNG() 
    {
        if (SaveLoadManager.GetCoin() >= Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetRangeLevel()])
        {
            SaveLoadManager.AddCoin(- Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetRangeLevel()]);
            SaveLoadManager.IncrementRange();
            txt[3].SetText("$" + Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetRangeLevel()]);

        }
    } 

}
