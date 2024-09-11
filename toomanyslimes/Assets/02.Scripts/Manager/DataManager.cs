using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : TSingletonMono<DataManager>
{

    public static WaveTable WaveTable;
    protected override void OnInitialize()
    {
        WaveTable = LoadTable<WaveTable>(eTableName.WaveTable);
        WaveTable.Reload();

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
    All = ~0,
}
