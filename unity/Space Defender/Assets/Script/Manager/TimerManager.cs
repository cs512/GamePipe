using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void Handler();
public delegate void Handler<T1>(T1 param1);
public delegate void Handler<T1, T2>(T1 param1, T2 param2);
public delegate void Handler<T1, T2, T3>(T1 param1, T2 param2, T3 param3);

public interface IAnimatable
{
    void AdvanceTime();
}

/**时钟管理器[同一函数多次计时，默认会被后者覆盖,delay小于1会立即执行]*/
public class TimerManager : PureSingleton<TimerManager>, IAnimatable
{

    public static List<IAnimatable> timerList = new List<IAnimatable>();

    public TimerManager()
    {
        timerList.Add(this);
    }

    private List<TimerHandler> _pool = new List<TimerHandler>();
    /** 用数组保证按放入顺序执行*/
    private List<TimerHandler> _handlers = new List<TimerHandler>();
    private int _currFrame = 0;
    // private uint _index = 0;

    public void AdvanceTime()
    {
        _currFrame++;
        for (int i = 0; i < _handlers.Count; i++)
        {
            TimerHandler handler = _handlers[i];
            long t = handler.userFrame ? _currFrame : currentTime;
            if (t >= handler.exeTime)
            {
                Delegate method = handler.method;
                object[] args = handler.args;
                if (handler.repeat)
                {
                    while (t >= handler.exeTime)
                    {
                        handler.exeTime += handler.delay;
                        method.DynamicInvoke(args);
                    }
                }
                else
                {
                    clear(handler.method);
                    method.DynamicInvoke(args);
                }
            }
        }
    }

    private object create(bool useFrame, bool repeat, int delay, Delegate method, params object[] args)
    {
        if (method == null)
        {
            return null;
        }

        //如果执行时间小于1，直接执行
        if (delay < 1)
        {
            method.DynamicInvoke(args);
            return -1;
        }
        TimerHandler handler;
        if (_pool.Count > 0)
        {
            handler = _pool[_pool.Count - 1];
            _pool.Remove(handler);
        }
        else
        {
            handler = new TimerHandler();
        }
        handler.userFrame = useFrame;
        handler.repeat = repeat;
        handler.delay = delay;
        handler.method = method;
        handler.args = args;
        handler.exeTime = delay + (useFrame ? _currFrame : currentTime);
        _handlers.Add(handler);
        return method;
    }

    /// /// <summary>
    /// 定时执行一次(基于毫秒)
    /// </summary>
    /// <param name="delay">延迟时间(单位毫秒)</param>
    /// <param name="method">结束时的回调方法</param>
    /// <param name="args">回调参数</param>
    public void doOnce(int delay, Handler method)
    {
        create(false, false, delay, method);
    }
    public void doOnce<T1>(int delay, Handler<T1> method, params object[] args)
    {
        create(false, false, delay, method, args);
    }
    public void doOnce<T1, T2>(int delay, Handler<T1, T2> method, params object[] args)
    {
        create(false, false, delay, method, args);
    }
    public void doOnce<T1, T2, T3>(int delay, Handler<T1, T2, T3> method, params object[] args)
    {
        create(false, false, delay, method, args);
    }

    /// /// <summary>
    /// 定时重复执行(基于毫秒)
    /// </summary>
    /// <param name="delay">延迟时间(单位毫秒)</param>
    /// <param name="method">结束时的回调方法</param>
    /// <param name="args">回调参数</param>
    public void doLoop(int delay, Handler method)
    {
        create(false, true, delay, method);
    }
    public void doLoop<T1>(int delay, Handler<T1> method, params object[] args)
    {
        create(false, true, delay, method, args);
    }
    public void doLoop<T1, T2>(int delay, Handler<T1, T2> method, params object[] args)
    {
        create(false, true, delay, method, args);
    }
    public void doLoop<T1, T2, T3>(int delay, Handler<T1, T2, T3> method, params object[] args)
    {
        create(false, true, delay, method, args);
    }


    /// <summary>
    /// 定时执行一次(基于帧率)
    /// </summary>
    /// <param name="delay">延迟时间(单位为帧)</param>
    /// <param name="method">结束时的回调方法</param>
    /// <param name="args">回调参数</param>
    public void doFrameOnce(int delay, Handler method)
    {
        create(true, false, delay, method);
    }
    public void doFrameOnce<T1>(int delay, Handler<T1> method, params object[] args)
    {
        create(true, false, delay, method, args);
    }
    public void doFrameOnce<T1, T2>(int delay, Handler<T1, T2> method, params object[] args)
    {
        create(true, false, delay, method, args);
    }
    public void doFrameOnce<T1, T2, T3>(int delay, Handler<T1, T2, T3> method, params object[] args)
    {
        create(true, false, delay, method, args);
    }

    /// <summary>
    /// 定时重复执行(基于帧率)
    /// </summary>
    /// <param name="delay">延迟时间(单位为帧)</param>
    /// <param name="method">结束时的回调方法</param>
    /// <param name="args">回调参数</param>
    public void doFrameLoop(int delay, Handler method)
    {
        create(true, true, delay, method);
    }
    public void doFrameLoop<T1>(int delay, Handler<T1> method, params object[] args)
    {
        create(true, true, delay, method, args);
    }
    public void doFrameLoop<T1, T2>(int delay, Handler<T1, T2> method, params object[] args)
    {
        create(true, true, delay, method, args);
    }
    public void doFrameLoop<T1, T2, T3>(int delay, Handler<T1, T2, T3> method, params object[] args)
    {
        create(true, true, delay, method, args);
    }

    /// <summary>
    /// 清理定时器
    /// </summary>
    /// <param name="method">method为回调函数本身</param>
    public void clearTimer(Handler method)
    {
        clear(method);
    }
    public void clearTimer<T1>(Handler<T1> method)
    {
        clear(method);
    }
    public void clearTimer<T1, T2>(Handler<T1, T2> method)
    {
        clear(method);
    }
    public void clearTimer<T1, T2, T3>(Handler<T1, T2, T3> method)
    {
        clear(method);
    }

    private void clear(Delegate method)
    {
        TimerHandler handler = _handlers.FirstOrDefault(t => t.method == method);
        if (handler != null)
        {
            _handlers.Remove(handler);
            handler.clear();
            _pool.Add(handler);
        }
    }

    /// <summary>
    /// 清理所有定时器
    /// </summary>
    public void clearAllTimer()
    {
        foreach (TimerHandler handler in _handlers)
        {
            clear(handler.method);
            clearAllTimer();
            return;
        }
    }

    public static void RemoveTimerMgr(TimerManager timerMgr)
    {
        timerList.Remove(timerMgr);
    }

    /// <summary>
    /// 游戏自启动运行时间，毫秒
    /// </summary>
    public long currentTime
    {
        get { return (long)(Time.time * 1000); }
    }

    /**定时处理器*/

    private class TimerHandler
    {
        /**执行间隔*/
        public int delay;
        /**是否重复执行*/
        public bool repeat;
        /**是否用帧率*/
        public bool userFrame;

        /**执行时间*/
        public long exeTime;

        /**处理方法*/
        public Delegate method;

        /**参数*/
        public object[] args;

        /**清理*/

        public void clear()
        {
            method = null;
            args = null;
        }
    }
}