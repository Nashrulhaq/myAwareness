using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public enum CharacterExpressions
{ 
    None = 0,
    Happy,
    Sad,
    Angry,
    Confused
}

/// <summary>
/// Manages the characters in the scenes
/// </summary>
public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _characterManager;
    public static CharacterManager Instance
    {
        get
        {
            if (_characterManager == null)
            {
                Debug.LogError(message: "CharacterManager is null");
            }

            return _characterManager;
        }
    }

    public List<CharacterScript> allCharacters;


    void Awake()
    {
        _characterManager = this;

        // init the characters
        if (allCharacters.Count > 0)
        {
            foreach (var elem in allCharacters)
                elem.LoadTextures();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CharacterScript GetCharacter(string name, int characterExpression = (int)CharacterExpressions.None)
    {
        foreach (CharacterScript character in allCharacters)
        {
            if (character.characterName == name)
            {
                CharacterScript charToReturn = character;
                return character;
            }
        }

        return null;
    }

}
