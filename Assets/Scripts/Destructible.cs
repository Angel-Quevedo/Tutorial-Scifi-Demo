using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] GameObject _crateDestroyed;

    public void DestroyCrate()
    {
        Instantiate(_crateDestroyed, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
