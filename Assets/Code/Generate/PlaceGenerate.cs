using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float minZ;
    public float spawnZ;

    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        //GeneratePlace();

        GameObject remove = null;
        GameObject _inst = null;

        foreach (GameObject place in _places)
        {
            if (place.transform.position.z <= minZ)
            {
                _inst = Instantiate(placeObj, new Vector3(0, 0, spawnZ), transform.rotation);
                _inst.transform.parent = transform;
                remove = place;                
            }
        }        

        if (remove != null)
        {
            _places.Remove(remove);
            Destroy(remove);
            remove = null;
        }

        if (_inst != null)
        {
            _places.Add(_inst);            
            _inst.GetComponent<RoadController>().dist = spawnZ;
            _inst.GetComponent<RoadController>().parent = _places[_places.Count-2];
            _inst = null;
        }
    }

    public void StopGame()
    {
        foreach (GameObject gm in _places)
        {
            gm.GetComponent<InstObjectMove>().moveSpeed = 0;
        }
    }

    void GeneratePlace()
    {
        float _distPlayerToPlace;
        float _currPlayerX;
        float _currGeneralPlaceX;

        if (_player.transform.position.x > 0) _currPlayerX = _player.transform.position.x;
        else _currPlayerX = -_player.transform.position.x;

        #region Ищем центральную плоскость
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

        #region Удаляем дальнюю плоскость
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

        #region Устанавливаем новую плоскость        
        if (generalPlace.transform.position.x > 0) _currGeneralPlaceX = generalPlace.transform.position.x;
        else _currGeneralPlaceX = -generalPlace.transform.position.x;

        float _result2 = _currPlayerX - _currGeneralPlaceX;
        if (_result2 < 0) _result2 *= -1;

        if (_result2 > 45)
        {
            //Устанавливаем слева
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

            //Устанавливаем справа
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
