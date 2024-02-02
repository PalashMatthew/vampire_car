using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingShotGun : MonoBehaviour
{
    Gun _gunController;
    GameplayController _gameplayController;
    CapsuleCollider _capsuleCollider;

    public ParticleSystem vfxShot;
    public ParticleSystem vfxPrewarm;

    public List<GameObject> _enemy;
    public List<GameObject> _boss;

    float _saveDamage;
    

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        _gunController = GetComponent<Gun>();
        _gameplayController = GameObject.Find("GameplayController").GetComponent<GameplayController>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        _capsuleCollider.enabled = false;

        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        _saveDamage = _gunController.damage;

        yield return new WaitForSeconds(_gunController.shotSpeed - 0.5f);

        vfxPrewarm.Play();

        yield return new WaitForSeconds(0.5f);

        vfxShot.Play();
        _capsuleCollider.enabled = true;

        yield return new WaitForSeconds(0.1f);

        _capsuleCollider.enabled = false;

        DamageEnemy();

        yield return new WaitForSeconds(0.2f);

        vfxShot.Stop();
        _enemy.Clear();
        _boss.Clear();

        StartCoroutine(Attack());
    }

    public void DamageEnemy()
    {
        List<GameObject> _enemyAttack = new List<GameObject>();

        //ENEMY
        for (int i = 0; i < _enemy.Count; i++)
        {
            float _minZ = 1000;
            GameObject _currentEnemy = null;

            foreach (GameObject obj in _enemy)
            {
                if (obj != null)
                {
                    if (obj.transform.position.z < _minZ && !_enemyAttack.Contains(obj))
                    {
                        _currentEnemy = obj;
                        _minZ = obj.transform.position.z;
                    }
                }                
            }

            _enemyAttack.Add(_currentEnemy);
        }

        for (int i = 0; i < _enemyAttack.Count; i++)
        {
            if (i > 0)
            {
                _gunController.damage *= _gunController.multiplyDamage;
            }

            if (_enemyAttack[i] != null)
                _gunController.DamageEnemy(_enemyAttack[i], gameObject);
        }

        //BOSS
        List<GameObject> _bossAttack = new List<GameObject>();

        for (int i = 0; i < _boss.Count; i++)
        {
            float _minZ = 1000;
            GameObject _currentEnemy = null;

            foreach (GameObject obj in _boss)
            {
                if (obj != null)
                {
                    if (obj.transform.position.z < _minZ && !_bossAttack.Contains(obj))
                    {
                        _currentEnemy = obj;
                        _minZ = obj.transform.position.z;
                    }
                }
            }

            _bossAttack.Add(_currentEnemy);
        }

        for (int i = 0; i < _bossAttack.Count; i++)
        {
            if (i > 0)
            {
                _gunController.damage *= _gunController.multiplyDamage;
            }

            if (_bossAttack[i] != null)
                _gunController.DamageBoss(_bossAttack[i], gameObject);
        }

        _gunController.damage = _saveDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            if (!_enemy.Contains(other.gameObject))
                _enemy.Add(other.gameObject);
        }

        if (other.tag == "boss")
        {
            if (!_boss.Contains(other.gameObject))
                _boss.Add(other.gameObject);
        }
    }
}
