using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

public class Entity {
    private Matrix _modelMatrix;
    // Warning: if setting the ModelMatrix to a sheer matrix, it will incorrectly calculate scale and rotation,
    // and the matrix will be set based on the calculated scale and rotation rather than to the assigned matrix
    public Matrix ModelMatrix {
        get => _modelMatrix;
        set {
            // Separate translation from rotation/scale
            Vector3 translation = value.Translation;
            Matrix rotScale = value * Matrix.CreateTranslation(-translation);

            // Transform the up vector according to rotScale
            Vector3 transformRight = Vector3.Transform(Vector3.Right, rotScale);
            Vector3 transformUp = Vector3.Transform(Vector3.Up, rotScale);

            // Get transformation fields
            _position = translation.XY();
            _size = new Vector2(transformRight.Length(), transformUp.Length());
            _angle = (float)Math.Acos(Vector3.Dot(transformUp, Vector3.Up) / _size.Y); 

            // Set the model matrix based on position, size, and angle
            // We set it indirectly to get rid of any sheering that may have been in the assigned matrix
            CalculateModelMatrix();
        }
    }
    private Vector2 _position;
    public Vector2 Position {
        get => _position;
        set {
            _position = value;
            CalculateModelMatrix();
        }
    }

    private Vector2 _size;
    public Vector2 Size {
        get => _size;
        set {
            _size = value;
            CalculateModelMatrix();
        }
    }


    private float _angle;
    public float Angle {
        get => _angle;
        set {
            _angle = value;
            CalculateModelMatrix();
        }
    }
    private List<Component> components;
    
    public Entity (
        Vector2 position,
        Vector2 size,
        float angle,
        List<Component> components = null
    ) {
        _position = position;
        _size = size;
        _angle = angle;
        CalculateModelMatrix();
        this.components = new List<Component>();
        AddComponents(components ?? new List<Component>());
    }

    private void CalculateModelMatrix()
    {
        _modelMatrix = Matrix.CreateRotationZ(_angle)
            * Matrix.CreateScale(new Vector3(_size, 1))
            * Matrix.CreateTranslation(new Vector3(_position, 0));
    }
    
    public void SetTransform(Matrix transform) {
        ModelMatrix = transform;
    }
    public void Transform(Matrix transform) {
        ModelMatrix *= transform;
    }
    public void Translate(Vector2 translation) {
        ModelMatrix *= Matrix.CreateTranslation(new Vector3(translation, 0));
    }
    public void Scale(Vector2 scale) {
        ModelMatrix *= Matrix.CreateScale(new Vector3(scale, 1));
    }
    public void Rotate(float rotation) {
        ModelMatrix *= Matrix.CreateRotationZ(rotation);
    }
    
    public void AddComponent (Component component) {
        components.Add(component);
        component.AttachTo(this);
    }
    public void AddComponents(List<Component> components) {
        components.ForEach(AddComponent);
    }
    
    public void Update(float deltaTime) {
        components.ForEach(comp => comp.Update(deltaTime));
    }
    public void Draw(float deltaTime) {
        components.ForEach(comp => comp.Draw(deltaTime));
    }

    internal Rectangle GetBoundingBox()
    {
        return new Rectangle(
            Position.ToPoint(),
            Size.ToPoint()
        );
    }
}
