using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : TSingletonMono<StageManager>
{
    #region Fields
    Data.StageData currentStageData;
    long currentStageIndex;// �ӽ�

    #endregion

    protected override void OnInitialize()
    {
        IsLoad = true;
    }
    public void SetupStage(long stageIndex)
    {
        //���������� �¾� . 
        // 1. �ʼ��� 2. ĳ���� ���� 3.���� ����
        // ������ �Ϸ�Ǹ� ���� �����δ� .
        // ���� ���������� ���� ���θ� ��� üũ�Ұ��� .
        // ���������� ������ ����� üũ�ϰ� ������ �Ͽ� UI�� ����ش� .

        StartCoroutine(IESetupStage(stageIndex));
        
    }
    IEnumerator IESetupStage(long stageIndex)
    {
        //���� ���������� �����͸� ����
        currentStageIndex = stageIndex;
        currentStageData = DataManager.StageTable[currentStageIndex];

        //���̺긦 �������� .
        SetNextWave();


        yield return null;
    }
    int maxWaveCount;
    int currentWaveIndex;
    public void SetNextWave()
    {
        //���� ġ��� �ϴ� ���̺��� ��
        maxWaveCount = currentStageData.WaveIndexArr.Length;
        //���� ���̺��� ������
        var nextWaveIndex=currentStageData.WaveIndexArr[currentWaveIndex];
        Data.WaveData waveData = DataManager.WaveTable[nextWaveIndex];
        //�ش� ���̺� �����͸� ������� ���������� ��ҵ��� ����
        SpawnManager.Instance.SpawnWave(waveData, currentStageData.MonsterIndexArr);

        //������� ���̺갡 ���õǾ��ٸ� ���������� ����

    }

    IEnumerator IEWarpSetupStage(long stageIndex)
    {
        yield return null;
    }
}
