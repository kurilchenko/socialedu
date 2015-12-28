using UnityEngine;
using System.Collections;

namespace Awespace
{

    public class TimelinePoses : Timeline
    {

        public PosePlayer poses;

        public override float Duration
        {
            get
            {
                return poses.Duration;
            }
        }

        public override void Play(float localRunningTime)
        {
            base.Play(localRunningTime);

            poses.time = localRunningTime;
            poses.Play();
        }

        public override void Pause()
        {
            base.Pause();

            poses.Pause();
        }

    }

}

