using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI[] txtCost;
    
    public TextMeshProUGUI[] txtLevel;
    
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
            txtCost[0].SetText("$" + Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetHPLevel()]);
            txtLevel[0].SetText("Level " + Configs.UpgradePlayer.upgradeLevels[SaveLoadManager.GetHPLevel()]);
            PlayerParticleHandler.I.PlayerUpgraded();
        }
    }

    public void UPG_DMG() 
    {
        if (SaveLoadManager.GetCoin() >= Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetDamageLevel()])
        {
            SaveLoadManager.AddCoin(- Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetDamageLevel()]);
            SaveLoadManager.IncrementDamage();
            txtCost[1].SetText("$" + Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetDamageLevel()]);
            txtLevel[1].SetText("Level " + Configs.UpgradePlayer.upgradeLevels[SaveLoadManager.GetDamageLevel()]);
        }
    } 
   
    public void UPG_AS() 
    {
        if (SaveLoadManager.GetCoin() >= Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetAttackSpeedLevel()])
        {
            SaveLoadManager.AddCoin(- Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetAttackSpeedLevel()]);
            SaveLoadManager.IncrementAttackSpeed();
            txtCost[2].SetText("$" + Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetAttackSpeedLevel()]);
            txtLevel[2].SetText("Level " + Configs.UpgradePlayer.upgradeLevels[SaveLoadManager.GetAttackSpeedLevel()]);
            
        }
    } 
    
    public void UPG_RNG() 
    {
        if (SaveLoadManager.GetCoin() >= Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetRangeLevel()])
        {
            SaveLoadManager.AddCoin(- Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetRangeLevel()]);
            SaveLoadManager.IncrementRange();
            txtCost[3].SetText("$" + Configs.UpgradePlayer.upgradeCosts[SaveLoadManager.GetRangeLevel()]);
            txtLevel[3].SetText("Level " + Configs.UpgradePlayer.upgradeLevels[SaveLoadManager.GetRangeLevel()]);
        }
    } 

}
