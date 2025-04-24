using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PhotoManager : MonoBehaviour
{
    [Header("Level Data")]
    [SerializeField] private PhotoCollection currentLevel;

    [Header("Scene References")]
    [SerializeField] private Draggable[] draggablePhotos;  // Drag manually for each scene
    [SerializeField] private PhotoClip[] photoClips;       // Drag manually for each scene


    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private AudioClip levelComplete;

    public GameObject levelCompletePanel;

    private void Start()
    {
        // Hide the hint text initially
        if (hintText != null)
            hintText.gameObject.SetActive(false);

        FindObjectOfType<TransitionController>().StartTransition();
        

        // if (levelCompletePanel != null)
        //   levelCompletePanel.SetActive(false);

    }

    private void Update()
    {
        // Optional: constantly check if all photos are placed
        if (AllPhotosPlacedCorrectly())
        {
            GameManager.Instance.PlaySFX(levelComplete);

            print(currentLevel.name);
            if (currentLevel.levelNumber <= 3)
            {
                int newNum = currentLevel.levelNumber + 1;
                string newLevel = "Level" + newNum.ToString();
                print(newLevel);
                levelCompletePanel.SetActive(true);
                FindObjectOfType<TransitionController>().EndTransition(newLevel);
            }
            else 
            {
                levelCompletePanel.SetActive(true);
                FindObjectOfType<TransitionController>().EndTransition("Credit");
            }
            

            // Debug.Log("LEVEL COMPLETE!");
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

