using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DataInputPanel : MonoBehaviour
{
    public InputField maxInput; //maximum seat number input
    public InputField exceptInput; //exception seat number input
    public List<int> exceptions; //exception list
    public List<string> eduCenters;
    public Text listExceptions;
    public Text pollRange;
    public Text calledIntText;
    [SerializeField]
    private int _maxInteger;
    private int _calledInteger;
    
    void Start()
    {
        listExceptions.text = ListToText(exceptions);
    }

    public void SetMaximum() //set the maximum num of list
    {
        if (!string.IsNullOrEmpty(maxInput.text)) //check if the column of maximum input is null
        {
            if (int.Parse(maxInput.text) > 0) //if the input num is greater than 0, then it would works
            {
                _maxInteger = int.Parse(maxInput.text); //store the input num as int variable
                pollRange.text = "抽獎數值範圍:\n1 - " + _maxInteger;
            }
            else
            {
                //error text
            }
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
        StartCoroutine(CallNewNumber());
        return _calledInteger;
    }

    IEnumerator CallNewNumber()
    {
        int callNum = Random.Range(1, _maxInteger + 1); //call a random integer from int function
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
    }

    public string CallAEduCenter() //return a random picked edu center
    {
        int pickedCenter = Random.Range(0, eduCenters.Count - 1);
        return eduCenters[pickedCenter];
    }

    private int RandomInteger()
    {
        int calledNum = (int)Random.Range(1f, _maxInteger);
        Debug.Log("Random number is: " + calledNum);
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
