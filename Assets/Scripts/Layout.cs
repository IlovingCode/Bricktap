using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Layout : MonoBehaviour
{
    [SerializeField] Vector3 size;

    Dictionary<string, Vector3> sizeMap = new Dictionary<string, Vector3>() {
        {"1x1", new Vector3(1, 1, 1)},
        {"1x2", new Vector3(2, 1, 1)},
        {"1x4", new Vector3(4, 1, 1)},
        {"2x2", new Vector3(2, 2, 1)},
    };
    // Start is called before the first frame update
    // void Start()
    // {

    // }

    Vector3 getSize(GameObject obj) => obj.transform.TransformDirection(sizeMap[obj.name]);

    void Align(Transform t, Transform next = null)
    {
        var x = size.x * -.5f;
        for (var i = 0; i < t.childCount; i++)
        {
            var child = t.GetChild(i);
            var s = getSize(child.gameObject);
            Debug.Log(s.y * .5f);
            child.localPosition = new Vector3(x + s.x * .5f, s.y * .5f, s.z * .5f);
            x += s.x;
        }
    }

    // Update is called once per frame
    void OnEnable()
    {
        var transComp = transform;
        for (var i = 0; i < transComp.childCount; i++)
        {
            Align(transComp.GetChild(i));
        }
    }
}
