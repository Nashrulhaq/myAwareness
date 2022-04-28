using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterExpressions
{ 
    Happy = 0,
    Sad,
    Angry,
    Confused
}

public class CharacterManager : MonoBehaviour
{
    public List<CharacterScript> allCharacters;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CharacterScript GetCharacter(string name, string characterExpression)
    {
        foreach (CharacterScript character in allCharacters)
        {
            if (character.name == name)
            {
                CharacterScript charToReturn = character;
                return character;
            }
        }

        return null;
    }
}
