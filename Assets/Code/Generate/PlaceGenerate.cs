using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlaceGenerate : MonoBehaviour
{
    [Header("Generate Place")]
    public GameObject placeObj;
    public List<GameObject> _places;
    private GameObject generalPlace;
    private GameObject leftPlace;
    private GameObject rightPlace;
    public float placeWidth;

    private GameObject _player;

    GameplayController _gameplayController;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _gameplayController = GameObject.Find("GameplayController").GetComponent<GameplayController>();
    }

    private void Update()
    {
        GeneratePlace();
    }

    void GeneratePlace()
    {
        float _distPlayerToPlace;
        float _currPlayerX;
        float _currGeneralPlaceX;

        if (_player.transform.position.x > 0) _currPlayerX = _player.transform.position.x;
        else _currPlayerX = -_player.transform.position.x;

        #region ���� ����������� ���������
        _distPlayerToPlace = 9999;

        foreach (GameObject obj in _places)
        {
            if (Vector3.Distance(_player.transform.position, obj.transform.position) < _distPlayerToPlace)
            {
                generalPlace = obj;
                _distPlayerToPlace = Vector3.Distance(_player.transform.position, obj.transform.position);

                if (rightPlace == obj) rightPlace = null;
                if (leftPlace == obj) leftPlace = null;
            }
        }

        foreach (GameObject obj in _places)
        {
            if (obj != generalPlace)
            {
                if (obj.transform.position.x > generalPlace.transform.position.x)
                    rightPlace = obj;
                else leftPlace = obj;
            }
        }
        #endregion

        #region ������� ������� ���������
        foreach (GameObject obj in _places)
        {
            float _currObjX;
            if (obj.transform.position.x > 0) _currObjX = obj.transform.position.x;
            else _currObjX = -obj.transform.position.x;

            float _result = _currPlayerX - _currObjX;
            if (_result < 0) _result *= -1;

            if (_result > 125)
            {
                _places.Remove(obj);
                if (rightPlace == obj) rightPlace = null;
                if (leftPlace == obj) leftPlace = null;
                Destroy(obj);
                return;
            }
        }
        #endregion

        #region ������������� ����� ���������        
        if (generalPlace.transform.position.x > 0) _currGeneralPlaceX = generalPlace.transform.position.x;
        else _currGeneralPlaceX = -generalPlace.transform.position.x;

        float _result2 = _currPlayerX - _currGeneralPlaceX;
        if (_result2 < 0) _result2 *= -1;

        if (_result2 > 45)
        {
            //������������� �����
            if (_player.transform.position.x < generalPlace.transform.position.x)
            {
                if (leftPlace == null)
                {
                    GameObject _newPlace = Instantiate(placeObj, new Vector3(generalPlace.transform.position.x - placeWidth, 0, 64), transform.rotation);
                    _places.Add(_newPlace);
                    leftPlace = _newPlace;

                    _newPlace.GetComponent<MeshRenderer>().material.mainTextureOffset = generalPlace.GetComponent<MeshRenderer>().material.mainTextureOffset;
                }
            }

            //������������� ������
            if (_player.transform.position.x > generalPlace.transform.position.x)
            {
                if (rightPlace == null)
                {
                    GameObject _newPlace = Instantiate(placeObj, new Vector3(generalPlace.transform.position.x + placeWidth, 0, 64), transform.rotation);
                    _places.Add(_newPlace);
                    rightPlace = _newPlace;

                    _newPlace.GetComponent<MeshRenderer>().material.mainTextureOffset = generalPlace.GetComponent<MeshRenderer>().material.mainTextureOffset;
                }
            }
        }
        #endregion
    }
}
