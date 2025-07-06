using UnityEngine;

public class Goal : MonoBehaviour
{
    public static bool goalMet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            goalMet = true;

            var mat = GetComponent<Renderer>().material;
            var c = mat.color;
            c.a = 1;
            mat.color = c;
        }
    }
}