
/*
     Author: Daniel Vindhjärta
     Last updated: 2018-01-01 
     Purpose:
     A class that keeps track of a countdown (i.e a timer)
     
     Usage:
     Call Tick() and pass 8for example) a deltatime value. Check Done() to see if the timer has run out.
     Progress() gives a percentage on how far the timer has progressed based on the start value compared
     to the current value in time.
     Reset() can be used to reset the timer if you don't want to delete the timer from memory and create
     a new one.
     
     Dependencies:
     None
     
*/
public class Timer
{
    private float duration = 0;
    private float time = 0;

    public Timer(float duration)
    {
        Reset(duration);
    }

    public float Time
    {
        get
        {
            return time;
        }
        set
        {
            if (value > duration) time = duration;
            if (value < 0) time = 0;
        }
    }

    public bool Done
    {
        get
        {
            if (time == 0) return true;
            return false;
        }
    }

    public void Tick(float step)
    {
        time -= step;
        if (time < 0)
            time = 0;
    }

    public float Progress
    {
        get
        {
            if (duration <= 0) return 0;
            return (duration-time) / duration;
        }
    }

    public void Reset(float duration)
    {
        if (duration < 0)
            this.duration = 0;
        else
            this.duration = duration;
        time = this.duration;
    }

}