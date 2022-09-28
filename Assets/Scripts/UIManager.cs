using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UIManager is NULL!");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private Text _polledNum;
    [SerializeField]
    private GameObject _dataInputPanel;
    [SerializeField]
    private PollTube _pollTube;
    private bool _inputPanelSwitch = true;

    public void HideInputPanel() //button to call out the input panel
    {
        _inputPanelSwitch = !_inputPanelSwitch;
        _dataInputPanel.SetActive(_inputPanelSwitch);
    }

    public void PollAnimation()
    {
        _pollTube.PlayAnimation();
    }
}
