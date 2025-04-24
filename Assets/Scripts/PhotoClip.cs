using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoClip : MonoBehaviour
{
    //the correct photoID
    [SerializeField] private int targetPhotoID;
    [SerializeField] private AudioClip cardComplete;

    private void OnTriggerStay2D(Collider2D other)
    {
        Draggable draggable = other.GetComponent<Draggable>();

        if (draggable != null && draggable.justDropped && !draggable.IsCorrectlyPlaced())
        {
            int droppedID = draggable.GetPhotoID();

            if (droppedID == targetPhotoID)
            {
                draggable.SetCorrectlyPlaced(true);

                // Snap to position
                draggable.transform.position = transform.position + new Vector3(0, -1.5f, 0);

                Debug.Log("Correctly placed photo ID: " + droppedID);

                GameManager.Instance.PlaySFX(cardComplete);

            }
            else
            {
                Debug.Log("Wrong photo. Expected ID: " + targetPhotoID + ", got: " + droppedID);
            }
        }
    }

    public int GetPhotoID() => targetPhotoID;

    public void SetTargetPhotoID(int id) => targetPhotoID = id;


}
