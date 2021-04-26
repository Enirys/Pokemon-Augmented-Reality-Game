using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ImageTargetDetection : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;

    void Start()
    {

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            if(GameManager.instance.pokemon1 == null)
            {
                GameManager.instance.pokemon1 = this.gameObject.GetComponentInChildren<Pokemon>();
            } else if(GameManager.instance.pokemon2 == null)
            {
                GameManager.instance.pokemon2 = this.gameObject.GetComponentInChildren<Pokemon>();
            }
            
        }
    }
}
