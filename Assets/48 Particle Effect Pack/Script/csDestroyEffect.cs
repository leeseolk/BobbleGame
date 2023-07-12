using UnityEngine;

public class csDestroyEffect : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
        {
            Destroy(gameObject);
        }
    }
}
