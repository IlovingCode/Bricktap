using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AppController : MonoBehaviour
{
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] Transform hand;
    [SerializeField] float delay;

    void Awake()
    {
    }

    IEnumerator Start()
    {
        Block.minSpeed = minSpeed;
        Block.maxSpeed = maxSpeed;
        enabled = false;

        yield return new WaitForSeconds(delay);

        var timer = 0f;
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            hand.position = Vector3.Lerp(hand.position, Input.mousePosition, .02f);

            yield return null;  
        }

        enabled = true;
    }

    void Update()
    {
        hand.position = Vector3.Lerp(hand.position, Input.mousePosition, .1f);
        if (Input.GetMouseButtonDown(0))
        {
            hand.DOScale(Vector3.one * .9f, .2f);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                // Debug.Log(objectHit.GetChild(0).GetChild(0).up);
                var dir = objectHit.GetChild(0).GetChild(0).up;
                ray.direction = dir;

                var d = Block.getSize(objectHit.gameObject) * .5f;
                d.x *= dir.x;
                d.y *= dir.y;
                d.z *= dir.z;

                ray.origin = objectHit.position + d;
                objectHit.GetComponent<Block>().Move(dir, Physics.Raycast(ray, .5f));

                // Do something with the object that was hit by the raycast.
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            hand.DOScale(Vector3.one, .2f);
        }
    }
}
