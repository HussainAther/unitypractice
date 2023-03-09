using UnityEngine;
using UnityEngine.Events;

public class Digicode : MonoBehaviour
{
    public string ValidCode;
    public GameObject Display;
    public UnityEvent OnValidCodeEntered;

    private string enteredCode = "";
    private TextMesh displayTextMesh = null;

    public void Start()
    {
        if (Display != null)
            displayTextMesh = Display.GetComponentInChildren<TextMesh>();
    }

    public void ButtonPressed(string value)
    {
        if (value == "C")
            enteredCode = "";
        else
        {
            if (enteredCode.Length >= ValidCode.Length)
                enteredCode = value;
            else
                enteredCode += value;
        }

        if (displayTextMesh)
            displayTextMesh.text = enteredCode;

        if (enteredCode == ValidCode)
            OnValidCodeEntered.Invoke();
    }
}
