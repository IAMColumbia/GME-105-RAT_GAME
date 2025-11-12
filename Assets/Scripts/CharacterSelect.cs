using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    private int currentIndex = 0;
    private List<string> characterList = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        characterList.Add("Pawn");
        characterList.Add("Knight");
        characterList.Add("Rook");
        characterList.Add("Bishop");
        characterList.Add("Queen");
        characterList.Add("King");

        nameText.text = characterList[currentIndex];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextText()
    {
        currentIndex = (currentIndex + 1) % characterList.Count;
        UpdateText();

    }

    public void PreviousText()
    {
        currentIndex = (currentIndex - 1) % characterList.Count;
        UpdateText();

    }

    public void UpdateText() {
        if (nameText != null && characterList.Count > 0)
        {
            nameText.text = characterList[currentIndex];
        }

    }
}
