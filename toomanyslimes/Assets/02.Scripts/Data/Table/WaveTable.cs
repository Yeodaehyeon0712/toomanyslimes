using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eWaveType
{
    None,
    Enemy,
    Item,
    Area,
    Boss,
    End,
}
namespace Data
{
    public class WaveData
    {
        public readonly eWaveType[,] Wave2DArray;

        public WaveData(Dictionary<string, string> dataPair)
        {
            int rowCount = dataPair.Count - 1;
            int colCount = 5;//이건 생각좀 ..

            Wave2DArray = new eWaveType[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
            {
                string key = $"Row{i + 1}";
                if (dataPair.TryGetValue(key, out string columnData))
                {
                    var columnArray = System.Array.ConvertAll(columnData.Split('|'), long.Parse);

                    if (columnArray.Length != colCount)
                    {
                        Debug.LogWarning($"{key} does not contain exactly 5 elements.");
                        continue;
                    }

                    for (int j = 0; j < colCount; j++)
                        Wave2DArray[i, j] = (eWaveType)columnArray[j];
                }
            }
        }
    }
}
public class WaveTable : TableBase
{
    Dictionary<long, Data.WaveData> _waveDataDic = new Dictionary<long, Data.WaveData>();
    public Data.WaveData this[long index]
    {
        get
        {
            if (_waveDataDic.ContainsKey(index))
                return _waveDataDic[index];

            return null;
        }
    }
    protected override void OnLoad()
    {
        LoadData(_tableName);
        foreach (var contents in _dataDic)
            _waveDataDic.Add(contents.Key, new Data.WaveData(contents.Value));
    }
}
