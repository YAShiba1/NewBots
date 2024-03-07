using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private Queue<Transform> _goldPosition;

    private void Start()
    {
        _goldPosition = new Queue<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Gold gold))
        {
            _goldPosition.Enqueue(gold.transform);
        }
    }

    public Transform TryGetNextGoldPosition()
    {
        if(_goldPosition.Count > 0)
        {
            return _goldPosition.Dequeue();
        }

        return null;
    }
}
