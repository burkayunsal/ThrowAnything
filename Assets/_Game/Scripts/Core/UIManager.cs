using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using SBF.Extentions.Colors;

public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] Panels pnl;
    [Header("Images")]
    [SerializeField] Images img;
    [Header("Buttons")]
    [SerializeField] Buttons btn;
    [Header("Texts")]
    [SerializeField] Texts txt;

    private CanvasGroup activePanel = null;

    public Panels GetPanel() => pnl;
    public Buttons GetButtons() => btn;

    private void Start()
    {
        UpdateTexts();
        UpdateHapticStatus();
    }

    public void Initialize(bool isButtonDerived)
    {
        btn.play.gameObject.SetActive(isButtonDerived);
        img.taptoStart.gameObject.SetActive(!isButtonDerived);
        FadeInAndOutPanels(pnl.mainMenu);
    }

    public void StartGame()
    {
        GameManager.OnStartGame();
    }

    public void OnGameStarted()
    {
        FadeInAndOutPanels(pnl.gameIn);
    }

    public void OpenUpgradePopup()
    {
        FadeInAndOutPanels(pnl.upgrade);
    }   
    public void CloseUpgradePopup()
    {
        ClosePanel(pnl.upgrade);
    }  
    
    public void OnFail()
    {
        FadeInAndOutPanels(pnl.fail);
    }

    public void OnSuccess(bool hasPrize = true)
    {
        if(hasPrize)
        {
            btn.nextLevel.gameObject.SetActive(false);
            PrizeHandler.I.ShowPrizeProcess();
        }
        else
        {
            btn.nextLevel.gameObject.SetActive(true);
            FadeInAndOutPanels(pnl.success);
        }
        
    }

    public void OnPrizeShow(float percentage, PrizeCanvas prizeCanvas)
    {
        btn.nextLevel.gameObject.SetActive(false);
        btn.getPrize.gameObject.SetActive(false);

        FadeInAndOutPanels(pnl.success);

        new SBF.Toolkit.DelayedAction(() => {

            AnimatePrize(percentage, prizeCanvas).OnComplete(() =>
            {
                if (percentage >= 1f)
                {
                    btn.getPrize.gameObject.SetActive(true);
                    prizeCanvas.img_sunShine.gameObject.SetActive(true);
                    prizeCanvas.img_prizeBg.enabled = false;

                    prizeCanvas.img_prizeVisible.transform.DOScale(1.1f, .2f).OnComplete(() =>
                    {
                        prizeCanvas.img_prizeVisible.transform.DOScale(.95f, .1f).OnComplete(() =>
                        {
                            prizeCanvas.img_prizeVisible.transform.DOScale(1.05f, .1f).OnComplete(() =>
                            {
                                prizeCanvas.img_prizeVisible.transform.DOScale(1f, .3f);
                            });
                        });
                    });


                    new SBF.Toolkit.DelayedAction(() => btn.nextLevel.gameObject.SetActive(true), 3f).Execute(this);
                }
                else
                {
                    btn.nextLevel.gameObject.SetActive(true);
                }
            });

        }, Configs.UI.FadeOutTime * 2f).Execute(this);
    }

    Tween AnimatePrize(float percentage, PrizeCanvas prizeCanvas)
    {
        float fillAmount = 0f;
        return DOTween.To(() => fillAmount, x => fillAmount = x, percentage, percentage * 2f).SetEase(Ease.Linear).OnUpdate(() =>
        {
            prizeCanvas.txt_percentage.text = (fillAmount * 100f).ToString("00").Insert(0, "%");
            prizeCanvas.img_prizeVisible.fillAmount = fillAmount;
        });
    }

    public void ReloadScene(bool isSuccess)
    {
        GameManager.ReloadScene(isSuccess);
    }

    void FadeInAndOutPanels(CanvasGroup _in)
    {
        CanvasGroup _out = activePanel;
        activePanel = _in;

        if(_out != null)
        {
            _out.interactable = false;
            _out.blocksRaycasts = false;

            _out.DOFade(0f, Configs.UI.FadeOutTime).OnComplete(() =>
            {
                _in.DOFade(1f, Configs.UI.FadeOutTime).OnComplete(() =>
                {
                    _in.interactable = true;
                    _in.blocksRaycasts = true;
                });
            });
        }
        else
        {
            _in.DOFade(1f, Configs.UI.FadeOutTime).OnComplete(() =>
            {
                _in.interactable = true;
                _in.blocksRaycasts = true;
            });
        }
       
       
    }
    
    void ClosePanel(CanvasGroup _panel)
    {
        _panel.interactable = false;
        _panel.blocksRaycasts = false;

        _panel.DOFade(0f, Configs.UI.FadeOutTime);
    }

    public void ShowJoystickHighlights(int area)
    {
        for (int i = 0; i < img.joystickHighlights.Length; i++)
        {
            img.joystickHighlights[i].gameObject.SetActive(i == area);
        }
    }

    public void UpdateTexts()
    {
        txt.level.text = "LEVEL " + (SaveLoadManager.GetLevel() + 1).ToString();
    }

    public void UpdateHapticStatus()
    {
        for (int i = 0; i < img.vibrations.Length; i++)
        {
            img.vibrations[i].color = SaveLoadManager.HasVibration() ? img.vibrations[i].color.SetAlpha(1f) : img.vibrations[i].color.SetAlpha(.1f);
        }
    }

    public void ChangeHapticStatus()
    {
        SaveLoadManager.ChangeVibrationStatus();
    }

    [System.Serializable]
    public class Panels
    {
        public CanvasGroup mainMenu, gameIn, success, fail, upgrade;
    }

    [System.Serializable]
    public class Images
    {
        public Image taptoStart;
        public Image[] joystickHighlights, vibrations;
        public Image upgradeTimer;
    }

    [System.Serializable]
    public class Buttons
    {
        public Button play, nextLevel,getPrize;
    }

    [System.Serializable]
    public class Texts
    {
        public TextMeshProUGUI level;
    }
    
   
}
