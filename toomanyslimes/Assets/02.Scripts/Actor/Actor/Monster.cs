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
        if ((hpBar.isActiveAndEnabled == false)&&(isBoss==false))
            hpBar.ShowHPBar(true);
        base.Hit(damage);
    
        if (isBoss)
            SetBossHPBar();
    }
    void SetBossHPBar()
    {
        //이건 나중에 보스 진입시 사용 ..
        UIManager.Instance.MainUI.ShowBossStagePanel();
        UIManager.Instance.MainUI.SetBossHPSlider(currentHP,statComponent.HP);
    }
}
