using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhotoManager : MonoBehaviour
{
    [Header("Level Data")]
    [SerializeField] private PhotoCollection currentLevel;

    [Header("Scene References")]
    [SerializeField] private Draggable[] draggablePhotos;  // Drag manually for each scene
    [SerializeField] private PhotoClip[] photoClips;       // Drag manually for each scene


    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI hintText;
    //[SerializeField] private GameObject levelCompletePanel;

    private void Start()
    {
        // Hide the hint text initially
        if (hintText != null)
            hintText.gameObject.SetActive(false);

        // if (levelCompletePanel != null)
        //   levelCompletePanel.SetActive(false);

    }

    private void Update()
    {
        // Optional: constantly check if all photos are placed
        if (AllPhotosPlacedCorrectly())
        {
            Debug.Log("LEVEL COMPLETE!");
           // if (levelCompletePanel != null)
                //levelCompletePanel.SetActive(true);
        }
    }

    private bool AllPhotosPlacedCorrectly()
    {
        foreach (Draggable photo in draggablePhotos)
        {
            if (!photo.IsCorrectlyPlaced())
                return false;
        }
        return true;
    }

    public void ShowHint(string text)
    {
        if (hintText != null)
        {
            hintText.text = text;
            hintText.gameObject.SetActive(true);
        }
    }

    public void HideHint()
    {
        if (hintText != null)
        {
            hintText.gameObject.SetActive(false);
        }
    }
}

