using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerComponent : BaseComponent
{
    #region Fields
    public float speed=5;
    public bool TouchedLeft{ get; set; }
    public bool TouchedRight { get; set; }
    #endregion

    #region Component Method
    public ControllerComponent(Actor owner) : base(owner, eComponent.ControllerComponent)
    {

    }
    protected override void OnFixedUpdate(float fixedDeltaTime)
    {
        Move();
    }
    #endregion

    #region Controller Method
    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if ((TouchedRight && h > 0) || (TouchedLeft && h < 0))
            h = 0;

        Vector3 nextPos = new Vector3(h * speed * Time.deltaTime, 0, 0);
        _owner.transform.Translate(nextPos);
    }
    #endregion
}
