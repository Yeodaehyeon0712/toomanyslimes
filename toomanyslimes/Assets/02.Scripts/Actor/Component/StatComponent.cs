using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatComponent : BaseComponent
{
    #region Fields
    double hp;
    float attackDamage;
    float attackSpeed;

    public double HP=>hp;
    public float AttackDamage=>attackDamage;
    public float AttackSpeed=>attackSpeed;
    #endregion

    public StatComponent(Actor owner) : base(owner, eComponent.StatComponent)
    {
        OnReset();
        if(_owner.ActorType==eActorType.Enemy)
        {
            var monsterData = DataManager.MonsterTable[_owner.Index];
            this.hp = monsterData.HP;
            attackDamage = monsterData.AttackDamage;
            attackSpeed = monsterData.AttackSpeed;
        }
        //юс╫ц
        hp = 100;
        attackDamage=50;
        attackSpeed=1;
    }

    protected override void OnReset()
    {
        hp = 0f;
        attackDamage = 0f;
        attackSpeed = 0f;
    }

}
