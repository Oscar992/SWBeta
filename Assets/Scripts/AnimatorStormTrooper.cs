using UnityEngine;
using System.Collections;

public class AnimatorStormTrooper : MonoBehaviour {

	Animator anim;
	private float elapsedTime;
    private float atackTime;
	public GameObject stt;
	public GameObject hips;
	private Vector3 lastPosition;
	bool isAttacking;
	bool hasDodgeLeft;
	bool hasDodgeRight;
	int lastAnimation;
	Vector3 initialPosition;
	void Start () 
	{
		anim = GetComponent<Animator> ();
		anim.SetBool ("isIdle",false);
		anim.SetBool ("isWDSword",true);
		anim.SetBool ("isFirstTimeWDS",true);
		elapsedTime = 0.0f;
        atackTime = elapsedTime + Random.Range(2.1f, 3.1f);
		hasDodgeRight = false;
		hasDodgeLeft = false;
		lastAnimation = 100;
		//lastPosition = hips.GetComponent<Transform> ().position;
		//anim.transform.parent.position
		initialPosition = transform.position;

	}

	void Update () {

		elapsedTime += Time.deltaTime;
		if (elapsedTime >= 2.0 && anim.GetBool("isFirstTimeWDS"))
        {
	    	anim.SetBool ("isIdle",true);
		    anim.SetBool ("isFirstTimeWDS",false);
		}
			
		if (!anim.GetBool ("isFirstTimeWDS"))
		{
            if (elapsedTime >= atackTime)
            {
                atackTime = elapsedTime + Random.Range(2.1f, 3.1f);

				//Random cuando esta atacando
				int pRandom = Random.Range(0, 4);
          
				//Random cuando se esta usando la fuerza
				pRandom = Random.Range(5,8);
				Debug.Log ("Random" + pRandom);

				while (pRandom == lastAnimation) {
					pRandom = Random.Range(5,8);
				}
					
			//	pRandom = 5;
                if (pRandom == 0)
                {
                    anim.SetBool("isAttacking1", true);
                }
                else if (pRandom == 1)
                {
                    anim.SetBool("isAttacking2", true);
                }
                else if (pRandom == 2)
                {
                    anim.SetBool("isAttacking3", true);
                }
                else if (pRandom == 3)
                {
                    anim.SetBool("isAttacking4", true);
                }
                else if (pRandom == 4)
                {
					isAttacking = true;
                    anim.SetBool("isAttacking5", true);
				} else if (pRandom == 5 && !hasDodgeLeft)
				{
					hasDodgeRight = false;
					
					anim.SetBool("isDodgeLeft", true);
				}else if (pRandom == 6 && !hasDodgeRight)
				{
					hasDodgeLeft = false;
					
					anim.SetBool("isDodgeRight", true);

				}else if (pRandom == 7)
				{
					anim.SetBool("isJumping",true);
				}else if (pRandom == 8)
				{
					anim.SetBool("isCrouch",true);
				}
				else if (Input.GetKeyDown(KeyCode.D))
                {
                    anim.SetBool("isDeath", true);
                } else
                {
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isAttacking1",false);
                    anim.SetBool("isAttacking2",false);
                    anim.SetBool("isAttacking3",false);
                    anim.SetBool("isAttacking4",false);
                    anim.SetBool("isAttacking5",false);
					anim.SetBool("isDodgeLeft",false);
					anim.SetBool("isDodgeRight",false);
					anim.SetBool("isCrouch",false);
					anim.SetBool("isJumping",false);
                    anim.SetBool("isDeath",false);
                }


				lastAnimation = pRandom;
            }
            else
            {
                anim.SetBool("isIdle", true);
                anim.SetBool("isAttacking1", false);
                anim.SetBool("isAttacking2", false);
                anim.SetBool("isAttacking3", false);
                anim.SetBool("isAttacking4", false);
                anim.SetBool("isAttacking5", false);
				anim.SetBool("isDodgeLeft",false);
				anim.SetBool("isDodgeRight",false);	
				anim.SetBool("isCrouch",false);
				anim.SetBool("isJumping",false);
                anim.SetBool("isDeath",false);
            }
        }
			

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("dodgeRight")) {
	
			transform.position = new Vector3 (transform.position.x - 0.5f, transform.position.y, transform.position.z); 
			Debug.Log (transform.position.x - 0.5f);

			if (transform.position.x - 0.5f <= -4f)
				hasDodgeRight = true;
			
		} 

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("dodgeLeft")) 
		{

			transform.position = new Vector3 (transform.position.x + 0.5f, transform.position.y, transform.position.z); 
			Debug.Log (transform.position.x - 0.5f);

			if (transform.position.x + 0.5f >= 4f)
				hasDodgeLeft = true;
			else
				hasDodgeLeft = false;
		}


		if(anim.GetCurrentAnimatorStateInfo(0).IsName("isJumping"))
			transform.position = new Vector3 (transform.position.x, transform.position.y + 0.2f, transform.position.z); 
		else
		{			
			if(!(transform.position.y > -7.0f &&  transform.position.y < -5.90f))
			transform.position = new Vector3 (transform.position.x, transform.position.y - 0.6f, transform.position.z);
		}


			
//		if(isAttacking)
//		{
//			Debug.Log("isAttacking5");
//			Vector3 diffVector;
//			Vector3 hipVector = hips.GetComponent<Transform> ().position;
//			diffVector = hipVector - lastPosition;
//			Vector3 pEnemyPosition = stt.GetComponent<Transform> ().position + diffVector;
//			// Avoid any reload.
//			//this.InMyState = true;
//			stt.GetComponent<Transform>().position = pEnemyPosition;
//			lastPosition = hipVector;
	//	}
	//	else 
	//	{
			//this.InMyState = false;
			// You have just leaved your state!
	//}
	}

	public void onAnimationEnded()
	{


		Vector3 diffVector;
	    //Vector3 hipVector = hips.GetComponent<Transform> ().position;
		diffVector = hips.GetComponent<Transform> ().position - stt.GetComponent<Transform> ().position;
		Vector3 pEnemyPosition =  new Vector3(stt.GetComponent<Transform> ().position.x + diffVector.x+3f,
									          stt.GetComponent<Transform> ().position.y,
			                                  stt.GetComponent<Transform> ().position.z);

	    Vector3 final = new Vector3 (pEnemyPosition.x,pEnemyPosition.y, pEnemyPosition.z);
		stt.GetComponent<Transform>().position = /*pEnemyPosition*/final;
		//lastPosition = hipVector;
	}


}
