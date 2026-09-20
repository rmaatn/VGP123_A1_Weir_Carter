using UnityEngine;

public class Marble : MonoBehaviour
{
   public int Style;
    private Animator anim;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        anim = GetComponent<Animator>();

        int numberOfStyles = anim.runtimeAnimatorController.animationClips.Length;

        if (Style <= 0 || Style > numberOfStyles) Style = Random.Range(1, numberOfStyles + 1);

        anim.Play(Style.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
