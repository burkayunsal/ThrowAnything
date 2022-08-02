using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RecruitManager : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private Image filledIMG;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag( "PlayerParent"))
        {
            filledIMG.DOFillAmount(1, Configs.UI.PopUpTimer).SetEase(Ease.Linear).OnComplete(() =>
            {
                UIManager.I.OpenRecruitPopup();
            });
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag( "PlayerParent"))
        {
            filledIMG.DOKill();
            filledIMG.fillAmount = 0;
            
            UIManager.I.CloseRecruitPopup();
        }   
    }

    [SerializeField] private int[] costs;
    
    public void BuyChar(int pt)
    {
        int _id = pt;

        PlayerTypes _type = (PlayerTypes)_id;
        if (SaveLoadManager.GetCoin() >= costs[_id])
        {
            SaveLoadManager.AddCoin(-costs[_id]);
            PlayerSpawner.I.SpawnPlayerMob(_type);
            PlayerController.I.AddToSavedList(_id);
        }
    }
    
    
}
