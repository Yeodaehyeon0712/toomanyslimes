using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Data
{
    public class CharacterData
    {
        public string ResourcePath;
        public int PathHash;

        public CharacterData(Dictionary<string, string> dataPair)
        {
            ResourcePath = dataPair["ResourcePath"];
            PathHash = ResourcePath.GetHashCode();
        }
    }
}
public class CharacterTable : TableBase
{
    Dictionary<long, Data.CharacterData> _characterDataDic = new Dictionary<long, Data.CharacterData>();
    public Data.CharacterData this[long index]
    {
        get
        {
            if (_characterDataDic.ContainsKey(index))
                return _characterDataDic[index];

            return null;
        }
    }
    protected override void OnLoad()
    {
        LoadData(_tableName);
        foreach (var contents in _dataDic)
            _characterDataDic.Add(contents.Key, new Data.CharacterData(contents.Value));
    }
}
