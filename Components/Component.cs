public abstract class Component {
    protected Entity parent; 

    public virtual void Update(float deltaTime) { }
    public virtual void Draw(float deltaTime) {}
    
    public virtual void AttachTo(Entity parent) {
        this.parent = parent;
    }
}