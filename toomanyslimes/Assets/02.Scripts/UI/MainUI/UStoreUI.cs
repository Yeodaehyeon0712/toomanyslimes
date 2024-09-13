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
        btn_FireSkill.onClick.AddListener(PurchaseFireSkill);

        btn_BladeSkill = transform.Find("Btn_Blade").GetComponent<Button>();
        btn_BladeSkill.onClick.AddListener(PurchaseBladeSkill);

        btn_RecoveryBuff = transform.Find("Btn_Recovery").GetComponent<Button>();
        btn_RecoveryBuff.onClick.AddListener(PurchaseRecovery);

        btn_CoinBuff = transform.Find("Btn_GetCoin").GetComponent<Button>();
        btn_CoinBuff.onClick.AddListener(PurchaseCoin);
    }
    public override void Enable()
    {
        base.Enable();
        BackgroundManager.Instance.IsBGMoveAtStore = true;
        CheckEnablePurchase();
    }

    protected override void OnRefresh()
    {
    }
    #endregion
    public void Exit()
    {
        BackgroundManager.Instance.IsBGMoveAtStore = false;


        base.Disable();
    }

    public void SetCoinCount(int coinCount)
    {
        text_CoinCount.text = coinCount.ToString();
    }
    public void PurchaseFireSkill()
    {
        CheckEnablePurchase();
        btn_FireSkill.enabled = false;
        Player.PlayerCharacter.GetComponent<Character>().RegisterSkill(true);
    }
    public void PurchaseBladeSkill()
    {
        CheckEnablePurchase();
        btn_BladeSkill.enabled = false;
        Player.PlayerCharacter.GetComponent<Character>().RegisterSkill(false);
    }
    public void PurchaseCoin()
    {
        CheckEnablePurchase();
        btn_CoinBuff.enabled = false;
        Player.Coin += 10;
        UIManager.Instance.MainUI.SetCoinCount(Player.Coin);
        SetCoinCount(Player.Coin);
    }
    public void PurchaseRecovery()
    {
        CheckEnablePurchase();
        btn_RecoveryBuff.enabled = false;
        Player.PlayerCharacter.Recovery(30);
    }
    public void CheckEnablePurchase()
    {
        //if (Player.Coin < 5)
        //{
        //    btn_CoinBuff.enabled = false;
        //    btn_BladeSkill.enabled = false;
        //    btn_FireSkill.enabled = false;
        //    btn_RecoveryBuff.enabled = false;
        //}
        //Player.Coin -= 5;
    }

}
