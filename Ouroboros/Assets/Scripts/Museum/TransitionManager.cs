using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(SceneRoutine(sceneIndex));
    }

    public IEnumerator SceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        //launch the scene
        SceneManager.LoadScene(sceneIndex);
    }
}
