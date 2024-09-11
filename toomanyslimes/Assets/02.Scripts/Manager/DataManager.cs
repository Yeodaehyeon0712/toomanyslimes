using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : TSingletonMono<DataManager>
{

    public static WaveTable WaveTable;
    public static StageTable StageTable;

    protected override void OnInitialize()
    {
        WaveTable = LoadTable<WaveTable>(eTableName.WaveTable);
        WaveTable.Reload();
        StageTable = LoadTable<StageTable>(eTableName.StageTable);
        StageTable.Reload();

        IsLoad = true;      
    }

    public T LoadTable<T>(eTableName name, bool isReload = false) where T : TableBase, new()
    {
        T t = new T();
        t.SetTableName = name.ToString();
        return t;
    }
}
[System.Flags]
public enum eTableName
{
    WaveTable = 1 << 0,
    StageTable = 1 << 1,
    All = ~0,
}
