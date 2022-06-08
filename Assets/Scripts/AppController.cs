using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AppController : MonoBehaviour
{
    public static bool IsGameEnd = false;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] Transform hand;
    [SerializeField] Transform X;
    [SerializeField] float delay;
    [SerializeField] float clickDelay;
    [SerializeField] float handMoveDuration;
    [SerializeField] Transform[] hitBlocks;
    [SerializeField] Vector3[] offsets;
    [SerializeField] AudioSource _falseAudioSource;

    IEnumerator Start()
    {
        Block.minSpeed = minSpeed;
        Block.maxSpeed = maxSpeed;
        enabled = false;
        var scale = X.localScale;
        X.localScale = Vector3.zero;

        yield return new WaitForSeconds(delay);

        // hand.DOMove()

        // var timer = 0f;
        // while (timer < 2f)
        // {
        //     timer += Time.deltaTime;
        //     hand.position = Vector3.Lerp(hand.position, Input.mousePosition, .02f);

        //     yield return null;
        // }

        var wait = new WaitForSeconds(clickDelay);
        var camera = GetComponentInChildren<Camera>();
        var ray = new Ray();
        var count = 0;

        yield return hand.DOMove(camera.WorldToScreenPoint(
            hitBlocks[0].position + offsets[0]), handMoveDuration + clickDelay).WaitForCompletion();
        foreach (var block in hitBlocks)
        {
            Debug.Log(count.ToString() + offsets[count]);
            yield return hand.DOMove(camera.WorldToScreenPoint(block.position + offsets[count]), handMoveDuration).WaitForCompletion();
            count++;

            yield return hand.DOScale(Vector3.one * .9f, .2f).WaitForCompletion();

            var dir = block.GetChild(0).GetChild(0).up;
            ray.direction = dir;

            var d = Block.getSize(block.gameObject) * .5f;
            d.x *= dir.x;
            d.y *= dir.y;
            d.z *= dir.z;

            ray.origin = block.position + d;
            block.GetComponent<Block>().Move(dir, Physics.Raycast(ray, .5f));

            hand.DOScale(Vector3.one, .2f);
            yield return wait;
        }
        AppController.IsGameEnd = true;
        // enabled = true;

        // enabled = false;
        X.DOScale(scale, .5f);
        _falseAudioSource.Play();
    }

    float timer = 0f;
    void Update()
    {
        this.timer += Time.deltaTime;
        if(this.timer > 27f)
        {
            AppController.IsGameEnd = true;
        }
    }

    // void Update()
    // {
    //     hand.position = Vector3.Lerp(hand.position, Input.mousePosition, .1f);
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         hand.DOScale(Vector3.one * .9f, .2f);
    //         RaycastHit hit;
    //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //         if (Physics.Raycast(ray, out hit))
    //         {
    //             Transform objectHit = hit.transform;
    //             // Debug.Log(objectHit.GetChild(0).GetChild(0).up);
    //             var dir = objectHit.GetChild(0).GetChild(0).up;
    //             ray.direction = dir;

    //             var d = Block.getSize(objectHit.gameObject) * .5f;
    //             d.x *= dir.x;
    //             d.y *= dir.y;
    //             d.z *= dir.z;

    //             ray.origin = objectHit.position + d;
    //             objectHit.GetComponent<Block>().Move(dir, Physics.Raycast(ray, .5f));

    //             // Do something with the object that was hit by the raycast.
    //         }
    //     }

    //     if (Input.GetMouseButtonUp(0))
    //     {
    //         hand.DOScale(Vector3.one, .2f);
    //     }
    // }
}
