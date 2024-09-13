using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eContentResultState
{
    InProgress,
    Victory,
    Defeat,
}
public abstract class StageFramework
{
    #region Fields
    protected eContentResultState currentContentsResultState;
    #endregion

    #region Framework Method
    //프레임 워크에서 공통적인 준비 .
    public virtual IEnumerator SetupStage(long stageIndex)
    {
        BackgroundManager.Instance.SetBackground(DataManager.StageTable[stageIndex].BGIndex);
        UIManager.Instance.ShowUIByContentType(StageManager.Instance.CurrentStageType);
        Player.PlayerCharacter = SpawnManager.Instance.SpawnCharacter<Actor>(1);
        yield return null;
    }
    public abstract IEnumerator IEStageProcess(long stageIndex);
    protected IEnumerator ExitStage(UnityEngine.Events.UnityAction afterAction,float time)
    {
        yield return null;
        yield return new WaitForSeconds(time);
        afterAction?.Invoke();
    }
    public virtual void CompleteStage()
    {
        currentContentsResultState = eContentResultState.Victory;
    }
    public virtual IEnumerator CleanStage()
    {
        yield return null;
    }
    #endregion
}
