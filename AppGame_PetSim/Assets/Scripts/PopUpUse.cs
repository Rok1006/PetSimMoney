using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpUse : MonoBehaviour
{
    public void OnClickItem()
    {
        if(Inventory.instance.PopUpItem != null)
        {
            GameObject pop = Inventory.instance.PopUpItem;
            pop.transform.Find("ItemUIPlacement").gameObject.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
            pop.transform.Find("ItemName").gameObject.GetComponent<Text>().text = gameObject.GetComponent<ItemsInfo>().itemName;
            pop.transform.Find("Descript").gameObject.GetComponent<Text>().text = gameObject.GetComponent<ItemsInfo>().description;
            Inventory.instance.itemSelected = gameObject;
            pop.SetActive(true);
        }
    }
}
