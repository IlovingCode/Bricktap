using UnityEngine;
using UnityEngine.Playables;

public class StartTimeline : MonoBehaviour
{
    public PlayableDirector Timeline;

    public string TriggerKey = "s";

    void Update()
    {
        if (Input.GetKeyDown(TriggerKey) && Timeline != null)
            Timeline.Play();
    }
}
