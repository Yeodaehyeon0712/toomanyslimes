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
    // ���� �ڵ� : ���� �������� Ȯ��
    [SerializeField]protected int spawnHashCode;
    public int SpawnHashCode => spawnHashCode;
    //�ε���

    [SerializeField] protected long index;
    public long Index => index;

    //Ÿ��
    [SerializeField]eActorType actorType=eActorType.None;
    public eActorType ActorType => actorType;

    //���� ���̵� : ���� �Ŵ����� ������  ����
    [SerializeField] protected uint worldID;
    public uint WorldID => worldID;

    //����
    protected double currentHP;
    public float DefaultAttackElapsedTime;

    #region Component Fields
    protected Dictionary<eComponent, BaseComponent> _componentDictionary = new Dictionary<eComponent, BaseComponent>();
    public FSMComponent FSM => fsmComponent;
    [SerializeField] protected FSMComponent fsmComponent;
    public eFSMState FSMState => fsmComponent.State;
    public StatComponent Stat => statComponent;
    [SerializeField] protected StatComponent statComponent;
    #endregion

    #endregion

    #region Init Method
    public virtual void Initialize()
    {
        fsmComponent = new FSMComponent(this);
        statComponent = new StatComponent(this);
    }
    #endregion

    #region Unity Method
    protected virtual void Update()
    {
        OnUpdateComponent(Time.deltaTime);
        DefaultAttackElapsedTime += Time.deltaTime * statComponent.AttackSpeed;
    }
    protected virtual void FixedUpdate()
    {

    }
    #endregion

    #region Actor Method
    public virtual void Spawn(long _index, uint _worldID ,int _spawnHashCode, eActorType _type)
    {
        index = _index;
        worldID = _worldID;
        spawnHashCode = _spawnHashCode;
        actorType = _type;
        statComponent.Reset();
        currentHP = statComponent.HP;
        gameObject.SetActive(true);
    }
    public virtual void Death()
    {
        fsmComponent.State = eFSMState.Death;
        SpawnManager.Instance.RegisterActorPool(worldID);
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
    public virtual void Hit(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0f)
        {
            currentHP = 0;
            Death();
        }
        Debug.Log($"{actorType}��{damage}�� ���ݷ����� �ǰݴ��� ���� ü���� {currentHP}");
    }
    public virtual void Recovery(double recovery)
    {
        System.Math.Clamp(currentHP += recovery,0,statComponent.HP);
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
    void ResetComponent()
    {
        foreach (var component in _componentDictionary)
            component.Value.Reset();
    }
    #endregion
}
