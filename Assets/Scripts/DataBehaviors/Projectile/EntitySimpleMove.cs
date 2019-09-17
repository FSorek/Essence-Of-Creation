using UnityEngine;

public class EntitySimpleMove : IEntitySimpleMove
{
    private IEntity entity;
    public EntitySimpleMove(IEntity entity)
    {
        this.entity = entity;
    }

    public void Move(Vector3 position, float speed)
    {
        entity.Position = (position - entity.Position).normalized * GameTime.deltaTime * speed;
    }
}
