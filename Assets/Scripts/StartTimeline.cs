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

    static int audioId = 0;

    IEnumerator Start()
    {
        audioSource = GetComponent<AudioSource>();

        yield return new WaitForSeconds(delay);

        audioSource.clip = clips[0];
        audioSource.time = clips[0].length * .55f; // + Random.Range(.2f, .3f);
        audioSource.volume = .7f;
        Timeline.Play();

        while (enabled)
        {
            var timer = Random.Range(5f, 13f);
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }

            {
                audioId += Random.Range(1, clips.Length / 2);
                audioId %= clips.Length;
                if (audioId == 0) audioId++;
            }

            audioSource.volume = 1f;
            audioSource.clip = clips[audioId];
            audioSource.time = 0f;
            Timeline.Play();
        }
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.S) && Timeline != null)
    //         Timeline.Play();
    // }
}
