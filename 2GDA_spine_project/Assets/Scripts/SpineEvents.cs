using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineEvents : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string eventName;

    public AudioSource audioSource;
    public AudioClip audioClip;

    Spine.EventData eventData;

    void Awake() {
        //skeletonAnimation = GetComponent<SkeletonAnimation>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //eventData = skeletonAnimation.Skeleton.Data.FindEvent(eventName);
        skeletonAnimation.AnimationState.Event += HandleAnimationStateEvent;
    }

    private void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e) {
        if("footstep" == e.Data.Name) {
            // Play stuff
            Play();
        }
    }

    public void Play(){
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
