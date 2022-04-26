using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScritp : MonoBehaviour
{
    public EnnemyAI lavraijaqueline;
   
	
	private Animator Jerare;
    bool chriss;
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

	
    void Start()
    {
		
        chriss = TryGetComponent(out Jerare);
		AssignAnimationIDs();
		
    }
        

    void Moove()
    {
        Jerare.SetFloat(_animIDSpeed,lavraijaqueline.statrun);

    }

   
    void Update()
    {
        
    }

	
}
