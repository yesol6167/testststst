using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class HostPerception : MonoBehaviour
{
    public UnityEvent<Transform> FindHost = default;
    public UnityEvent LostHost = default;
    public LayerMask HostMask = default;
    public Transform myTarget = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (myTarget != null) return;
        if ((HostMask & 1 << other.gameObject.layer) != 0)
        {
            //타겟을 처음 발견했을때
            myTarget = other.transform;
            FindHost?.Invoke(myTarget);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (myTarget == other.transform)
        {
            myTarget = null;
            LostHost?.Invoke();
        }
    }
}
