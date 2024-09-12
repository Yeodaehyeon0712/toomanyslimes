using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eActorType
{
    None,
    Player,
    Enemy,
    End,
}
public class Actor : MonoBehaviour
{
    #region Fields

    // 고유 코드
    [SerializeField]protected int spawnHashCode;
    public int SpawnHashCode => spawnHashCode;
    //인덱스

    [SerializeField] protected long index;
    public long Index => index;

    //타입
    [SerializeField]eActorType actorType=eActorType.None;
    public eActorType ActorType => actorType;

    //월드 아이디
    [SerializeField] protected uint worldID;
    public uint WorldID => worldID;



    protected double currentHP;
    #region Component Fields
    protected Dictionary<eComponent, BaseComponent> _componentDictionary = new Dictionary<eComponent, BaseComponent>();
    #endregion

    #endregion

    #region Init Method
    public virtual void Initialize()
    {
        //_controllerComponent = new ControllerComponent(this);
    }
    #endregion

    #region Unity Method
    protected virtual void Update()
    {
        OnUpdateComponent(Time.deltaTime);
    }
    protected virtual void FixedUpdate()
    {
        //if (_controllerComponent != null) _controllerComponent.FixedUpdate(TimeManager.FixedDeltaTime);
    }
    #endregion

    #region Actor Method
    public virtual void Spawn(long _index, uint _worldID ,int _spawnHashCode, eActorType _type)
    {
        index = _index;
        worldID = _worldID;
        spawnHashCode = _spawnHashCode;
        actorType = _type;
        gameObject.SetActive(true);
    }
    public virtual void Death()
    {
        SpawnManager.Instance.RegisterActorPool(worldID);
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
    #endregion

    #region Component Method
    public void AddComponent(BaseComponent component)
    {
        if (_componentDictionary.ContainsKey(component.ComponentType))
        {
            _componentDictionary[component.ComponentType].Destroy();
            _componentDictionary.Remove(component.ComponentType);
        }
        _componentDictionary.Add(component.ComponentType, component);
    }
    public T GetComponent<T>(eComponent componentType) where T : BaseComponent
    {
        return _componentDictionary.ContainsKey(componentType) ? _componentDictionary[componentType] as T : null;
    }
    protected void OnUpdateComponent(float deltaTime)
    {
        foreach (var component in _componentDictionary)
            component.Value.NextFrame(deltaTime);
    }
    #endregion
}
