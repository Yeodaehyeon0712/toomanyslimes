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
        actorPool = new Dictionary<eActorType, Dictionary<int,MemoryPool<Actor>>>();
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

        var data = GetDataByType(type, index);
        string resourcePath = data.resourcePath;
        int pathHash = data.pathHash;

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
    public (string resourcePath,int pathHash) GetDataByType(eActorType type,long index)
    {
        string resourcePath=null;
        int pathHash = 0;

        switch (type)
        {
            case eActorType.Player:
                {
                    resourcePath = DataManager.CharacterTable[index].ResourcePath;
                    pathHash = DataManager.CharacterTable[index].PathHash;
                }
                break;
            case eActorType.Enemy:
                {
                    resourcePath = DataManager.MonsterTable[index].ResourcePath;
                    pathHash = DataManager.MonsterTable[index].PathHash;
                }
                break;
        }
        return (resourcePath,pathHash);
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
