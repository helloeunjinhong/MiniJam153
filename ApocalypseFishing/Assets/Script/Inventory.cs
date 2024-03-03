using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private List<GameObject> itemList;

    public Inventory()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(GameObject item)
    {
        itemList.Add(item);
    }

    public List<GameObject> GetItemList()
    {
        return itemList;
    }
}
