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
        //스킬이 꺼져있다면 발동을 위한 카운트
        if(isSkillOn==false)
        {
            if(elapsedTime>=skillUseTime)
            {
                UseSkill();
                isSkillOn = true;
                elapsedTime = 0;
            }
        }
        //스킬이 켜져 있다면 끄기 위한 카운드
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
