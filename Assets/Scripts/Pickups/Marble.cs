using UnityEngine;

public class Marble : MonoBehaviour
{
    [SerializeField] private int Style;
    private Animator anim;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        anim = GetComponent<Animator>();

        if (Style <= 0 || Style > anim.runtimeAnimatorController.animationClips.Length) Style = Random.Range(1, anim.runtimeAnimatorController.animationClips.Length);

        anim.Play(Style.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
