using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Letter", menuName = "GMTK/Letter")]
public class LetterData : ScriptableObject
{
    [SerializeField] int giftId;
    public List<string> contentList = new List<string>();

    public int GetID() { return giftId; }
}
