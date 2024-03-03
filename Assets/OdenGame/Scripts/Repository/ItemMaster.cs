using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "OdenGame/CreateItemMaster", fileName = "ItemMaster")]
public class ItemMaster  : ScriptableObjectInstaller
{

    [Serializable]
    public class ItemParam
    {
        public int id;
        public string name;
        public string detail;
        public string fileName;
    }
    
    [SerializeField] private ItemParam[] itemParams;
    public ItemParam[] Data
    {
        get { return itemParams; }
    }

    public ItemParam GetItem(int itemId)
    {
        foreach (var param in itemParams)
        {
            if (param.id == itemId)
            {
                return param;
            }
        }

        return null;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(this);
    }
}
