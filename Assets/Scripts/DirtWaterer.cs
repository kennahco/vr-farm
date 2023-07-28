using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtWaterer : MonoBehaviour
{
    public ParticleSystem part;
    public Renderer renderer;
    public List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    public int size = 1;
    public Color[] paintedColor;

    Texture2D texture;

    private void Start()
    {
        texture = Instantiate(renderer.material.mainTexture) as Texture2D;
        renderer.material.mainTexture = texture;
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(this.gameObject, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            RaycastHit hitinfo;
            Physics.Raycast(collisionEvents[i].intersection, Vector3.down, out hitinfo);
            texture.SetPixels((int)hitinfo.textureCoord.x, (int)hitinfo.textureCoord.y, size, size, paintedColor, 1);
            texture.Apply(false);
        }
    }
}
