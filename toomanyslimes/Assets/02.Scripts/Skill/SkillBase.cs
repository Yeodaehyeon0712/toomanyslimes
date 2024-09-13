using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase 
{
    protected Actor owner;
    protected float elapsedTime;
    protected float skillUseTime;
    protected float skillDurationTime;

    protected bool isSkillOn;
    public  SkillBase(Actor _owner)
    {
        owner = _owner;
    }
    public virtual void Init()
    {

    }
    public void OnUpdate(float deltaTime)
    {
        elapsedTime += deltaTime;
        //��ų�� �����ִٸ� �ߵ��� ���� ī��Ʈ
        if(isSkillOn==false)
        {
            if(elapsedTime>=skillUseTime)
            {
                UseSkill();
                isSkillOn = true;
                elapsedTime = 0;
            }
        }
        //��ų�� ���� �ִٸ� ���� ���� ī���
        else
        {
            if (elapsedTime >= skillDurationTime)
            {
                StopSkill();
                isSkillOn = false;
                elapsedTime = 0;
            }
            OnUpdateSkill(deltaTime);
        }
    }
    protected abstract void UseSkill();
    protected abstract void StopSkill();
    protected abstract void OnUpdateSkill(float deltaTime);

}
