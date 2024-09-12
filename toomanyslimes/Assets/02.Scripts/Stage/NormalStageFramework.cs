using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStageFramework : StageFramework
{
    Data.StageData CurrentStageData => DataManager.StageTable[stageIndex];
    long stageIndex;

    int RaceStageCount;
    int currentWaveProgress;
    public int CurrentStageProgress => currentWaveProgress;

    public override IEnumerator IEStageProcess(long _stageIndex)
    {
        stageIndex = _stageIndex;
        currentContentsResultState = eContentResultState.InProgress;
        RaceStageCount = CurrentStageData.WaveIndexArr.Length;

        yield return IEWaveProcess();
    }
    IEnumerator IEWaveProcess()
    {
        //배경을 움직이자 .
        BackgroundManager.Instance.ReqisterBGResetAction(SpawnWave);
        BackgroundManager.Instance.IsBGMove = true;
        //이제 게임의 진행을 체크
        while (currentContentsResultState == eContentResultState.InProgress)
        {
            CheckStageState();
            yield return null;
        }

        //해당 게임이 마무리 되었다면
        switch (currentContentsResultState)
        {
            case eContentResultState.Victory:
                yield return ExitStage(() =>Debug.Log("Rmx"),1f);
                break;
            case eContentResultState.Defeat:
                break;
        }
    }
    void SpawnWave()
    {
        if (currentWaveProgress >= RaceStageCount)
        {
            BackgroundManager.Instance.IsBGMove=false;
            Debug.Log("모두 소진");
            return;
        }
        currentWaveProgress++;
        Data.WaveData waveData = DataManager.WaveTable[currentWaveProgress];
        SpawnManager.Instance.SpawnWave(waveData, CurrentStageData.MonsterIndexArr);
    }
    void CheckStageState()
    {

    }
    //플레이어 혹은 보스 몬스터의 죽음을 체크함
    //IEnumerator IESubProcess()
    //{
    //    //플레이어의 죽음을 체크
    //    if (Player.PlayerCharacter.FSMState == eFSMState.Death)
    //    {
    //        _currentContentsResultState = eContentResultState.Defeat;
    //        break;
    //    }

    //    if (constructure.LoadActor.FSMState == eFSMState.Death)
    //    {
    //        _currentContentsResultState = eContentResultState.Victory;
    //        break;
    //    }
    //}
    public void ASDASD()
    {
        //리셋이 진행 될 때마다 웨이브가 해당 리셋 장소에 설치되면 됨 .
        
    }
}
public enum eStageType
{
    Race,
    Loop,
    Boss,
}
