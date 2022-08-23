using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Spell : MonoBehaviour
{
    private Vector2 velocity;
    private Vector2 startPosition;

    private int _damage;
    private float _maxDistance;
    public void CreateBullet(Vector2 direction, float speed, float maxDistance)
    {
        velocity = direction * speed;
        _maxDistance = maxDistance;
        startPosition = transform.position;
    }

    public void Update()
    {
        if (Vector2.Distance(transform.position, startPosition) > _maxDistance)
            Destroy(gameObject);
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = position + velocity * Time.deltaTime;
        if (TryFindCollisionOnLine(position, newPosition))
            Destroy(gameObject);
        transform.position = newPosition;
    }

    public bool TryFindCollisionOnLine(Vector2 startPosition, Vector2 finishPosition)
    {
        RaycastHit2D[] hits = Physics2D.LinecastAll(startPosition, finishPosition);
        bool result = false;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.GetComponent<Player>())
                continue;
            if (hit.transform.GetComponent<IHitable>() != null)
            {
                IHitable enemy = hit.transform.GetComponent<IHitable>();
                enemy.GetDamage(_damage);
            }
            result = true;
        }

        return result;
    }


}
