using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Data
{
    public class WaveData
    {
        public readonly List<long[]> Wave;
        public WaveData(Dictionary<string, string> dataPair)
        {
            int maxRow = dataPair.Count - 1;
            Wave = new List<long[]>(maxRow);

            for (int row = 0; row < maxRow; row++)
            {
                string key = $"Row{row + 1}";

                if (dataPair.TryGetValue(key, out string columnData))
                {
                    var columnArray = System.Array.ConvertAll(columnData.Split('|'), long.Parse);

                    if (columnArray.Length != 5)
                    {
                        Debug.LogWarning($"{key} does not contain exactly 5 elements.");
                        continue;
                    }
                    Wave.Add(columnArray);
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
