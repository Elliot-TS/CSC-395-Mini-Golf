using Microsoft.Xna.Framework;

public class RigidBody : Component {
    public Vector2 Velocity;
    public float InvMass;
    private Vector2 acceleration;
    
    // Mass <= 0 means infinite mass
    public RigidBody( double mass ) {
        this.InvMass = mass <= 0 ? 0 : 1 / (float)mass;
    }
    
    public override void Update(float deltaTime) {
        float dt = deltaTime;
        Velocity += acceleration * dt;
        parent?.Translate(Velocity * dt);
        acceleration = Vector2.Zero;
    }
    public void ApplyForce(Vector2 force) {
        acceleration = force * InvMass;
    }

}
