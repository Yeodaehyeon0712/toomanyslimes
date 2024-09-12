using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatComponent : BaseComponent
{
    public StatComponent(Actor owner) : base(owner, eComponent.StatComponent)
    {
        OnReset();
    }
    //ü��
    //���ݷ�
    //���� ���� ����

    // Main Attack Status
    public double AttackDamage;
    public float AttackSpeed;
}
