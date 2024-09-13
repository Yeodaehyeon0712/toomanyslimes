using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrowSkill : SkillBase
{
    public FireArrowSkill(Actor _owner) : base(_owner)
    {
        owner = _owner;
    }
    public override void Init()
    {
        base.Init();
        skillUseTime = 1;
        skillDurationTime = 1;
    }
    protected override void OnUpdateSkill(float deltaTime)
    {

    }

    protected override void StopSkill()
    {

    }

    protected override void UseSkill()
    {
        SpawnArrow();
    }
    void SpawnArrow()
    {
        var prefab = Resources.Load<ArrowWeapon>("Weapon/Arrow");
        var arrow = Object.Instantiate(prefab,owner.transform.position,Quaternion.identity);
        arrow.InitWeapon();
        arrow.Spawn();
    }
}
