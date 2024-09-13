using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    protected override void OnCollisionItem()
    {
        Player.Coin+=1;
        UIManager.Instance.MainUI.SetCoinCount(Player.Coin);
        DestroyItem();
    }
}
