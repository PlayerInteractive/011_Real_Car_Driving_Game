using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextType : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip ticksound;
    public Text objectiveText;

    private void OnEnable()
    {
        StartCoroutine(TextSteps(GetComponent<Text>().text));
    }

    // Update is called once per frame
 
    public IEnumerator TextSteps(string ObjectiveStatments)
    {
        objectiveText.text = "";
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.1f));
        //char[] myChars = ObjectiveStatments.ToCharArray();
        //		print (myChars.Length);
        foreach (char c in ObjectiveStatments)
        {
            //			Debug.Log (counter++);
            objectiveText.text += c;

            yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.1f));
            GetComponent<AudioSource>().PlayOneShot(ticksound);
            //				yield return new WaitForSeconds (0.01f);
        }
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(.5f));
        //transform.GetComponentInParent<Animator>().enabled = true;

    }


    public static class CoroutineUtil
    {
        public static IEnumerator WaitForRealSeconds(float time)
        {
            float start = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup < start + time)
            {
                yield return null;
            }
        }
    }

    private void OnDisable()
    {
        GetComponent<AudioSource>().Stop();
    }
}