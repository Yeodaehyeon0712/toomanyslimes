using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UStoreUI : UPopUpUI
{
    #region Field
    Button btn_Exit;
    TextMeshProUGUI text_CoinCount;

    Button btn_FireSkill;
    Button btn_BladeSkill;
    Button btn_RecoveryBuff;
    Button btn_CoinBuff;
    #endregion

    #region Init
    protected override void InitReference()
    {
        base.InitReference();
        btn_Exit = transform.Find("Btn_Exit").GetComponent<Button>();
        btn_Exit.onClick.AddListener(Exit);
        text_CoinCount = transform.Find("Panel_Coin/Text_Title").GetComponent<TextMeshProUGUI>();

        btn_FireSkill = transform.Find("Btn_FireSkill").GetComponent<Button>();
        btn_FireSkill.onClick.AddListener(Exit);

        btn_BladeSkill = transform.Find("Btn_Blade").GetComponent<Button>();
        btn_BladeSkill.onClick.AddListener(Exit);

        btn_RecoveryBuff = transform.Find("Btn_Recovery").GetComponent<Button>();
        btn_RecoveryBuff.onClick.AddListener(Exit);

        btn_CoinBuff = transform.Find("Btn_GetCoin").GetComponent<Button>();
        btn_CoinBuff.onClick.AddListener(Exit);
    }

    protected override void OnRefresh()
    {
    }
    #endregion
    public void Exit()
    {
        base.Disable();
    }

    public void SetCoinCount(int coinCount)
    {
        text_CoinCount.text = coinCount.ToString();
    }
   
}
