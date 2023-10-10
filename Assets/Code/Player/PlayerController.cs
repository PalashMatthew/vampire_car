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

    public bool isDead;

    [Header("Level Up")]
    public ParticleSystem fxLevelUp;
    public TMP_Text tLevelUp;    


    public void Initialize()
    {
        _playerUIController = GetComponentInChildren<PlayerUIController>();
        _playerStats = GetComponent<PlayerStats>();

        isDead = false;
        tLevelUp.gameObject.SetActive(false);
    }

    public void Hit(float _damage)
    {
        _playerStats.currentHp -= _damage;

        StartCoroutine(GameObject.Find("GameplayUI").GetComponent<GameplayUIController>().PlayerHit());
        
        StartCoroutine(HitAnim());

        if (_playerStats.currentHp <= 0 && !isDead)
        {
            //Смерть
            GameObject.Find("PopUp Dead").GetComponent<PopUpDead>().ButOpen();
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

    public void LevelUp()
    {
        fxLevelUp.Play();

        StartCoroutine(LevelUpTextAnimation());
    }    

    IEnumerator LevelUpTextAnimation()
    {
        tLevelUp.gameObject.SetActive(true);
        tLevelUp.transform.DOScale(0, 0);

        tLevelUp.transform.DOScale(1, 0.3f).SetEase(Ease.OutBack);

        yield return new WaitForSeconds(2);

        tLevelUp.transform.DOScale(0, 0.3f).SetEase(Ease.InBack);

        yield return new WaitForSeconds(0.3f);
        tLevelUp.gameObject.SetActive(false);

        GameObject.Find("PopUp Upgrade").GetComponent<PopUpUpgrade>().ButOpen();
    }
}
