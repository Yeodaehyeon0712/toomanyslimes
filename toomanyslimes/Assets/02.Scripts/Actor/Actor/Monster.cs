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
            StageManager.Instance.GetFramework<NormalStageFramework>(eContentsType.Normal).KillBoss();
    }
}
