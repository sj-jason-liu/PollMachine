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
    [SerializeField]
    private GameObject _howToPanel;
    private CanvasGroup _dataInputCanvasGroup;
    private bool _inputPanelSwitch = true;
    private bool _howToPanelSwitch = false;
    private bool _isEduCenterToggleOn = false;

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

    public void EduCenterToggleChanger()
    {
        _isEduCenterToggleOn = !_isEduCenterToggleOn;
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

    public void HowToPanelSwitch() //Hide/reveal How To Panel
    {
        _howToPanelSwitch = !_howToPanelSwitch;
        switch(_howToPanelSwitch)
        {
            case true:
                _howToPanel.SetActive(_howToPanelSwitch);
                break;
            case false:
                _howToPanel.SetActive(_howToPanelSwitch);
                break;
        }
    }

    public void PollAnimation() //poll button function
    {
        if(!_isEduCenterToggleOn) //if not in edu center meeting mode
        {
            int polledNum = _dataInput.CallANumber(); //poll a new number
            _polledNumText.text = polledNum.ToString();//replace the polled num text with polled number
        }
        else
        {
            string pickedCenter = _dataInput.CallAEduCenter(); //poll a center
            _polledNumText.text = pickedCenter; //show text on ball
        }
        _pollTube.PlayAnimation(); //play tube animation
        _pollTextAnimator.SetTrigger("CallText"); //play polled num animation
    }
}
