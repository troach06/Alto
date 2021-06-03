using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimator : MonoBehaviour {
    public AudioSource offSound;
	// Use this for initialization
	void Start () {
        ScaleUp();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ScaleUp()
    {
        iTween.ScaleBy(gameObject, new Vector3(2, 2, 2), 1);
    }
    public void ScaleDown()
    {
        StartCoroutine(Disable());
        if (offSound != null)
        {
            offSound.Play();
        }
        iTween.ScaleTo(gameObject, new Vector3(0f, 0f, 0f), 1);
    }
    public void ScaleKeyboardDown()
    {
        StartCoroutine(Disable());
        if (offSound != null)
        {
            offSound.Play();
        }
        iTween.ScaleTo(gameObject, new Vector3(0.000375f, 0.000375f, 0.000375f), 1);
    }
    IEnumerator Disable() {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
}
}
