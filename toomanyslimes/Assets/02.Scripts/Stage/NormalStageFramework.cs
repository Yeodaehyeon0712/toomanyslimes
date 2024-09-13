using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStageFramework : StageFramework
{
    Data.StageData CurrentStageData => DataManager.StageTable[stageIndex];
    //To Do : 플레이어에 귀속
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
        BackgroundManager.Instance.IsBGMove = false;
        UIManager.Instance.MainUI.Disable();
        switch (currentContentsResultState)
        {
            case eContentResultState.Victory:
                {
                    Debug.Log("승리");
                    break;
                }
            case eContentResultState.Defeat:
                {
                    Debug.Log("패배");
                break;
                }
        }
    }
    void SpawnWave()
    {
        currentWaveProgress++;
        Data.WaveData waveData = DataManager.WaveTable[currentWaveProgress];
        SpawnManager.Instance.SpawnWave(waveData, CurrentStageData.MonsterIndexArr,CurrentStageData.BossIndex);
        if (currentWaveProgress >= RaceStageCount)
        {
            BackgroundManager.Instance.IsLastWave = true;
            Debug.Log("마지막 웨이브");
        }
    }
    void CheckStageState()
    {
        //플레이어가 죽었다면
        if (Player.PlayerCharacter.FSMState == eFSMState.Death)
        {
            currentContentsResultState = eContentResultState.Defeat;
            return;
        }
    }
}

