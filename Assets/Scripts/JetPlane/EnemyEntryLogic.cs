using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyEntryLogic : MonoBehaviour, IExplosible
{
    private float _xStart;
    private float _xPosition;
    private float _yPosition;
    private float _xLeftBound;
    private float _xRightBound;
    private float _yLowerBound = 2.5f;
    private Vector3 _spawnPosition;
    private int _direction = 1;
    private int _hitPoint = 50;
    private int _score = 50;
    private int _damage = 50;
    private float _fireInterval = 3.0f;
    private int _burstCount = 1;
    private bool _canFire = true;
    private GameObject _containerTypeEnemyBullet;
    private float _timeToRush;
    private bool _isTimeToRush = false;

    [SerializeField]
    private float _boardLeftBorder = -2.5f;
    [SerializeField]
    private float _boardRightBorder = 2.5f;
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private GameObject _enemyEntryBullet;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovement();
    }

    void Spawn()
    {
        _xStart = transform.position.x;
        _xLeftBound = Math.Max(_boardLeftBorder, _xStart - 1);
        _xRightBound = Math.Min(_boardRightBorder, _xStart + 1);

        _xPosition = transform.position.x;
        _spawnPosition = new Vector3(_xPosition, 4, 0);
        transform.position = _spawnPosition;

        _containerTypeEnemyBullet = GameObject.Find("EnemyBulletContainer");
        if (_containerTypeEnemyBullet == null)
        {
            Debug.LogWarning("Container for enemy bullets is null.");
        }

        _timeToRush = UnityEngine.Random.Range(10f, 20f);
    }

    IEnumerator DirectionDecision()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        _xPosition = transform.position.x;
        if (_xPosition < _xLeftBound)
        {
            _direction = 1;
        }
        else if (_xPosition > _xRightBound)
        {
            _direction = -1;
        }
    }

    IEnumerator BulletTimer()
    {
        yield return new WaitForSeconds(_fireInterval);
        _canFire = true;
    }

    IEnumerator RushToBottom()
    {
        yield return new WaitForSeconds(_timeToRush);
        _isTimeToRush = true;
    }

    void ControlMovement()
    {
        _xPosition = transform.position.x;
        _yPosition = transform.position.y;

        StartCoroutine(DirectionDecision());

        if (_yPosition > _yLowerBound)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        } else
        {
            StartCoroutine(RushToBottom());
            if (_isTimeToRush)
            {
                transform.Translate(Vector3.down * _speed * 2 * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.right * _speed * _direction * Time.deltaTime);
            }
            if (_canFire)
            {
                _canFire = false;
                GameObject bullet = Instantiate(_enemyEntryBullet, transform.position + new Vector3(0f, -0.25f, 0f), Quaternion.identity);
                if (_containerTypeEnemyBullet != null)
                {
                    bullet.transform.parent = _containerTypeEnemyBullet.transform;
                }
                StartCoroutine(BulletTimer());
            }
        }
        // Debug.Log(string.Format("Updating EnemyEntry intended as: x {0}, y {1}, direction {2}", _xPosition, _yPosition, _direction));
    }

    public int GetDamage()
    {
        return _damage;
    }

    public int GetHealth()
    {
        return _hitPoint;
    }

    public int GetScore()
    {
        return _score;
    }

    public void TakeDamage(int damageTaken)
    {
        _hitPoint -= damageTaken;
        Debug.Log(string.Format("Enemy Entry has {0} hit point left.", _hitPoint));
        if (_hitPoint < 1)
        {
            Destroy(this.gameObject);
        }
    }

    //Note: All collide calculation is done on the side which can take damage (aka active objects). If both sides are able to take damage, the logic is on the enemy side.
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject;
        // If the other object is the player, deduct the hit point of the player then destroy this bullet instance.
        if (other.tag == "Player")
        {
            IExplosible player = null;
            PlayerBulletLogic bullet = null;
            int selfDamage = 0;
            switch (other.name)
            {
                case "JetPlayer":
                    {
                        player = otherObject.GetComponent<JetPlayerController>();
                        break;
                    }
                case "PlayerBulletPrefab(Clone)":
                    {
                        bullet = otherObject.GetComponent<PlayerBulletLogic>();
                        break;
                    }
                default:
                    break;
            }
            if (player != null)
            {
                Debug.Log("JetPlayerController found.");
                selfDamage = player.GetDamage();
                player.TakeDamage(_damage);
                TakeDamage(selfDamage);
            } else if (bullet != null)
            {
                selfDamage = bullet.GetDamage();
                Destroy(otherObject);
                TakeDamage(selfDamage);
            }
        }
    }
}
