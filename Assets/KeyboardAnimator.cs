using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardAnimator : MonoBehaviour {

    public AudioSource offSound;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ScaleUp()
    {
        iTween.ScaleTo(gameObject, new Vector3(0.00085f, 0.00085f, 0.00085f), 1);
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
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
