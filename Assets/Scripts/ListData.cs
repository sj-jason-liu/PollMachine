using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListData
{
    public List<int> listData;

    public ListData (DataInputPanel data)
    {
        listData = data.exceptions;
    }
}
