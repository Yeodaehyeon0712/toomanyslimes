using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBladeSkill : SkillBase
{
    int bladeCount = 2;
    float radius = 1f;
    float rotationSpeed = 180f;
    BladeWeapon[] blades;

    public RotateBladeSkill(Actor _owner) : base(_owner)
    {
        owner = _owner;
    }

    public override void Init(Actor _owner)
    {
        base.Init(_owner);

        skillUseTime = 6;
        skillDurationTime = 6;

        blades = new BladeWeapon[bladeCount];
        for (int i = 0; i < bladeCount; i++)
        {
            float angle = i * Mathf.PI * 2 / bladeCount;
            Vector3 bladePosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            var prefab = Resources.Load<BladeWeapon>("Weapon/Blade");
            blades[i] = Object.Instantiate(prefab, (Vector2)_owner.transform.position + (Vector2)bladePosition, Quaternion.identity, _owner.transform);
            blades[i].InitWeapon();
        }
        StopSkill();
    }

    protected override void OnUpdateSkill(float deltaTime)
    {
        for (int i = 0; i < bladeCount; i++)
        {
            blades[i].transform.RotateAround(owner.transform.position, Vector3.forward, rotationSpeed * deltaTime);
        }
    }

    protected override void StopSkill()
    {
        foreach (var blade in blades)
            blade.Hide();
    }

    protected override void UseSkill()
    {
        foreach (var blade in blades)
            blade.Spawn();
    }
}
