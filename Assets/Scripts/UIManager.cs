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
    private GameObject _dataInputPanel;
    [SerializeField]
    private PollTube _pollTube;
    private Animator _pollTextAnimator;
    private DataInputPanel _dataInput;
    private bool _inputPanelSwitch = true;

    private void Start()
    {
        _dataInput = GetComponentInChildren<DataInputPanel>();
        if (_dataInput == null)
            Debug.LogError("DataInputPanel is NULL or MISSING!");
        _pollTextAnimator = GetComponent<Animator>();
        if (_pollTextAnimator == null)
            Debug.LogError("Poll Text Animator is NULL!");
    }

    public void HideInputPanel() //button to call out the input panel
    {
        _inputPanelSwitch = !_inputPanelSwitch;
        _dataInputPanel.SetActive(_inputPanelSwitch);
    }

    public void PollAnimation() //poll button function
    {
        _dataInput.CallANumber(); //poll a new number
        int polledNum = _dataInput.ReturnPolledNumber(); //receive number from DataInputPanel
        _polledNumText.text = polledNum.ToString();//replace the polled num text with polled number
        _pollTube.PlayAnimation(); //play tube animation
        _pollTextAnimator.SetTrigger("CallText"); //play polled num animation
    }
}
