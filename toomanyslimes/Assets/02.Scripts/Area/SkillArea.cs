using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillArea :AreaBase
{
    [SerializeField] bool isFire;
    protected override void OnCollisionItem()
    {
        Player.PlayerCharacter.GetComponent<Character>().RegisterSkill(isFire);
    }
    public override void DestroyArea()
    {
        if (isFire)
            base.DestroyArea();
    }
}
