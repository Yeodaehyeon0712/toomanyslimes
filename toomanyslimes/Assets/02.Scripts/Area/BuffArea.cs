using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffArea : AreaBase
{
    int coinValue = 5;
    double recoveryValue=30;
    [SerializeField]bool isMoney;
    protected override void OnCollisionItem()
    {
        if(isMoney)
        {
            Player.Coin += coinValue;
            UIManager.Instance.MainUI.SetCoinCount(Player.Coin);
        }
        else
            Player.PlayerCharacter.Recovery(recoveryValue);
    }
    public override void DestroyArea()
    {
        if(isMoney)
            base.DestroyArea();
    }
}
