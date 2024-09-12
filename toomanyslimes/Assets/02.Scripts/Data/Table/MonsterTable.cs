using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class MonsterData
    {
        public double HP;
        public float AttackDamage;
        public float AttackSpeed;
        public string ResourcePath;
        public int PathHash;
        public bool IsBoss;
        public bool IsRangeAttack;

        public MonsterData(Dictionary<string, string> dataPair)
        {

            HP = double.Parse(dataPair["HP"]);
            AttackDamage = float.Parse(dataPair["AttackDamage"]);
            AttackSpeed = float.Parse(dataPair["AttackSpeed"]);
            ResourcePath = dataPair["ResourcePath"];
            PathHash = ResourcePath.GetHashCode();
            IsBoss = bool.Parse(dataPair["IsBoss"]);
            IsBoss = bool.Parse(dataPair["IsRangedAttack"]);
        }
    }
}

public class MonsterTable : TableBase
{
    Dictionary<long, Data.MonsterData> _monsterDataDic = new Dictionary<long, Data.MonsterData>();
    public Data.MonsterData this[long index]
    {
        get
        {
            if (_monsterDataDic.ContainsKey(index))
                return _monsterDataDic[index];

            return null;
        }
    }
    protected override void OnLoad()
    {
        LoadData(_tableName);
        foreach (var contents in _dataDic)
            _monsterDataDic.Add(contents.Key, new Data.MonsterData(contents.Value));
    }
}
