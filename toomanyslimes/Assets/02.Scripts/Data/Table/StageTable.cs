using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Data
{
    public class StageData
    {
        public readonly long[] WaveIndexArr;
        public readonly long[] MonsterIndexArr;
        //추후 아이템도 ..
        public readonly long BossIndex;
        public readonly long BGIndex;

        public StageData(Dictionary<string, string> dataPair)
        {
            WaveIndexArr = System.Array.ConvertAll(dataPair["WaveIndex"].Split('|'), v => long.Parse(v));
            MonsterIndexArr = System.Array.ConvertAll(dataPair["MonsterIndex"].Split('|'), v => long.Parse(v));
            BossIndex = long.Parse(dataPair["BossIndex"]);
            BGIndex = long.Parse(dataPair["BGIndex"]);
        }
    }
}
public class StageTable : TableBase
{
    Dictionary<long, Data.StageData> _stageDataDic = new Dictionary<long, Data.StageData>();
    public Data.StageData this[long index]
    {
        get
        {
            if (_stageDataDic.ContainsKey(index))
                return _stageDataDic[index];

            return null;
        }
    }
    protected override void OnLoad()
    {
        LoadData(_tableName);
        foreach (var contents in _dataDic)
            _stageDataDic.Add(contents.Key, new Data.StageData(contents.Value));
    }
}
