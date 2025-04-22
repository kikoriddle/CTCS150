using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Photo Collection", menuName = "CTCS150/Photo Collection")]
public class PhotoCollection : ScriptableObject
{
    [Header("Level Information")]
    public int levelNumber;
    public string levelName;

    [Header("Photos")]
    public Photo[] photos;
}
