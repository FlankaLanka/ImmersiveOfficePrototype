using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Kino;

public class TweenCutscene : MonoBehaviour
{
    private DigitalGlitch glitch;
    private void Start() => glitch = FindObjectOfType<DigitalGlitch>();
    
    //part of cutscene, called in fungus flowchart
    public void StartIntensityIncrease() => StartCoroutine(IntensityIncrease());

    private IEnumerator IntensityIncrease()
    {
        float duration = 5f;
        float timer = 0f;
        while (duration > timer)
        {
            glitch.intensity = 0.4f + 0.6f * timer / duration; // original 0.4 + up to 0.6
            timer += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MainWorld");
    }

    private void Update()
    {
        //for quick skipping of intro
        if(Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainWorld");
    }
}
