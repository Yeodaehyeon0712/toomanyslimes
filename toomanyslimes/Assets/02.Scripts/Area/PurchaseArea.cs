using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseArea : AreaBase
{
    protected override void OnCollisionItem()
    {
        UIManager.Instance.StoreUI.Enable();
    }
}
