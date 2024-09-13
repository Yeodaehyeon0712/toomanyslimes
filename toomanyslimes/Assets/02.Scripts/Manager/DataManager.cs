using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : TSingletonMono<DataManager>
{

    public static WaveTable WaveTable;
    public static StageTable StageTable;
    public static MonsterTable MonsterTable;
    public static CharacterTable CharacterTable;
    public static LocalizingTable LocalizingTable;

    protected override void OnInitialize()
    {
        WaveTable = LoadTable<WaveTable>(eTableName.WaveTable);
        WaveTable.Reload();

        StageTable = LoadTable<StageTable>(eTableName.StageTable);
        StageTable.Reload();

        MonsterTable = LoadTable<MonsterTable>(eTableName.MonsterTable);
        MonsterTable.Reload();

        CharacterTable = LoadTable<CharacterTable>(eTableName.CharacterTable);
        CharacterTable.Reload();

        LocalizingTable = LoadTable<LocalizingTable>(eTableName.LocalizingTable);
        LocalizingTable.Reload();

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
    MonsterTable = 1 << 2,
    CharacterTable=1<<3,
    LocalizingTable=1<<4,
    All = ~0,
}
