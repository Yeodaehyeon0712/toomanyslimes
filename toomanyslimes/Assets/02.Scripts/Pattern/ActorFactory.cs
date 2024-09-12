using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorFactory 
{
    #region Fields
    Dictionary<eActorType, Dictionary<int, MemoryPool<Actor>>> actorPool;
    Dictionary<uint,Actor> actorDic;
    Transform instanceRoot;
    uint currentWorldID;
    #endregion

    public ActorFactory(Transform _instanceRoot)
    {
        instanceRoot = _instanceRoot;
        currentWorldID = 0;
        actorPool = new Dictionary<eActorType, Dictionary<int, MemoryPool<Actor>>>();
        for(eActorType i=0;i<eActorType.End;++i)
        {
            actorPool[i] = new Dictionary<int, MemoryPool<Actor>>();
        }
        actorDic = new Dictionary<uint, Actor>();
    }

    #region Factory Method
    public T GetActor<T>(eActorType type,long index,Transform spawnPoint=null)where T:Actor
    {
        ++currentWorldID;

        string resourcePath = "Prefabs/AB";
        int pathHash = 0;
        //������ �Ŵ����κ��� �ش� �ε����� �����͸� �޴´� .

        //���� �ش� Ǯ�� �ش� ������ ���Ͱ� ���ٸ� Ǯ�� ����
        if (actorPool[type].ContainsKey(pathHash)==false)
            actorPool[type].Add(pathHash, new MemoryPool<Actor>(20));

        //�ش� Ǯ�κ��� ���͸� �����´�
        T spawnedActor = actorPool[type][pathHash].GetItem() as T;

        //���� ���Ͱ� ���ٸ� �������ش�
        if(spawnedActor==null)
        {
            T prefabs=Resources.Load<T>(resourcePath);

            T cloneAsset =Object.Instantiate(prefabs, spawnPoint);
            cloneAsset.Initialize();
            spawnedActor = cloneAsset;
        }
        else//Ǯ�� �ִٸ�
        {
            if(spawnPoint!=null)
            spawnedActor.transform.SetParent(spawnPoint);
        }

        spawnedActor.Spawn(index, currentWorldID, pathHash, type);
        actorDic.Add(currentWorldID, spawnedActor);
        return spawnedActor;
    }
    public void RegisterActorAtPool(uint worldID)
    {
        if (actorDic.ContainsKey(worldID) == false) return;

        Actor temp = actorDic[worldID];
        actorDic.Remove(worldID);
        //����� �������� ����
        //actorPool[temp.ActorType][temp.SpawnHashCode].Register(temp);
        temp.transform.SetParent(instanceRoot);//�̰� �׽�Ʈ .
    }
    #endregion
}
