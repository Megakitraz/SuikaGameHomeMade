using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private Vector3 _position;
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

        // Position used for the cube.
        _position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void OnGUI()
    {
        // Compute a fontSize based on the size of the screen width.
        GUI.skin.label.fontSize = (int)(Screen.width / 25.0f);

        GUI.Label(new Rect(20, 20, _width, _height * 0.25f),
            "x = " + _position.x.ToString("f2") +
            ", y = " + _position.y.ToString("f2"));
    }
    void Update()
    {

        //Debug.Log("_fruitToDropModel: " + (_fruitToDropModel != null));
        // Handle screen touches.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                //Debug.Log("Touch Moved");

                Vector2 pos = touch.position;
                pos.x = (pos.x - _width) / _width;
                pos.y = (pos.y - _height) / _height;


                if (_fruitToDropModel != null) _fruitToDropModel.transform.position = new Vector3(pos.x, _positionY, 0);

                _position = new Vector3(-pos.x, pos.y, 0.0f);

            }
            else if(touch.phase == TouchPhase.Began)
            {
                //Debug.Log("Touch Began");

                Vector2 pos = touch.position;
                pos.x = (pos.x - _width) / _width;
                pos.y = (pos.y - _height) / _height;

                if (_fruitToDropModel == null) _fruitToDropModel = Instantiate(_prefabFruitOneModel,new Vector3(pos.x, _positionY, 0),Quaternion.identity,transform);

                _position = new Vector3(-pos.x, pos.y, 0.0f);

            }
            else if(touch.phase == TouchPhase.Ended)
            {
                //Debug.Log("Touch Ended");

                Vector2 pos = touch.position;
                pos.x = (pos.x - _width) / _width;
                pos.y = (pos.y - _height) / _height;

                if (_fruitToDropModel != null) {
                    _fruitToDropModel.transform.position = new Vector3(pos.x, 0, 0);
                    Destroy(_fruitToDropModel);
                    _fruitToDropModel = null;
                    Instantiate(_prefabFruitOne, new Vector3(pos.x, _positionY, 0), Quaternion.identity, _parentFruit);
                }

                _position = new Vector3(-pos.x, pos.y, 0.0f);
            }
        }
    }
}
