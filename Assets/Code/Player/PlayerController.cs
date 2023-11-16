using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public List<MeshRenderer> meshRenderer;

    PlayerUIController _playerUIController;
    PlayerStats _playerStats;
    PlayerPassiveController _playerPassiveController;

    public bool isDead;

    public List<GameObject> carsMesh;

    public bool isBrakeDamage;


    public void Initialize()
    {
        _playerUIController = GetComponentInChildren<PlayerUIController>();
        _playerStats = GetComponent<PlayerStats>();
        _playerPassiveController = GetComponent<PlayerPassiveController>();

        isDead = false;

        ChoiseCar();
    }

    void ChoiseCar()
    {
        foreach (GameObject gm in carsMesh)
        {
            if (gm.GetComponent<CarPlayerMesh>().carName == PlayerPrefs.GetString("selectedCarID"))
            {
                gm.GetComponent<CarPlayerMesh>().CarChoise();
            } 
            else
            {
                gm.GetComponent<CarPlayerMesh>().mesh.SetActive(false);
            }
        }
    }

    public void Hit(float _damage)
    {
        float _finalDamage;
        _finalDamage = _damage - (_damage / 100 * _playerStats.armorProcent);

        if (!isBrakeDamage)
        {
            _finalDamage = _finalDamage - _playerStats.block;
        } 
        else
        {
            _finalDamage = _finalDamage - _playerStats.iron;
        }        

        if (_playerPassiveController.isDodge)
        {
            int rand = Random.Range(1, 101);
            if (rand > _playerStats.dodgeProcent)
            {
                _playerStats.currentHp -= _finalDamage;

                StartCoroutine(GameObject.Find("GameplayUI").GetComponent<GameplayUIController>().PlayerHit());

                StartCoroutine(HitAnim());
            }
        } 
        else
        {
            _playerStats.currentHp -= _finalDamage;

            StartCoroutine(GameObject.Find("GameplayUI").GetComponent<GameplayUIController>().PlayerHit());

            StartCoroutine(HitAnim());            
        }

        if (_playerStats.currentHp < 0) _playerStats.currentHp = 0;

        isBrakeDamage = false;

        if (_playerStats.currentHp <= 0 && !isDead)
        {
            //Смерть
            if (!GameObject.Find("PopUp Recovery").GetComponent<PopUpRecovery>().isRecovery)
            {
                GameObject.Find("PopUp Recovery").GetComponent<PopUpRecovery>().ButOpen();
            }                
            else
            {
                GameObject.Find("GameplayController").GetComponent<WaveController>().StopGame();
                GameObject.Find("PopUp Win").GetComponent<PopUpWin>().ButOpen();
            }

            isDead = true;
        }
    }

    IEnumerator HitAnim()
    {
        foreach (MeshRenderer _mesh in meshRenderer)
        {
            _mesh.material.EnableKeyword("_EMISSION");
        }
        
        yield return new WaitForSeconds(0.1f);

        foreach (MeshRenderer _mesh in meshRenderer)
        {
            _mesh.material.DisableKeyword("_EMISSION");
        }
    }    
}
