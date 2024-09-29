using System;
using System.Collections;
using System.Collections.Generic;
using Hearings.SaveSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class CurrentShop : MonoBehaviour
{
    [SerializeField] private Items items = new Items();

    [SerializeField] private List<GameObject> itemList = new List<GameObject>();
    [SerializeField] private List<Transform> shopPoints = new List<Transform>();
    [SerializeField] private List<GameObject> instantiadedObjects = new List<GameObject>();

    public int cashToAddToEnable = 5;

    private void Start()
    {
        if (SaveManager.Instance)
        {
            SaveManager.Instance.playerData.cash += cashToAddToEnable;
        }
    }

    private void Awake()
    {
        GenerateItemsInShop();
    }

    [Button]
    public void GenerateItemsInShop()
    {
        for (int i = 0; i < 3; i++)
        {
            int subItem = Random.Range(0, items.SubItemsList.Count);
            int lvlObject = Random.Range(0, 100);
        
            var currentShop = items.SubItemsList[subItem];

            if (items.SubItemsList[subItem].lv1.probability <= lvlObject)
            {
                itemList.Add(items.SubItemsList[subItem].lv1.itemObject);
            }
            else if (lvlObject > items.SubItemsList[subItem].lv1.probability && lvlObject < 
                     items.SubItemsList[subItem].lv1.probability + items.SubItemsList[subItem].lv2.probability)
            {
                itemList.Add(items.SubItemsList[subItem].lv2.itemObject);
            }
            else
            {
                itemList.Add(items.SubItemsList[subItem].lv3.itemObject);
            }
        }
        
        GenerateOnPoint(itemList);
        
    }

    [Button]
    public void ReloadShop()
    {
        itemList.Clear();
        
        foreach (var obj in instantiadedObjects)
        {
            Destroy(obj);
        }
        
        instantiadedObjects.Clear();
        
        GenerateItemsInShop();
    }

    public void GenerateOnPoint(List<GameObject> items)
    {
        for (int i = 0; i < shopPoints.Count; i++)
        {
            GameObject obj = Instantiate(itemList[i], shopPoints[i].position, shopPoints[i].rotation);
            instantiadedObjects.Add(obj);
        }
    }
}

[Serializable]
public struct Items
{
    public List<SubItems> SubItemsList;
}

[Serializable]
public struct SubItems
{
    public ItemParam lv1;
    public ItemParam lv2;
    public ItemParam lv3;
}

[Serializable]
public struct ItemParam
{
    public GameObject itemObject;
    [Range(0,100)]
    public int probability;
}
