using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using UnityStandardAssets.CrossPlatformInput;

public class ScriptManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset _inkJSONFile;

    private Story _storyScipt;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    void Awake()
    {
        LoadStory();
        GetNextLine();
    }

    void LoadStory()
    {
        _storyScipt = new Story(_inkJSONFile.text);

        // bind to use in ink script
        _storyScipt.BindExternalFunction("Name", (string charName) => ChangeName(charName));
    }

    public void GetNextLine()
    {
        // a check to see if can go to next line, have content to go to
        if (_storyScipt.canContinue)
        {
            // gets next line
            string text = _storyScipt.Continue();

            // removes white space
            text = text.Trim();

            // show text in UI text
            dialogueText.text = text;
        }
        else
        {
            dialogueText.text = "";
        }
    }

    public void ChangeName(string name)
    {
        // get name
        string speakerName = name;

        // store in the UI
        nameText.text = speakerName;
    }

    // Update is called once per frame
    void Update()
    {
        GameInput();
    }

    void GameInput()
    {
        // for mobile press and mouse click
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            GetNextLine();
        }
    }

    
}
