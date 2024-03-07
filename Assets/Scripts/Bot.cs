using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField][Range(1, 15)] private int _speed;

    private Base _parentBase;
    private Transform _currentTarget;

    public Transform CurrentTarget => _currentTarget;

    private void Update()
    {
        if(_currentTarget != null)
        {
            MoveTo();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Base baaaaase))
        {
            baaaaase.TakeGold();
            DestroyFirstChild();

            _currentTarget = null;
        }
    }

    public void SetParentBase(Base parentBase)
    {
        _parentBase = parentBase;
    }

    public void SetTarget(Transform target)
    {
        _currentTarget = target;
    }

    private void MoveTo()
    {
        Vector3 targetPosition = _currentTarget.position;
        targetPosition.y = transform.position.y;

        transform.LookAt(targetPosition);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

        CheckForNearbyGoldResources();
    }

    private void CheckForNearbyGoldResources()
    {
        if (_currentTarget.TryGetComponent(out Gold gold) == true)
        {
            float distance = Vector3.Distance(transform.position, _currentTarget.position);
            float pickupRadius = 2f;

            if (distance < pickupRadius)
            {
                PickUpGoldResource();
                SetTarget(_parentBase.transform);
            }
        }
    }

    private void PickUpGoldResource()
    {
        _currentTarget.SetParent(transform);
    }

    private void DestroyFirstChild()
    {
        if(transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        else
        {
            throw new System.Exception("No child objects to destroy.");
        }
    }
}
