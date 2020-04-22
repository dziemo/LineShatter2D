using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDisplayer : MonoBehaviour
{
    public IntVariable displayValue;
    public string preText;
    public TextMeshProUGUI textComponent;

    public void DisplayText ()
    {
        textComponent.text = preText + '\n' + displayValue.RuntimeValue;
    }
}
