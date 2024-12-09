using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManagerOrc : MonoBehaviour
{
    public static AnimManagerOrc Instance;
    Animator myAnimator;
    const string hitAnim = "Hit";
    const string missAnim = "Miss";
    const string deathAnim = "DeathOrc";


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        myAnimator = GetComponent<Animator>();
    }

    public static void Hit()
    {
        Instance.myAnimator.SetTrigger(hitAnim);
    }

    public static void Miss()
    {
        Instance.myAnimator.SetTrigger(missAnim);
    }

    public static void Death()
    {
        Instance.myAnimator.SetTrigger(deathAnim);
    }

}
