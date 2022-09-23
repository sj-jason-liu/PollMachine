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
    //public int[] exceptions;
    public Text listExceptions;
    public Text pollRange;
    private int _maxInteger;
    private string exceptList = "";
    
    void Start()
    {
        //exceptions = new List<int>();
        listExceptions.text = ListToText(exceptions);
    }

    public void SetMaximum() //set the maximum num of list
    {
        if (!string.IsNullOrEmpty(maxInput.text)) //check if the column of maximum input is null
        {
            if (int.Parse(maxInput.text) > 0) //if the input num is greater than 0, then it would works
            {
                _maxInteger = int.Parse(maxInput.text); //store the input num as int variable
                pollRange.text = "The range is: 1 - " + _maxInteger;
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

    private string ListToText(List<int> list)
    {
        string result = "";
        foreach(int content in list)
        {
            result = result.ToString() + content.ToString() + ", ";
        }
        return result;
    }
}
