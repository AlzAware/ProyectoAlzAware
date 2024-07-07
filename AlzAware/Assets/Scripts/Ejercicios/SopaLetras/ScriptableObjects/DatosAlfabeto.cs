using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class DatosAlfabeto : ScriptableObject
{
    [System.Serializable]
    public class LetterData
    {
        public string letter;
        public Sprite image;

    }
    public List<LetterData> AlphabetNormal = new List<LetterData>();
    public List<LetterData> AlphabetGreen = new List<LetterData>();
    public List<LetterData> AlphabetRed = new List<LetterData>();
}
