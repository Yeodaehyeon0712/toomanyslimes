using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : BaseComponent
{
    public float speed=3;
    public bool TouchedLeft{ get; set; }
    public bool TouchedRight { get; set; }
    public MovementComponent(Actor owner) : base(owner, eComponent.MovementComponent)
    {
        OnReset();
    }
    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if ((TouchedRight && h > 0) || (TouchedLeft && h < 0))
            h = 0;

        Vector3 nextPos = new Vector3(h * speed * Time.deltaTime, 0, 0);
        _owner.transform.Translate(nextPos);
    }
    protected override void OnUpdate(float fixedDeltaTime)
    {
        Move();
    }
}
