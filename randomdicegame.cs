using UnityEngine;
using TMPro;
using UnityEngine.UI;
// Random Dice Game

public class DiceGame : MonoBehaviour {
    public string inputValue = "1";
    public TMP_Text outputText;
    public TMP_InputField inputField;
    public Button button;
    int throwDice() {
        Debug.Log("Throwing dice...");
        Debug.Log("Finding random between 1 to 6...");
        int diceResult = Random.Range(1,7);
        Debug.Log($"Result: {diceResult}");
        return diceResult;
    }

public void processGame() {
    inputValue = inputField.text;
    try {
        int inputInteger = int.Parse(inputValue);
        int totalSix = 0;
        for (var i = 0; i < 10; i++) {
            var diceResult = throwDice();
            if (diceResult == 6) { totalSix++; }
            if (diceResult == inputInteger) {
                outputText.text = $"DICE RESULT:
                    {diceResult} \r\nYOU WIN!";
            } else {
                outputText.text = $"DICE RESULT:
                {diceResult} \r\nYOU LOSE!";
            }
       }
       Debug.Log($"Total of six: {totalSix}");
       } catch {
       outputText.text = "Input is not a number!";
       Debug.LogError("Input is not a number!");
       }
    }
}
