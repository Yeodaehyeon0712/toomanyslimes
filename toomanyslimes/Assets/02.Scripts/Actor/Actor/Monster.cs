using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Actor
{
    bool isBoss=>DataManager.MonsterTable[index].IsBoss;
    public override void Death()
    {
        base.Death();
        if (isBoss)
            StageManager.Instance.CompleteStage();
    }
    public override void Hit(float damage)
    {
        if (hpBar.isActiveAndEnabled == false)
            hpBar.ShowHPBar(true);
        base.Hit(damage);
        //HP 바를 켜준다 . 만약 보스라면 UI로 보여준다 .
    }
}
