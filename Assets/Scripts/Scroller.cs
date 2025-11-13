using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;

    Vector2 offest;
    Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offest += moveSpeed * Time.deltaTime;
        material.mainTextureOffset = offest;
    }
}
