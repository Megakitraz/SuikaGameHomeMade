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
    private float _cameraWidth;
    private float _limiteWidthToDrop;

    [SerializeField] private GameObject[] _prefabFruitsModel;
    
    [SerializeField] private GameObject[] _prefabFruits;
    [SerializeField] private int[] _pobabilityFruits;
    [SerializeField] private GameObject[] _nextFruitsModel;
    [SerializeField] private GameObject[] _nowFruitsModel;

    private int[] _selectedFruits;
    private int _totalProbabilityFruits;

    [SerializeField] public Transform _parentFruit;
    [SerializeField] private float _positionY;

    private GameObject _fruitToDropModel;

    private float _cooldownDrop;

    private bool _began;

    void Awake()
    {
        Instance = this;

        _width = (float)Screen.width / 2.0f;
        _height = (float)Screen.height / 2.0f;

        _cameraWidth = Camera.main.orthographicSize * _width / _height;

        _fruitToDropModel = null;

        



    }

    private void Start()
    {
        _cooldownDrop = GameManager.Instance.cooldownDrop;
        _began = false;

        Random.InitState(System.DateTime.Now.Second);

        _totalProbabilityFruits = 0;
        for (int i = 0; i < _pobabilityFruits.Length; i++)
        {
            _totalProbabilityFruits += _pobabilityFruits[i];
        }

        _selectedFruits = new int[2] { Random.Range(0, _totalProbabilityFruits), Random.Range(0, _totalProbabilityFruits) };
        for (int i = 0; i < _nextFruitsModel.Length; i++)
        {
            if (i == SelectedFruit(_selectedFruits[1])) _nextFruitsModel[i].SetActive(true);
            else _nextFruitsModel[i].SetActive(false);

            if (i == SelectedFruit(_selectedFruits[0])) _nowFruitsModel[i].SetActive(true);
            else _nowFruitsModel[i].SetActive(false);
        }
    }

    void Update()
    {

        _cooldownDrop += Time.deltaTime;
        //Debug.Log("_cooldownDrop" + _cooldownDrop);
        if (_cooldownDrop < GameManager.Instance.cooldownDrop) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved && _began)
            {

                Vector2 pos = touch.position;
                pos.x = ((pos.x - _width) / _width) * _cameraWidth;
                float limit = _rightBucket.position.x - _rightBucket.lossyScale.x - _prefabFruits[SelectedFruit(_selectedFruits[0])].transform.lossyScale.x/2f;
                if (Mathf.Abs(pos.x) > limit)
                {
                    if (pos.x > 0) pos.x = limit;
                    else pos.x = -limit;
                }


                if (_fruitToDropModel != null) _fruitToDropModel.transform.position = new Vector3(pos.x, _positionY, 0);

            }
            else if(touch.phase == TouchPhase.Began)
            {

                Vector2 pos = touch.position;
                pos.x = ((pos.x - _width) / _width) * _cameraWidth;
                float limit = _rightBucket.position.x - _rightBucket.lossyScale.x - _prefabFruits[SelectedFruit(_selectedFruits[0])].transform.lossyScale.x/2f;
                if (Mathf.Abs(pos.x) > limit)
                {
                    if (pos.x > 0) pos.x = limit;
                    else pos.x = -limit;
                }

                if (_fruitToDropModel == null) _fruitToDropModel = Instantiate(_prefabFruitsModel[SelectedFruit(_selectedFruits[0])], new Vector3(pos.x, _positionY, 0),Quaternion.identity,transform);
                _began = true;
            }
            else if(touch.phase == TouchPhase.Ended && _began)
            {

                Vector2 pos = touch.position;
                pos.x = ((pos.x - _width) / _width) * _cameraWidth;
                float limit = _rightBucket.position.x - _rightBucket.lossyScale.x - _prefabFruits[SelectedFruit(_selectedFruits[0])].transform.lossyScale.x/2f;
                if (Mathf.Abs(pos.x) > limit)
                {
                    if (pos.x > 0) pos.x = limit;
                    else pos.x = -limit;
                }

                if (_fruitToDropModel != null) {
                    _fruitToDropModel.transform.position = new Vector3(pos.x, 0, 0);
                    Destroy(_fruitToDropModel);
                    _fruitToDropModel = null;
                    Instantiate(_prefabFruits[SelectedFruit(_selectedFruits[0])], new Vector3(pos.x, _positionY, 0), Quaternion.identity, _parentFruit);
                }

                _cooldownDrop = 0;
                _began = false;
                NextFruit();
            }
        }
    }


    private void NextFruit()
    {
        _selectedFruits = new int[2] { _selectedFruits[1], Random.Range(0, _totalProbabilityFruits) };
        for (int i = 0; i < _nextFruitsModel.Length; i++)
        {
            if (i == SelectedFruit(_selectedFruits[1])) _nextFruitsModel[i].SetActive(true);
            else _nextFruitsModel[i].SetActive(false);

            if (i == SelectedFruit(_selectedFruits[0])) _nowFruitsModel[i].SetActive(true);
            else _nowFruitsModel[i].SetActive(false);
        }
        Debug.Log("_selectedFruits{ " + _selectedFruits[0] + " ; " + _selectedFruits[1] + " }");
    }

    private int SelectedFruit(int selection)
    {
        int totProb = 0;
        for (int i = 0; i < _pobabilityFruits.Length; i++)
        {
            totProb += _pobabilityFruits[i];
            if (selection < totProb) return i;
        }
        return 0;
    }
}
