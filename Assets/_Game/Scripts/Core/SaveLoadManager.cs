using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    #region LEVEL

    const string KEY_LEVEL = "levels";

    public static void IncreaseLevel() => PlayerPrefs.SetInt(KEY_LEVEL, GetLevel() + 1);
    public static int GetLevel() => PlayerPrefs.GetInt(KEY_LEVEL, 0);

    #endregion
    

    #region COIN

    const string KEY_COIN = "coins";

    public static void AddCoin(int add)
    {
        PlayerPrefs.SetInt(KEY_COIN, GetCoin() + add);
        UIManager.I.UpdateCoin();

    }

    public static int GetCoin() => PlayerPrefs.GetInt(KEY_COIN, 0);

    #endregion

    #region COIN

    const string KEY_VIBRATION = "vibrator";

    public static bool HasVibration() => PlayerPrefs.GetInt(KEY_VIBRATION, 1) == 1;

    public static void ChangeVibrationStatus() { if (HasVibration()) SetVibrationStatus(false); else SetVibrationStatus(true); }

    public static void SetVibrationStatus(bool isEnabled) { PlayerPrefs.SetInt(KEY_VIBRATION, isEnabled ? 1 : 0); UIManager.I.UpdateHapticStatus(); }

    #endregion

    #region PRIZES

    const string KEY_PRIZES = "priozes_";

    public static bool HasPrizeTaken(int id) => PlayerPrefs.GetInt(KEY_PRIZES + id, 0) == 1;

    public static void SetPrizeTaken(int id) => PlayerPrefs.SetInt(KEY_PRIZES + id, 1);

    #endregion

    
    #region ATTACK_SPEED

    const string KEY_AS = "attackSpeed";

    public static void IncrementAttackSpeed()
    {
        PlayerPrefs.SetInt(KEY_AS, GetAttackSpeedLevel() + 1);
        //TODO Init plkayers again
    }
    
    public static int GetAttackSpeedLevel() => PlayerPrefs.GetInt(KEY_AS, 0);

    #endregion

    #region DAMAGE

    const string KEY_DMG = "damage";

    public static void IncrementDamage()
    {
        PlayerPrefs.SetInt(KEY_DMG, GetDamageLevel() + 1);
        //TODO Init plkayers again
    }
    
    public static int GetDamageLevel() => PlayerPrefs.GetInt(KEY_DMG, 0);

    #endregion
    
    #region HP

    const string KEY_HP = "hp";

    public static void IncrementHP()
    {
        PlayerPrefs.SetInt(KEY_HP, GetHPLevel() + 1);
        //TODO Init plkayers again
    }
    
    public static int GetHPLevel() => PlayerPrefs.GetInt(KEY_HP, 0);

    #endregion
    
    #region RANGE

    const string KEY_RNG = "hp";

    public static void IncrementRange()
    {
        PlayerPrefs.SetInt(KEY_RNG, GetRangeLevel() + 1);
        //TODO Init plkayers again
    }
    
    public static int GetRangeLevel() => PlayerPrefs.GetInt(KEY_RNG, 0);

    #endregion
}
