using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrouselComponent : MonoBehaviour
{
    #region [ VARIABLES ]

    [SerializeField] private List<CarrouselItem> carrouselItems;
    [SerializeField] private GameObject item;
    [SerializeField] private RectTransform[] points;

    internal List<Item> items;
    private int selectedPointIndex = 2;
    private int selectedItemIndex;

    #endregion

    #region [ UNITY METHODS ]

    private void OnEnable()
    {
        CreateItems();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            RollItems(1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            RollItems(-1);
        }
    }

    #endregion

    #region [ METHODS ]

    private void CreateItems()
    {
        CleanItems();
        carrouselItems = new List<CarrouselItem>();

        foreach (var i in points)
        {
            var _item = Instantiate(item, i.position, i.rotation, transform);
            _item.transform.localScale = i.localScale;

            carrouselItems.Add(_item.GetComponent<CarrouselItem>());
        }

        InitializeItems(items, carrouselItems);
        selectedItemIndex = carrouselItems.Count / 2;
    }

    private void InitializeItems(List<Item> _items, List<CarrouselItem> _carrouselItems)
    {
        int itemIndex = 0;

        foreach (var item in _carrouselItems)
        {
            if (itemIndex == _items.Count) itemIndex = 0;

            item.initializeItem(_items[itemIndex]);
            itemIndex++;
        }
    }

    private void RollItems(int direction)
    {
        int _initialPointIndex = selectedPointIndex - (2 * direction);
        int _selectedItemIndex = ArrayLoopIncrement(selectedItemIndex, -direction, carrouselItems.Count - 1); ;

        for (int i = 0; i < points.Length; i++)
        {

            if (i == points.Length - 1)
            {
                carrouselItems[_selectedItemIndex].manipulateItem(points[_initialPointIndex], false);
                break;
            }

            carrouselItems[_selectedItemIndex].manipulateItem(points[_initialPointIndex]);

            _initialPointIndex = ArrayLoopIncrement(_initialPointIndex, direction, points.Length - 1);
            _selectedItemIndex = ArrayLoopIncrement(_selectedItemIndex, direction, carrouselItems.Count - 1);
        }

        selectedItemIndex = ArrayLoopIncrement(selectedItemIndex, direction, carrouselItems.Count - 1);
    }

    private int ArrayLoopIncrement(int actualIndex, int increment, int maxIndex)
    {
        int newIndex = actualIndex + increment;

        if (newIndex < 0) return maxIndex;
        else if (newIndex > maxIndex) return 0;

        return newIndex;
    }

    internal Item GetSelectedItem()
    {
        var item = carrouselItems[selectedItemIndex];
        item.ChooseItem();

        return item.itemConfig;
    }

    internal void CleanItems()
    {
        foreach (var item in carrouselItems)
        {
            Destroy(item.gameObject);
        }
    }

    #endregion
}

#region [ STRUCTS ]

[Serializable]
public struct Item
{
    [SerializeField] internal ItemData data;
    [SerializeField] internal bool acquired;
    [SerializeField] internal bool showPrice;

}

#endregion
