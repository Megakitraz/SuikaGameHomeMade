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
        StartCoroutine(IsHigherThanLimit());
    }


    protected void FusionFruit(Collision2D collision, string tag)
    {
        if (collision.gameObject.tag == tag && !_fusionMade)
        {
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

    IEnumerator IsHigherThanLimit()
    {
        float timeOutOfLimit = 0;
        while (true)
        {
            if (transform.position.y > GameManager.Instance.limit) timeOutOfLimit += Time.deltaTime;
            else if(transform.position.y < -10) Destroy(gameObject);
            else timeOutOfLimit = 0;

            if (timeOutOfLimit > GameManager.Instance.cooldownFinnish) GameManager.Instance.GameOver();





            yield return new WaitForEndOfFrame();
        }
    }

}
