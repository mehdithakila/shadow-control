using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Mirror;
using Cinemachine;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	[RequireComponent(typeof(PlayerInput))]
#endif
	public class multi_mouvement : NetworkBehaviour
	{
		[Header("Player")]
		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 2.0f;
		[Tooltip("Sprint speed of the character in m/s")]
		public float SprintSpeed = 5.335f;
		[Tooltip("How fast the character turns to face movement direction")]
		[Range(0.0f, 0.3f)]
		public float RotationSmoothTime = 0.12f;
		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;

		public bool SaqrTi1BATAR = true;
		public bool IliesTi1BATAR = true;

		[Space(10)]
		[Tooltip("The height the player can jump")]
		public float JumpHeight = 1.2f;
		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		public float Gravity = -15.0f;

		[Space(10)]
		[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
		public float JumpTimeout = 0.50f;
		[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
		public float FallTimeout = 0.15f;

		[Header("Player Grounded")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;
		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;
		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.28f;
		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;
		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 70.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -30.0f;
		[Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
		public float CameraAngleOverride = 0.0f;
		[Tooltip("For locking the camera position on all axis")]
		public bool LockCameraPosition = false;

		[Header("Attaque")]
		public bool attacked = false;
		public bool waitingForInput = false;
		public bool gotInput = false;
		public int combo = 0;
		public GameObject swordMesh;
		public swordMC sword;
		public GameObject footMesh;
		public swordMC foot;

		[Header("Skin")]
		public List<GameObject> everyCloathe;
		public List<GameObject> shinobiCloathes;
		public List<GameObject> GSCloathes;
		public List<GameObject> BrwCloathes;
		public bool IsShi;
		public bool IsGS;
		public bool IsBrw;
		public NetworkAnimator hamid;




		// cinemachine
		private float _cinemachineTargetYaw;
		private float _cinemachineTargetPitch;

		// player
		private float _speed;
		private float _animationBlend;
		private float _targetRotation = 0.0f;
		private float _rotationVelocity;
		private float _verticalVelocity;
		private float _terminalVelocity = 53.0f;

		// timeout deltatime
		private float _jumpTimeoutDelta;
		private float _fallTimeoutDelta;

		// animation IDs
		private int _animIDSpeed;
		private int _animIDGrounded;
		private int _animIDJump;
		private int _animIDFreeFall;
		private int _animIDMotionSpeed;
		private int _animIDDeath;
		private int _animIDAttack;
		private int _animIDAttack2;
		private int _animIDAttack3;
		private int _animIDAttack4;
		private int _animIDAttack5;
		private int _animIDIsShi;
		private int _animIDIsGS;
		private int _animIDIsBrw;
		private int _animIDChangeStance;


		private Animator _animator;
		private CharacterController _controller;
		private StarterAssetsInputs _input;
		private GameObject _mainCamera;
		private LifeBar life;
		public bool isAttacking = false;
		public bool Died = false;

		private const float _threshold = 0.01f;

		private bool _hasAnimator;

		private void Awake()
		{
			// get a reference to our main camera
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
		}

		public override void OnStartLocalPlayer()
		{
			GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().Follow = transform.GetChild(0).transform;
		}
		public override void OnStartAuthority()
		{
			base.OnStartAuthority();
			PlayerInput playerInput = GetComponent<PlayerInput>();
			playerInput.enabled = true;
			CharacterController character = GetComponent<CharacterController>();
			character.enabled = true;
			ThirdPersonController thirdPersonController = GetComponent<ThirdPersonController>();
			thirdPersonController.enabled = true;
		}

		private void RUPTUUURE()
		{
			_animator.SetBool(_animIDAttack, false);
			_animator.SetBool(_animIDAttack2, false);
			_animator.SetBool(_animIDAttack3, false);
			_animator.SetBool(_animIDAttack4, false);
			_animator.SetBool(_animIDAttack5, false);
		}

		private void Start()
		{
			TryGetComponent(out life);
			_hasAnimator = TryGetComponent(out _animator);
			_controller = GetComponent<CharacterController>();
			_input = GetComponent<StarterAssetsInputs>();

			AssignAnimationIDs();
			swordMesh.SetActive(isAttacking);
			footMesh.SetActive(isAttacking);
			// reset our timeouts on start
			_jumpTimeoutDelta = JumpTimeout;
			_fallTimeoutDelta = FallTimeout;


			if (_animator.GetBool(_animIDIsShi))
			{
				GoShinobi();
			}
			else if (_animator.GetBool(_animIDIsGS))
			{
				GoGS();
			}
			else if (_animator.GetBool(_animIDIsBrw))
			{
				GoBrw();
			}
		}

		public void ChangeStance()
		{
			_animator.SetBool(_animIDChangeStance, false);
			attacked = false;
			waitingForInput = false;
			gotInput = false;
			isAttacking = false;
			swordMesh.SetActive(isAttacking);
			footMesh.SetActive(isAttacking);
		}

		public void ChangeStanceT()
		{
			_animator.SetBool(_animIDChangeStance, true);
		}

		private void RUPTURECloath()
		{
			_animator.SetBool(_animIDIsShi, false);
			_animator.SetBool(_animIDIsGS, false);
			_animator.SetBool(_animIDIsBrw, false);
		}

		public void GoShinobi()
		{
			foreach (GameObject cloathe in everyCloathe)
			{
				cloathe.SetActive(false);
			}

			foreach (GameObject cloathe in shinobiCloathes)
			{
				cloathe.SetActive(true);
			}

			RUPTURECloath();
			_animator.SetBool(_animIDIsShi, true);
			IsShi = true;
			IsGS = false;
			IsBrw = false;
		}

		public void GoGS()
		{
			foreach (GameObject cloathe in everyCloathe)
			{
				cloathe.SetActive(false);
			}

			foreach (GameObject cloathe in GSCloathes)
			{
				cloathe.SetActive(true);
			}

			RUPTURECloath();
			_animator.SetBool(_animIDIsGS, true);
			IsShi = false;
			IsGS = true;
			IsBrw = false;
		}

		public void GoBrw()
		{
			foreach (GameObject cloathe in everyCloathe)
			{
				cloathe.SetActive(false);
			}

			foreach (GameObject cloathe in BrwCloathes)
			{
				cloathe.SetActive(true);
			}

			RUPTURECloath();
			_animator.SetBool(_animIDIsBrw, true);
			IsShi = false;
			IsGS = false;
			IsBrw = true;
		}

		

		private void Update()
		{
			
			if (!isLocalPlayer) return;
			if (waitingForInput)
			{
				if (_input.attaque && !gotInput)
				{
					combo += 1;
					gotInput = true;
				}
			}


			_hasAnimator = TryGetComponent(out _animator);
			if (_hasAnimator)
			{
				_animator.SetBool(_animIDDeath, false);
			}

			Attaque();


			if (!Died)
			{
				isAlive();
			}

			if (IliesTi1BATAR && !attacked)
			{
				JumpAndGravity();
			}

			GroundedCheck();
			if (SaqrTi1BATAR && !attacked)
			{
				Move();
			}

			if (gotInput)
			{
				switch (combo)
				{
					case 1:
						_animator.SetBool(_animIDAttack2, true);
						break;

					case 2:
						_animator.SetBool(_animIDAttack3, true);
						break;

					case 3:
						_animator.SetBool(_animIDAttack4, true);
						break;

					case 4:
						_animator.SetBool(_animIDAttack5, true);
						break;
					default:
						UGOKE();
						break;
				}
			}

			if (!waitingForInput && !gotInput)
			{
				combo = 0;
				attacked = false;
			}
		}

		private void UGOKE()
		{
			attacked = false;
			waitingForInput = false;
			RUPTUUURE();
		}

		private void LateUpdate()
		{

			if (!waitingForInput)
			{
				gotInput = false;
			}

			CameraRotation();
		}

		private void isAlive()
		{
			if (life.vie <= 0)
			{
				SaqrTi1BATAR = false;
				IliesTi1BATAR = false;
				Died = true;
				UIGameOver.instance.OnPlayerDeath();
				_animator.SetBool(_animIDDeath, true);

			}
		}

		private void WatingForInput()
		{
			waitingForInput = !waitingForInput;
			attacked = !attacked;
			gotInput = false;
			isAttacking = !isAttacking;
			swordMesh.SetActive(isAttacking);
			footMesh.SetActive(isAttacking);
		}

		private void AssignAnimationIDs()
		{
			_animIDSpeed = Animator.StringToHash("Speed");
			_animIDGrounded = Animator.StringToHash("Grounded");
			_animIDJump = Animator.StringToHash("Jump");
			_animIDFreeFall = Animator.StringToHash("FreeFall");
			_animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
			_animIDDeath = Animator.StringToHash("Dead");
			_animIDAttack = Animator.StringToHash("Attack");
			_animIDAttack2 = Animator.StringToHash("Attack2");
			_animIDAttack3 = Animator.StringToHash("Attack3");
			_animIDAttack4 = Animator.StringToHash("Attack4");
			_animIDAttack5 = Animator.StringToHash("Attack5");
			_animIDIsShi = Animator.StringToHash("IsShi");
			_animIDIsGS = Animator.StringToHash("IsGS");
			_animIDIsBrw = Animator.StringToHash("IsBrw");
			_animIDChangeStance = Animator.StringToHash("ChangeStance");
		}

		private void GroundedCheck()
		{
			// set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

			// update animator if using character
			if (_hasAnimator)
			{
				_animator.SetBool(_animIDGrounded, Grounded);
			}
		}

		private void CameraRotation()
		{
			// if there is an input and camera position is not fixed
			if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
			{
				_cinemachineTargetYaw += _input.look.x * Time.deltaTime;
				_cinemachineTargetPitch += _input.look.y * Time.deltaTime;
			}

			// clamp our rotations so our values are limited 360 degrees
			_cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
			_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

			// Cinemachine will follow this target
			CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
		}

		private void Attackcontinues()
		{
			if (combo == 2)
			{
				Debug.Log("ataaaaaack");
				_animator.SetBool(_animIDAttack, true);
			}
			else
			{
				combo = 0;
				_animator.SetBool(_animIDAttack, false);
				_animator.SetBool(_animIDAttack2, false);
				_animator.SetBool(_animIDAttack3, false);
				_animator.SetBool(_animIDAttack4, false);
				_animator.SetBool(_animIDAttack5, false);
			}

		}

		private void Attaque()
		{
			if (_input.attaque && combo == 0 && !attacked)
			{
				combo += 1;
				Debug.Log("ataaaaaack");
				_animator.SetBool(_animIDAttack, true);
			}
			_input.attaque = false;
		}

		private void Move()
		{
			// set target speed based on move speed, sprint speed and if sprint is pressed
			float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_input.move == Vector2.zero) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}
			_animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);

			// normalise input direction
			Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (_input.move != Vector2.zero)
			{
				_targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
				float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

				// rotate to face input direction relative to camera position
				transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
			}


			Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

			// move the player
			_controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

			// update animator if using character
			if (_hasAnimator)
			{
				_animator.SetFloat(_animIDSpeed, _animationBlend);
				_animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
			}
		}

		private void JumpAndGravity()
		{
			if (Grounded)
			{
				// reset the fall timeout timer
				_fallTimeoutDelta = FallTimeout;

				// update animator if using character
				if (_hasAnimator)
				{
					_animator.SetBool(_animIDJump, false);
					_animator.SetBool(_animIDFreeFall, false);
				}

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocity < 0.0f)
				{
					_verticalVelocity = -2f;
				}

				// Jump
				if (_input.jump && _jumpTimeoutDelta <= 0.0f)
				{
					// the square root of H * -2 * G = how much velocity needed to reach desired height
					_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

					// update animator if using character
					if (_hasAnimator)
					{
						_animator.SetBool(_animIDJump, true);
					}
				}

				// jump timeout
				if (_jumpTimeoutDelta >= 0.0f)
				{
					_jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{
				// reset the jump timeout timer
				_jumpTimeoutDelta = JumpTimeout;

				// fall timeout
				if (_fallTimeoutDelta >= 0.0f)
				{
					_fallTimeoutDelta -= Time.deltaTime;
				}
				else
				{
					// update animator if using character
					if (_hasAnimator)
					{
						_animator.SetBool(_animIDFreeFall, true);
					}
				}

				// if we are not grounded, do not jump
				_input.jump = false;
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocity < _terminalVelocity)
			{
				_verticalVelocity += Gravity * Time.deltaTime;
			}
		}

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (Grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;

			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		}
	}
}