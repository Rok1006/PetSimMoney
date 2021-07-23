using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Sorting
{
    public static void QuickSort(List<GameObject> itemList, int left, int right) 
    {
        if (left < right)
        {
            int pivot = Partition(itemList, left, right);

            //if (pivot > 1) {
                QuickSort(itemList, left, pivot - 1);
            //}
            //if (pivot + 1 < right) {
                QuickSort(itemList, pivot + 1, right);
            //}
        }
    
    }

    private static int Partition(List<GameObject> itemList, int left, int right)
    {
        ItemsInfo rightInfo = itemList[right].GetComponent<ItemsInfo>();
        ItemType pivotType = rightInfo.type;
        Rarity pivotRarity = rightInfo.rarity;
        int pivotId = 0;
        Int32.TryParse(rightInfo.itemID.Substring(2,2), out pivotId);
        
        int leftIndex = (left - 1);
        for (int j = left; j < right; j++)
        {
            ItemsInfo itemsInfo = itemList[j].GetComponent<ItemsInfo>();
            int itemID = 0;
            Int32.TryParse(itemsInfo.itemID.Substring(2,2), out itemID);
            if (itemsInfo.type > pivotType)
            {
                leftIndex++;

                GameObject temp = itemList[leftIndex];
                itemList[leftIndex] = itemList[j];
                itemList[j] = temp;
            }
            else if(itemsInfo.type == pivotType)
            {
                if (itemsInfo.rarity > pivotRarity)
                {
                        leftIndex++;

                        GameObject temp = itemList[leftIndex];
                        itemList[leftIndex] = itemList[j];
                        itemList[j] = temp;
                }
                else if(itemsInfo.rarity == pivotRarity)
                {
                    if(itemID > pivotId)
                    {
                        leftIndex++;

                        GameObject temp = itemList[leftIndex];
                        itemList[leftIndex] = itemList[j];
                        itemList[j] = temp;
                    }
                }
            }
        }

        GameObject temp1 = itemList[leftIndex + 1];
        itemList[leftIndex + 1] = itemList[right];
        itemList[right] = temp1;

        return leftIndex + 1;
    }

    public static void QuickSortReward(List<Reward> itemList, int left, int right) 
    {
        if (left < right)
        {
            int pivot = PartitionReward(itemList, left, right);

            //if (pivot > 1) {
                QuickSortReward(itemList, left, pivot - 1);
            //}
            //if (pivot + 1 < right) {
                QuickSortReward(itemList, pivot + 1, right);
            //}
        }
    
    }

    private static int PartitionReward(List<Reward> itemList, int left, int right)
    {
        ItemsInfo rightInfo = itemList[right].item.GetComponent<ItemsInfo>();
        ItemType pivotType = rightInfo.type;
        Rarity pivotRarity = rightInfo.rarity;
        int pivotId = 0;
        Int32.TryParse(rightInfo.itemID.Substring(2,2), out pivotId);
        
        int leftIndex = (left - 1);
        for (int j = left; j < right; j++)
        {
            ItemsInfo itemsInfo = itemList[j].item.GetComponent<ItemsInfo>();
            int itemID = 0;
            Int32.TryParse(itemsInfo.itemID.Substring(2,2), out itemID);
            if (itemsInfo.type < pivotType)
            {
                leftIndex++;

                Reward temp = itemList[leftIndex];
                itemList[leftIndex] = itemList[j];
                itemList[j] = temp;
            }
            else if(itemsInfo.type == pivotType)
            {
                if (itemsInfo.rarity < pivotRarity)
                {
                        leftIndex++;

                        Reward temp = itemList[leftIndex];
                        itemList[leftIndex] = itemList[j];
                        itemList[j] = temp;
                }
                else if(itemsInfo.rarity == pivotRarity)
                {
                    if(itemID < pivotId)
                    {
                        leftIndex++;

                        Reward temp = itemList[leftIndex];
                        itemList[leftIndex] = itemList[j];
                        itemList[j] = temp;
                    }
                    else if(itemsInfo.rarity == pivotRarity)
                    {
                        if(itemList[j].amount > itemList[right].amount)
                        {
                            leftIndex++;

                            Reward temp = itemList[leftIndex];
                            itemList[leftIndex] = itemList[j];
                            itemList[j] = temp;
                        }
                    }
                }
            }
        }

        Reward temp1 = itemList[leftIndex + 1];
        itemList[leftIndex + 1] = itemList[right];
        itemList[right] = temp1;

        return leftIndex + 1;
    }
}
