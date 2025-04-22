using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Photo photo; //reference to the SO

    [Header("Dragging Settings")]
    //[SerializeField] private float dragSpeed = 1.0f;
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private float rotationAmount = 5.0f;
    [SerializeField] private AnimationCurve pickupCurve;
    [SerializeField] private AnimationCurve dropCurve;

    // Physics feel -> Add some juice
    private Vector3 offset;
    private Vector3 velocity;
    private Vector3 originalScale;
    private Quaternion originalRotation;
    private Vector3 targetPosition;

    public bool isDragging = false;
    public bool justDropped = false;
    private bool isCorrectlyPlaced = false;

    // References
    private SpriteRenderer spriteRenderer;
    private Camera mainCam;
    
    //bug fix -> only check draggable when finished being drraged


    private void Awake()
    {
        //get the child sprite componenet and assign it later with SO's photoSprite
        Transform child = transform.Find("photoImage");
        if (child != null) spriteRenderer = child.GetComponent<SpriteRenderer>();

        mainCam = Camera.main;
        
        if (photo != null && spriteRenderer != null)
            spriteRenderer.sprite = photo.photoSprite;

        originalScale = transform.localScale;
        originalRotation = transform.rotation;
    }

    public void OnMouseDown()
    {

        Debug.Log("Begin Dragging");

        if (isCorrectlyPlaced)
            return; // Prevent dragging if already correctly placed

        isDragging = true;

        Vector3 mouseWorld = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;
        offset = transform.position - mouseWorld;

        // Set target position to follow smoothly
        targetPosition = transform.position;

        float rotZ = Random.Range(-rotationAmount, rotationAmount);
        transform.rotation = originalRotation * Quaternion.Euler(0, 0, rotZ);

        StopAllCoroutines();
        StartCoroutine(AnimateScale(originalScale * 1.1f, pickupCurve));

        PhotoManager manager = FindObjectOfType<PhotoManager>();
        if (manager != null)
            manager.ShowHint(photo.hintText);


        /*
      // Notify manager of selection
      photoManager?.SelectPhoto(this, photo.hintText);

      // Apply a slight rotation for that tactile feel
      transform.rotation = originalRotation * Quaternion.Euler(0, 0, Random.Range(-rotationAmount, rotationAmount));


      // Play custom pickup sound if available, otherwise default
      if (photoData != null && photoData.customPickupSound != null)
          AudioManager.Instance?.PlayCustomSound(photoData.customPickupSound);
      else
          AudioManager.Instance?.PlayPickupSound();
      */
    }


    public void OnMouseDrag()
    {
        if (!isDragging || isCorrectlyPlaced) return;

        Vector3 mouseWorld = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;

        Vector3 newPos = mouseWorld + offset;

        // make sure it doesnt drag outside of the thing
        float minX = -7.7f;
        float maxX = 7.7f;
        float minY = -3.6f;
        float maxY = 3.6f;

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        transform.position = newPos;

    }

    public void OnMouseUp()
    {
        isDragging = false;
        justDropped = true;
        StartCoroutine(ClearJustDroppedFlag());

        StopAllCoroutines();
        StartCoroutine(AnimateScale(originalScale, dropCurve));

        // Return to original rotation
        transform.rotation = originalRotation;
     
        PhotoManager manager = FindObjectOfType<PhotoManager>();
        if (manager != null)
            manager.HideHint();
       
    }

    public int GetPhotoID() => photo != null ? photo.photoID : -1;

    private System.Collections.IEnumerator AnimateScale(Vector3 targetScale, AnimationCurve curve)
    {
        float time = 0f;
        float duration = 0.2f;
        Vector3 startScale = transform.localScale;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = curve.Evaluate(time / duration);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        transform.localScale = targetScale;
    }

    //check if the photo are correctly placed
    public bool IsCorrectlyPlaced()
    {
        return isCorrectlyPlaced;
    }
    public void SetCorrectlyPlaced(bool value)
    {
        isCorrectlyPlaced = value;
    }
    private IEnumerator ClearJustDroppedFlag()
    {
        yield return null; // wait 1 frame
        justDropped = false;
    }
}
