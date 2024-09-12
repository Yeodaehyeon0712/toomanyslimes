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
        //데이터 매니저로부터 해당 인덱스의 데이터를 받는다 .

        //만약 해당 풀에 해당 종류의 액터가 없다면 풀을 생성
        if (actorPool[type].ContainsKey(pathHash)==false)
            actorPool[type].Add(pathHash, new MemoryPool<Actor>(20));

        //해당 풀로부터 액터를 가져온다
        T spawnedActor = actorPool[type][pathHash].GetItem() as T;

        //만약 액터가 없다면 생성해준다
        if(spawnedActor==null)
        {
            T prefabs=Resources.Load<T>(resourcePath);

            T cloneAsset =Object.Instantiate(prefabs, spawnPoint);
            cloneAsset.Initialize();
            spawnedActor = cloneAsset;
        }
        else//풀에 있다면
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
        //사용후 돌려놓는 과정
        //actorPool[temp.ActorType][temp.SpawnHashCode].Register(temp);
        temp.transform.SetParent(instanceRoot);//이건 테스트 .
    }
    #endregion
}
