using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Photo", menuName = "CTCS150/Photo")]
public class Photo : ScriptableObject
{
    //this is where photo ends up to be in the end
    public int photoID;

    [Header("Visual")]
    public Sprite photoSprite;

    public string hintText;

    [Header("Level Information")]
    public int levelNumber; // Which level this photo appears in
}
