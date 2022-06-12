using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AppController : MonoBehaviour
{
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] Transform hand;
    [SerializeField] Transform X;
    [SerializeField] float delay;
    [SerializeField] float xDelay;
    [SerializeField] float clickDelay;
    [SerializeField] float handMoveDuration;
    [SerializeField] float handClickDuration;
    [SerializeField] Transform[] hitBlocks;
    [SerializeField] Vector3[] offsets;
    // [SerializeField] AudioSource _falseAudioSource;

    IEnumerator Start()
    {
        Application.targetFrameRate = 60;
        Block.minSpeed = minSpeed;
        Block.maxSpeed = maxSpeed;
        enabled = false;
        var scale = X.localScale;
        X.localScale = Vector3.zero;
        var timer = Time.time;

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
        // var ray = new Ray();
        var count = 0;
        var handFirstMoveDuration = handMoveDuration + clickDelay;

        foreach (var block in hitBlocks)
        {
            // Debug.Log(count.ToString() + offsets[count]);
            yield return hand.DOMove(camera.WorldToScreenPoint(block.position + offsets[count]), handMoveDuration + handFirstMoveDuration)
                .SetEase(Ease.OutQuad).WaitForCompletion();
            count++;
            handFirstMoveDuration = 0f;

            yield return hand.DOScale(Vector3.one * .9f, handClickDuration).WaitForCompletion();

            var dir = block.GetChild(0).GetChild(0).up;
            // ray.direction = dir;

            var d = Block.getSize(block.gameObject) * .5f + new Vector3(.3f, .3f, .3f);
            d.x *= dir.x;
            d.y *= dir.y;
            d.z *= dir.z;

            // ray.origin = block.position + d;
            block.GetComponent<Block>().Move(dir, Physics.CheckSphere(block.position + d, .3f));

            hand.DOScale(Vector3.one, .2f);
            yield return wait;
        }

        // enabled = true;

        // enabled = false;
        yield return new WaitForSeconds(xDelay);
        X.DOScale(scale, .5f).SetEase(Ease.InQuad);
        // _falseAudioSource.Play();
        Debug.Log(Time.time - timer);
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
