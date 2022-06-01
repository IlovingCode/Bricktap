using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class StartTimeline : MonoBehaviour
{
    public PlayableDirector Timeline;
    public float delay = 1f;
    public AudioClip[] clips;

    public string TriggerKey = "s";
    private AudioSource audioSource;

    IEnumerator Start()
    {
        audioSource = GetComponent<AudioSource>();

        yield return new WaitForSeconds(delay);

        // audioSource.clip = clips[clips.Length - 1];
        Timeline.Play();

        while (enabled)
        {
            var timer = Random.Range(5f, 10f);
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }

            audioSource.clip = clips[Random.Range(0, clips.Length)];
            Timeline.Play();
        }
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.S) && Timeline != null)
    //         Timeline.Play();
    // }
}
