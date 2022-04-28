using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharacterScript
{
    [Header("Name of character")]
    public string characterName;

    [Header("Number of textures for character")]
    public int numberOfTextures;

    // may need change to list as each character will have many textures 
    private List<Texture2D> characterModels;
    private List<Texture2D> characterFaces;

    private string _pathCharactersBody = "Characters/Body/";
    private string _pathCharactersFace = "Characters/Face/";

    /// <summary>
    /// Get textures belonging to the characters
    /// The number of textures is represented by var specified by user
    /// </summary>
    public void LoadTextures()
    {
        InitList();

        //Load a Textures of characters
        for (int i = 0; i < numberOfTextures; ++i)
        {
            var bodyTexture = Resources.Load<Texture2D>(_pathCharactersBody + characterName + i.ToString());
            var faceTexture = Resources.Load<Texture2D>(_pathCharactersFace + characterName + i.ToString());

            if (bodyTexture)
            {
                characterModels.Add(bodyTexture);
            }

            if (faceTexture)
            {
                characterFaces.Add(faceTexture);
            }
        }
    }

    /// <summary>
    /// Init list of textures if not already init before
    /// </summary>
    void InitList()
    {
        if (characterModels == null)
        {
            characterModels = new List<Texture2D>();
        }

        if (characterFaces == null)
        {
            characterFaces = new List<Texture2D>();
        }
    }

}
