using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntegerInputField : MonoBehaviour
{
    public int Value { get; private set; } = 30;
    
    public void ValidateInput(string newValue)
    {
        try
        {
            int valueAsInt = int.Parse(newValue);
            if (valueAsInt <= 0)
            {
                transform.GetComponent<TMP_InputField>().text = Value.ToString();
                return;
            }
            Value = valueAsInt;
        }
        catch (Exception e)
        {
            transform.GetComponent<TMP_InputField>().text = Value.ToString();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
