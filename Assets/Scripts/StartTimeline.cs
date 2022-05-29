using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class StartTimeline : MonoBehaviour
{
    public PlayableDirector Timeline;
    public float delay = 1f;

    public string TriggerKey = "s";

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);

        Timeline.Play();

        while (enabled)
        {
            var timer = Random.Range(5f, 10f);
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }

            Timeline.Play();
        }
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.S) && Timeline != null)
    //         Timeline.Play();
    // }
}
