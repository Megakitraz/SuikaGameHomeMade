using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] protected GameObject _nextFruit;
    public bool _fusionMade;

    private void Start()
    {
        _fusionMade = false;
    }


    protected void FusionFruit(Collision2D collision, string tag)
    {
        if (collision.gameObject.tag == tag && !_fusionMade)
        {
            Debug.Log("Fusion: " + tag);
            if(_nextFruit != null) Instantiate(_nextFruit, collision.contacts[0].point, Quaternion.identity, InputManager.Instance._parentFruit);
            collision.gameObject.GetComponent<Fruit>()._fusionMade = true;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FusionFruit(collision, gameObject.tag);
    }

}
