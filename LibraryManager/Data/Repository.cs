using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml;

namespace LibraryManager.Data;

public class Repository<T>
{
    private static Repository<T> _instance = new(new List<T>());
    private List<T> _items;

    public Repository(List<T> initialData)
    {
        _items = initialData;
    }

    public static void Initialize(List<T> initialData)
    {
        _instance = new Repository<T>(initialData);
    }

    public static List<T> GetAll()
    {
        return _instance._items;
    }

    public static void Update(T oldItem, T newItem)
    {
        if (oldItem == null || newItem == null)
        {
            return;
        }

        var index = _instance._items.IndexOf(oldItem);
        if (index == -1)
        {
            return;
        }

        _instance._items[index] = newItem;
    }

    public static void Delete(T item)
    {
        if (item == null)
        {
            return;
        }

        var index = _instance._items.IndexOf(item);

        if (index == -1)
        {
            return;
        }

        _instance._items.RemoveAt(index);
    }

    public static void UpdateAll(List<T> updatedList)
    {
        _instance._items = updatedList;
    }

    public static void Add(T item)
    {
        _instance._items.Add(item);
    }
}