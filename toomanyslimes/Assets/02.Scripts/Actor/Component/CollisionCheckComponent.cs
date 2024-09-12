using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheckComponent : BaseComponent
{
    #region Fields
    LayerMask obstacleLayer=1<<3;
    Transform ownerTransform;
    GameObject battleTarget;
    float rayLength = 0.5f; 

    ControllerComponent controllerComponent;
    #endregion

    #region Component Method
    public CollisionCheckComponent(Actor owner) : base(owner, eComponent.ColiisionCheckComponent)
    {
        OnReset();
        controllerComponent = _owner.GetComponent<ControllerComponent>(eComponent.ControllerComponent);
        ownerTransform = _owner.transform;
    }
    protected override void OnUpdate(float fixedDeltaTime)
    {
        CheckForward();
        CheckLeft();
        CheckRight();
        CheckBattleTarget();
    }
    #endregion

    #region Check Collision Method  
    public void CheckLeft()
    {
        Vector2 direction1 = Quaternion.Euler(0, 0, -45) * Vector2.left; 
        Vector2 direction2 = Vector2.left;                                
        Vector2 direction3 = Quaternion.Euler(0, 0, 45) * Vector2.left; 

        controllerComponent.TouchedLeft = CheckRaycastCollisions(direction1, direction2, direction3);
    }
    public void CheckRight()
    {
        Vector2 direction1 = Quaternion.Euler(0, 0, 45) * Vector2.right;  
        Vector2 direction2 = Vector2.right;                               
        Vector2 direction3 = Quaternion.Euler(0, 0, -45) * Vector2.right; 
        
        controllerComponent.TouchedRight = CheckRaycastCollisions(direction1, direction2, direction3);
    }
    public void CheckForward()
    {
        Vector2 direction1 = Quaternion.Euler(0, 0, -45) * Vector2.up;
        Vector2 direction2 = Vector2.up;
        Vector2 direction3 = Quaternion.Euler(0, 0, 45) * Vector2.up;

        bool collisionDetected = CheckRaycastCollisions(direction1, direction2, direction3,0.1f);
        BackgroundManager.Instance.IsBGMove = (collisionDetected==false);
    }
    bool CheckRaycastCollisions(Vector2 dir1, Vector2 dir2, Vector2 dir3, float extraLength = 0)
    {
        Vector2 basePosition = ownerTransform.position;
        Vector2[] directions = { dir1, dir2, dir3 };
        float[] lengths = { rayLength + extraLength, rayLength, rayLength + extraLength };

        for (int i = 0; i < 3; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(basePosition, directions[i], lengths[i], obstacleLayer);
#if UNITY_EDITOR
            Debug.DrawRay(basePosition, directions[i] * lengths[i], Color.blue);
#endif
            if (hit.collider != null && (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Obstacle")))
                return true;
        }
        return false;
    }
    #endregion

    #region Check Battle Method
    void CheckBattleTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(ownerTransform.position, Vector2.up, rayLength, obstacleLayer);
        //주변에 아무것도 충돌할게 없다거나 충돌체가 적이 아니라면 배틀하는 상태에서 벗어난다 .
        if (hit.collider == null|| hit.collider.CompareTag("Enemy") == false)
        {
            battleTarget = null;
            return;
        }
        //이미 타겟으로 지정한 적과 충돌했다면 아무것도 하지 않는다 .
        if (battleTarget == hit.collider.gameObject) return;

        //새로운 적이 감지되었다면
        battleTarget = hit.collider.gameObject;
        Debug.Log("공격");      
    }
    #endregion
}
