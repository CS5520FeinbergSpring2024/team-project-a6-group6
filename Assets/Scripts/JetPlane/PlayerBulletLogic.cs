using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletLogic : MonoBehaviour, IBullet
{
    private int _damage = 50;

    [SerializeField]
    private float _speed = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y > 5f)
        {
            Destroy(gameObject, 1);
        }
    }

    public int GetDamage()
    {
        return _damage;
    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject;
        // If the other object is an Enemy, destroy the enemy then destroy this bullet instance.
        if (other.tag == "Enemy")
        {
            IExplosible enemy = null;
            switch (other.name)
            {
                case "EnemyEntryPrefab(Clone)":
                    {
                        enemy = otherObject.GetComponent<EnemyEntryLogic>();
                        break;
                    }
                default:
                    break;
            }
            if (enemy != null )
            {
                Debug.Log("EnemyEntryLogic found.");
                enemy.TakeDamage(_damage);
            }
            if (enemy is IExplosible)
            {
                Debug.Log("IExplosible instance caught.");
            }
            // Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }*/
}
