using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaxIntData
{
    public int maxIntData;

    public MaxIntData(DataInputPanel dataInputPanel)
    {
        maxIntData = dataInputPanel.maxInteger;
    }
}
