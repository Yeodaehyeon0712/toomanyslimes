using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleComponent : BaseComponent
{
    List<SkillBase> skillList = new List<SkillBase>();
    public BattleComponent(Actor owner) : base(owner, eComponent.BattleComponent)
    {
        
    }
    protected override void OnUpdate(float deltaTime)
    {
        if (skillList == null) return;
        foreach(var skill in skillList)
        {
            skill.OnUpdate(deltaTime);
        }
    }
    public void RegisterSkill(SkillBase newSkill)
    {
        newSkill.Init(_owner);
        skillList.Add(newSkill);
    }
}
