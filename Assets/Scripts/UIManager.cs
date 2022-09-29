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
    private Text _polledNumText;
    [SerializeField]
    private PollTube _pollTube;
    private Animator _pollTextAnimator;
    private DataInputPanel _dataInput;
    private CanvasGroup _dataInputCanvasGroup;
    private bool _inputPanelSwitch = true;

    private void Start()
    {
        _dataInput = GetComponentInChildren<DataInputPanel>();
        if (_dataInput == null)
            Debug.LogError("DataInputPanel is NULL or MISSING!");
        _pollTextAnimator = GetComponent<Animator>();
        if (_pollTextAnimator == null)
            Debug.LogError("Poll Text Animator is NULL!");
        _dataInputCanvasGroup = GetComponentInChildren<CanvasGroup>();
        if (_dataInputCanvasGroup == null)
            Debug.LogError("Canvas Group of DataInputPanel is MISSING!");
    }

    public void HideInputPanel() //button to call out the input panel
    {
        _inputPanelSwitch = !_inputPanelSwitch;
        switch(_inputPanelSwitch)
        {
            case true:
                _dataInputCanvasGroup.alpha = 1;
                break;
            case false:
                _dataInputCanvasGroup.alpha = 0;
                break;
        }
    }

    public void PollAnimation() //poll button function
    {
        int polledNum = _dataInput.CallANumber(); //poll a new number
        _polledNumText.text = polledNum.ToString();//replace the polled num text with polled number
        _pollTube.PlayAnimation(); //play tube animation
        _pollTextAnimator.SetTrigger("CallText"); //play polled num animation
    }
}
