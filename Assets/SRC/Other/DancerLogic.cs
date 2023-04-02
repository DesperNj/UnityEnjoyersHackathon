using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class DancerLogic : MonoBehaviour
{
    public SkeletonAnimation skeleton;
    // Start is called before the first frame update
    void Start()
    {
        skeleton = GetComponent<SkeletonAnimation>();
    }
    public void DancerCatchBeat()
    {
        skeleton.AnimationName = "dance";
        Invoke(nameof(SetIdle), 0.3f);
    }
    public void DancerMissBeat()
    {
        skeleton.AnimationName = "fail";
        Invoke(nameof(SetIdle), 0.3f);
    }
    public void SetIdle()
    {
        skeleton.AnimationName = "idle";
    }
}
