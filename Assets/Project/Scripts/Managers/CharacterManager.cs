using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : BaseSingleton<CharacterManager>
{
    public Dictionary<string, CharacterSheet> CharacterSheets = new Dictionary<string, CharacterSheet>();

    public CharacterSheet CreateCharacterSheet(string characterName)
    {
        if (!CharacterSheets.ContainsKey(characterName))
        {
            CharacterSheets.Add(characterName, new CharacterSheet());
        }

        return CharacterSheets[characterName];
    }
}

public class CharacterSheet
{
    public CharacterSheet()
    {
        Initiative = 0;
    }

    public int Initiative
    {
        get;
        set;
    }
}
