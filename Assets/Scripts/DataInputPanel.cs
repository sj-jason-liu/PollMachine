using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DataInputPanel : MonoBehaviour
{
    public InputField minInput; //minimum seat number input
    public InputField maxInput; //maximum seat number input
    public InputField exceptInput; //exception seat number input
    public InputField conMinInput; //minimum of continuous number
    public InputField conMaxInput; //maximum of continuous number
    public List<int> exceptions; //exception list
    public List<string> eduCenters;
    public Text listExceptions;
    public Text pollRange;
    //public Text calledIntText;
    public int minInteger;
    public int maxInteger;
    private int _calledInteger;
    private int _halfInt;
    private int _moduleNum = 0;
    private int callNum;
    private int serialCounts;
    private int _defaultListCounts; //set the default counts of list at the first poll
    private bool _hasSetDefaultListCounts;
    private bool _hasPolledFirstNum;
    
    void Start()
    {
        listExceptions.text = ListToText(exceptions);
        serialCounts = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            AddException();
        }
    }

    public void SetMaximum() //set the maximum num of list
    {
        if (!string.IsNullOrEmpty(maxInput.text) && !string.IsNullOrEmpty(minInput.text)) //check if the column of maximum input is null
        {
            if (int.Parse(maxInput.text) > 0) //if the input num is greater than 0, then it would works
            {
                minInteger = int.Parse(minInput.text); //store the input num of min
                maxInteger = int.Parse(maxInput.text); //store the input num as int variable
                //_halfInt = Mathf.RoundToInt((1 + maxInteger)/2); //storage half of range
                minInput.text = null;
                maxInput.text = null;
                pollRange.text = "抽獎數值範圍:\n" + minInteger + " - " + maxInteger;
            }
            else
            {
                //error text
            }
        } 
    }

    public void AddContinuousExceptions()
    {
        //check if both inputfield is not null and greater than 1
        if (!string.IsNullOrEmpty(conMinInput.text) && !string.IsNullOrEmpty(conMaxInput.text)
            && int.Parse(conMinInput.text) > 0 && int.Parse(conMaxInput.text) > 0)
        {
            //get several numbers from min to max by loop
            for (int i = int.Parse(conMinInput.text); i < int.Parse(conMaxInput.text)+1; i++)
            {
                //add these numbers into exceptions list
                var existInt = exceptions.Contains(i);

                if(existInt)
                {
                    exceptions.Remove(i);
                }
                else
                {
                    exceptions.Add(i);
                    var newList = exceptions.Distinct();
                    exceptions = newList.ToList();
                }
            }

            conMinInput.text = null;
            conMaxInput.text = null;
            listExceptions.text = ListToText(exceptions); //print it to text
        }
    }

    public void AddException() //set the exception num of list
    {
        //check if input is not null and greater than 1
        if(!string.IsNullOrEmpty(exceptInput.text) && int.Parse(exceptInput.text) > 0)
        {
            int inputInt = int.Parse(exceptInput.text); //store input num as variable
            var existInt = exceptions.Contains(inputInt); //check if there is a same int already

            if(existInt) //if existed then remove it
            {
                exceptions.Remove(inputInt);
            }
            else //if not existed then add one
            {
                exceptions.Add(inputInt); //add the new value to list
                var newList = exceptions.Distinct(); //remove all the duplicated int
                exceptions = newList.ToList(); //cover the old list with new content
            }

            exceptInput.text = null;
            listExceptions.text = ListToText(exceptions); //print it to text
        } 
    }

    public void ClearExceptions()
    {
        exceptions.Clear();
        listExceptions.text = ListToText(exceptions); //print it to text
    }

    //function of calling a new number from list
    public int CallANumber()
    {
        if(!_hasSetDefaultListCounts) //set default counts at the first poll
            _defaultListCounts = exceptions.Count;
            _hasSetDefaultListCounts = true;
        StartCoroutine(CallNewNumber());
        return _calledInteger;
    }

    IEnumerator CallNewNumber()
    {
        
        int callNum = Random.Range(minInteger, maxInteger + 1); //call a random integer from int function
        var existNum = exceptions.Contains(callNum); //check if random one has existed in the list
        if (existNum) //if existed
        {
            yield return new WaitForEndOfFrame(); //restart coroutine to receive a new number
        }
        else //if not
        {
            _calledInteger = callNum;
            //calledIntText.text = "Called: " + callNum.ToString(); 
            exceptions.Add(callNum);
            listExceptions.text = ListToText(exceptions);
        }
        

        // Module

        /*
        if(_hasPolledFirstNum)
            serialCounts++;
        if(serialCounts > 5)
            serialCounts -= 4;
        //Debug.Log("Current serialCounts is: " + serialCounts);
        if(_moduleNum == 1 || _moduleNum == 2)
        {
            switch(_moduleNum)
            {
                case 1: //1st polled greater than half
                    //Debug.Log("Enter Module: " + _moduleNum);
                    switch(serialCounts)
                    {
                        case 2:
                            //Debug.Log("Module: " + _moduleNum + ", case: " + serialCounts);
                            int firstPolled = exceptions[_defaultListCounts];
                            if((maxInteger - firstPolled) > (firstPolled - _halfInt))
                            {
                                callNum = Random.Range(firstPolled, maxInteger + 1);
                            }
                            else
                            {
                                callNum = Random.Range(_halfInt, firstPolled);
                            }
                            break;
                        case 3:
                            //Debug.Log("Module: " + _moduleNum + ", case: " + serialCounts);
                            int secondPolled = exceptions[_defaultListCounts + 1];
                            if((_halfInt - secondPolled) > (secondPolled - 1))
                            {
                                callNum = Random.Range(secondPolled, _halfInt + 1);
                            }
                            else
                            {
                                callNum = Random.Range(1, secondPolled);
                            }
                            break;
                        case 4:
                            //Debug.Log("Module: " + _moduleNum + ", case: " + serialCounts);
                            int first = exceptions[_defaultListCounts];
                            int thirdPolled = exceptions[_defaultListCounts + 2];
                            if(first > thirdPolled)
                            {
                                callNum = Random.Range(first + 1, maxInteger + 1);
                            }
                            else
                            {
                                callNum = Random.Range(_halfInt + 1, first);
                            }
                            break;
                        case 5:
                            //Debug.Log("Module: " + _moduleNum + ", case: " + serialCounts);
                            int second = exceptions[_defaultListCounts + 1];
                            int forthPolled = exceptions[_defaultListCounts + 3];
                            if(second > forthPolled)
                            {
                                callNum = Random.Range(second + 1, _halfInt + 1);
                            }
                            else
                            {
                                callNum = Random.Range(1, second);
                            }
                            break;
                    }
                    break;
                case 2: //1st polled less or equal to half
                    //Debug.Log("Enter Module: " + _moduleNum);
                    switch(serialCounts)
                    {
                        case 2:
                            //Debug.Log("Module: " + _moduleNum + ", case: " + serialCounts);
                            int firstPolled = exceptions[_defaultListCounts];
                            if((_halfInt - firstPolled) > (firstPolled - 1))
                            {
                                callNum = Random.Range(firstPolled + 1, _halfInt + 1);
                            }
                            else
                            {
                                callNum = Random.Range(1, firstPolled);
                            }
                            break;
                        case 3:
                            //Debug.Log("Module: " + _moduleNum + ", case: " + serialCounts);
                            int secndPolled = exceptions[_defaultListCounts];
                            if((maxInteger - secndPolled) > (secndPolled - _halfInt))
                            {
                                callNum = Random.Range(secndPolled + 1, maxInteger + 1);
                            }
                            else
                            {
                                callNum = Random.Range(_halfInt, secndPolled);
                            }
                            break;
                        case 4:
                            //Debug.Log("Module: " + _moduleNum + ", case: " + serialCounts);
                            int first = exceptions[_defaultListCounts];
                            int thirdPolled = exceptions[_defaultListCounts + 2];
                            if(first > thirdPolled)
                            {
                                callNum = Random.Range(first + 1, _halfInt + 1);
                            }
                            else
                            {
                                callNum = Random.Range(1, first);
                            }
                            break;
                        case 5:
                            //Debug.Log("Module: " + _moduleNum + ", case: " + serialCounts);
                            int second = exceptions[_defaultListCounts + 1];
                            int forthPolled = exceptions[_defaultListCounts + 3];
                            if(second > forthPolled)
                            {
                                callNum = Random.Range(second + 1, maxInteger + 1);
                            }
                            else
                            {
                                callNum = Random.Range(_halfInt, second);
                            }
                            break;
                    }
                    break;
            }
        }
        else
        {
            switch(serialCounts)
            {
                case 0:
                    //no number has been polled, poll a new 1
                    callNum = Random.Range(1, maxInteger + 1);
                    _hasPolledFirstNum = true;
                    break;
                case 1:
                    //the 1st has been polled
                    int firstPolled = exceptions[exceptions.Count - 1]; //get the last data of exceptions list
                    if(firstPolled > _halfInt)
                    {
                        callNum = Random.Range(1, _halfInt + 1);
                        _moduleNum = 1;
                    }
                    else
                    {
                        callNum = Random.Range(_halfInt, maxInteger + 1);
                        _moduleNum = 2;
                    }
                    break;
            }
        }
        */
        
        /*
        var existNum = exceptions.Contains(callNum); //check if random one has existed in the list
        if (existNum) //if existed
        {
            yield return new WaitForEndOfFrame(); //restart coroutine to receive a new number
        }
        else //if not
        {
            _calledInteger = callNum;
            //calledIntText.text = "Called: " + callNum.ToString(); 
            exceptions.Add(callNum);
            listExceptions.text = ListToText(exceptions);
        }
        */
    }

    public string CallAEduCenter() //return a random picked edu center
    {
        int pickedCenter = Random.Range(0, eduCenters.Count - 1);
        return eduCenters[pickedCenter];
    }

    private int RandomInteger()
    {
        int calledNum = (int)Random.Range(1f, maxInteger);
        //Debug.Log("Random number is: " + calledNum);
        return calledNum;
    }

    private string ListToText(List<int> list)
    {
        string result = "";
        foreach(int content in list)
        {
            result = result.ToString() + content.ToString() + ", ";
        }
        return result;
    }

    public void SaveList()
    {
        if(!string.IsNullOrEmpty(listExceptions.text))
        {
            SaveSystem.SaveData(this);
        }       
    }

    public void LoadList()
    {
        ListData data = SaveSystem.LoadData();

        exceptions = data.listData;
        listExceptions.text = ListToText(exceptions);
    }
}
