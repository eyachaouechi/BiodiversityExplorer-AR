using Polyperfect.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class InfoBehaviour : MonoBehaviour
{
    const float SPEED = 6f;

    [SerializeField]
    Transform SectionInfo;

    Vector3 desiredScale = Vector3.zero;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        SectionInfo.localScale = Vector3.Lerp(SectionInfo.localScale, desiredScale, Time.deltaTime * SPEED);
    }
    public void OpenInfo()
    {
        desiredScale = Vector3.one;
    }

    public void CloseInfo()
    {
        desiredScale = Vector3.zero;
    }

    public void PlaySound()
    {
        Common_AudioManager.Mute();
        audioSource.Play();

        float clipLength = audioSource.clip.length;
        StartCoroutine(StartMethod(clipLength));

    }


    private IEnumerator StartMethod(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        Common_AudioManager.UnMute();
    }

}