using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    public Material transitionMaterial; // Assign the material with your shader
    public float transitionDuration = 1.5f;

    public void EndTransition(string name)
    {
        StartCoroutine(AnimateTransition(name));
    }

    private IEnumerator AnimateTransition(string name)
    {
        float timer = 0f;

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float radius = Mathf.Lerp(1f, 0f, timer / transitionDuration);
            transitionMaterial.SetFloat("_Radius", radius);
            yield return null;
        }

        SceneManager.LoadScene(name);
    }

    public void StartTransition()
    {
        StartCoroutine(AnimateStart());
    }

    private IEnumerator AnimateStart()
    {
        float timer = 0f;

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float radius = Mathf.Lerp(0f, 1f, timer / transitionDuration);
            transitionMaterial.SetFloat("_Radius", radius);
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
