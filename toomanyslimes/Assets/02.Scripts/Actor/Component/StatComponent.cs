using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatComponent : BaseComponent
{
    #region Fields
    double hp;
    float attackDamage;
    float attackSpeed;

    public double HP =>hp;
    public float AttackDamage=>attackDamage;
    public float AttackSpeed=>attackSpeed;
    #endregion

    public StatComponent(Actor owner) : base(owner, eComponent.StatComponent)
    {

    }

    protected override void OnReset()
    {
        if (_owner.ActorType == eActorType.Enemy)
        {
            var monsterData = DataManager.MonsterTable[_owner.Index];
            this.hp = monsterData.HP;
            attackDamage = monsterData.AttackDamage;
            attackSpeed = monsterData.AttackSpeed;
        }
        else if (_owner.ActorType == eActorType.Player)
        {
            //юс╫ц
            hp = 100;
            attackDamage = 50;
            attackSpeed = 1;
        }
    }

}
