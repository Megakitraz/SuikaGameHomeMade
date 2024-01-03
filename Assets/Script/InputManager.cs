using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [SerializeField] private Transform _rightBucket;

    //private Vector3 _position;
    private float _width;
    private float _height;

    [SerializeField] private GameObject _prefabFruitOneModel;
    [SerializeField] private GameObject _prefabFruitOne;

    [SerializeField] public Transform _parentFruit;
    [SerializeField] private float _positionY;

    private GameObject _fruitToDropModel;

    void Awake()
    {
        Instance = this;

        _width = (float)Screen.width / 2.0f;
        _height = (float)Screen.height / 2.0f;

        _fruitToDropModel = null;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {

                Vector2 pos = touch.position;
                pos.x = ((pos.x - _width) / _width) * (_rightBucket.position.x - _prefabFruitOne.transform.lossyScale.x/2f);


                if (_fruitToDropModel != null) _fruitToDropModel.transform.position = new Vector3(pos.x, _positionY, 0);

            }
            else if(touch.phase == TouchPhase.Began)
            {

                Vector2 pos = touch.position;
                pos.x = ((pos.x - _width) / _width) * (_rightBucket.position.x - _prefabFruitOne.transform.lossyScale.x / 2f);

                if (_fruitToDropModel == null) _fruitToDropModel = Instantiate(_prefabFruitOneModel,new Vector3(pos.x, _positionY, 0),Quaternion.identity,transform);


            }
            else if(touch.phase == TouchPhase.Ended)
            {

                Vector2 pos = touch.position;
                pos.x = ((pos.x - _width) / _width) * (_rightBucket.position.x - _prefabFruitOne.transform.lossyScale.x / 2f);

                if (_fruitToDropModel != null) {
                    _fruitToDropModel.transform.position = new Vector3(pos.x, 0, 0);
                    Destroy(_fruitToDropModel);
                    _fruitToDropModel = null;
                    Instantiate(_prefabFruitOne, new Vector3(pos.x, _positionY, 0), Quaternion.identity, _parentFruit);
                }
            }
        }
    }
}
