using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eContentsType
{
    Normal,
    End,
}
public class StageManager : TSingletonMono<StageManager>
{
    #region Fields
    bool isChangingStage;
    [SerializeField] eContentsType prevStage = eContentsType.End;
    [SerializeField] eContentsType currStage = eContentsType.End;
    public eContentsType CurrentStageType => currStage;
    Coroutine mainProcess;
    Coroutine stageFramework;

    Dictionary<eContentsType, StageFramework> stageFrameworkDic = new Dictionary<eContentsType, StageFramework>((int)eContentsType.End)
    {
        { eContentsType.Normal, new NormalStageFramework() },
    };
    #endregion

    protected override void OnInitialize()
    {
        IsLoad = true;
    }

    public void SetupStage(eContentsType type,long stageIndex)
    {
        if (isChangingStage) return;

        isChangingStage = true;
        if (mainProcess != null)
            StopCoroutine(mainProcess);

        prevStage = currStage;
        currStage = type;
        mainProcess = StartCoroutine(IESetupStage(stageIndex));    
    }
    IEnumerator IESetupStage(long stageIndex)
    {
        if (stageFramework != null)
            StopCoroutine(stageFramework);
        
        if (stageFrameworkDic.ContainsKey(prevStage))
            yield return stageFrameworkDic[prevStage].CleanStage();

        yield return stageFrameworkDic[currStage].SetupStage(stageIndex);
        stageFramework = StartCoroutine(stageFrameworkDic[currStage].IEStageProcess(stageIndex));

        mainProcess = null;
        isChangingStage = false;
    }
    public T GetFramework<T>(eContentsType type) where T : StageFramework
    {
        return stageFrameworkDic[type] as T;
    }
    public void CompleteStage()
    {
        stageFrameworkDic[currStage].CompleteStage();
    }
}
