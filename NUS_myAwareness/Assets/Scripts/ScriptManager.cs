using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using UnityStandardAssets.CrossPlatformInput;

public class ScriptManager : MonoBehaviour
{
    private static ScriptManager _scriptManager;
    public static ScriptManager Instance
    {
        get
        {
            if (_scriptManager == null)
            {
                Debug.LogError(message:"ScriptManager is null");
            }

            return _scriptManager;
        }
    }

    [SerializeField]
    private TextAsset _inkJSONFile;

    private Story _storyScipt;
    public Story storyScript
    {
        get { return _storyScipt; }
    }

    // list of tags specified in ink document
    private List<string> _tags;

    [Header("UI related items to dialogue")]
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    [Header("Variables for Script Manager")]
    public float typeSpeed = 0.05f;

    Coroutine coTypeEachWord;

    void Awake()
    {
        _scriptManager = this;

        LoadStory();
        GetNextLine();
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

    void LoadStory()
    {
        _storyScipt = new Story(_inkJSONFile.text);

        // bind to use in ink script
        _storyScipt.BindExternalFunction("Name", (string charName) => ChangeName(charName));

        // from characterManager
        _storyScipt.BindExternalFunction
            ("Character", (string charName, int expressionIndex) => CharacterManager.Instance.GetCharacter(charName, expressionIndex));
    }

    /// <summary>
    /// Get next line of based on .INK file specified in inspector
    /// </summary>
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
            if (coTypeEachWord != null)
                StopCoroutine(coTypeEachWord);

            coTypeEachWord = StartCoroutine(TypeDialogue(text));
            //dialogueText.text = text;
        }
        else
        {
            dialogueText.text = "";
        }
    }

    /// <summary>
    /// Using tags to cue stuff in game from the dialogue
    /// </summary>
    //void ParseTags()
    //{
    //    _tags = _storyScipt.currentTags;
    //    foreach (string t in _tags)
    //    {
    //        string prefix = t.Split(' ')[0];
    //        string param = t.Split(' ')[1];

    //        switch (prefix.ToLower())
    //        {
    //            case "anim":
    //                break;
    //            case "color":
    //                SetTextColor();
    //                break;
    //        }
    //    }
    //}

    /// <summary>
    /// Type each word effect
    /// </summary>
    IEnumerator TypeDialogue(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    /// <summary>
    /// Get name based on binded function in ink file
    /// </summary>
    /// <param name="name">Name specified</param>
    public void ChangeName(string name)
    {
        // get name
        string speakerName = name;

        // store in the UI
        nameText.text = speakerName;
    }
    
}
