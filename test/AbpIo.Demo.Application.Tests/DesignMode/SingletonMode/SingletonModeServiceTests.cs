using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AbpIo.Demo.DesignMode.SingletonMode
{
    #region
    //意图：保证一个类仅有一个实例，并提供一个访问它的全局访问点。
    //主要解决：一个全局使用的类频繁地创建与销毁。
    //何时使用：当您想控制实例数目，节省系统资源的时候。
    //如何解决：判断系统是否已经有这个单例，如果有则返回，如果没有则创建。
    //关键代码：构造函数是私有的。
    #endregion

    
    /// <summary>
    /// 单例模式
    /// </summary>
    public class SingletonModeServiceTests: DemoApplicationTestBase
    {
        [Fact]
        public void test()
        {
            //获取唯一可用的对象
            var vv = SingleObject.getInstance();
            vv.ShouldNotBeNull();
        }
    }
    /// <summary>
    /// 饿汉式
    /// </summary>
    public class SingleObject
    {
        private static SingleObject instance = new SingleObject();
        private SingleObject() { }
        public static SingleObject getInstance()
        {
            return instance;
        }
    }

    /// <summary>
    /// 懒汉式
    /// </summary>
    public class SingleObject1
    {
        private static SingleObject1 instance = new SingleObject1();
        private SingleObject1() { }
        public static SingleObject1 getInstance()
        {
            if (instance == null)//线程不安全
            {
                instance = new SingleObject1();
            }
            return instance;
        }
    }
    

    /// <summary>
    /// 懒汉式
    /// </summary>
    public class SingleObject2
    {
        private static SingleObject2 instance;
        private SingleObject2() { }
        private static readonly object _lock=new object();
        public static SingleObject2 getInstance()
        {
            if (instance ==null)
            {
                lock (_lock)//线程安全
                {
                    instance = new SingleObject2();
                }
            }
            return instance;
        }
    }
    /// <summary>
    /// 双验锁
    /// </summary>
    public class SingleObject3 {
        private volatile static SingleObject3 singleton;
        private SingleObject3() { }
        private static object _lock=new object();
        public static SingleObject3 getSingleton() {
            if (singleton==null)
            {
                lock (_lock)
                {
                    if (singleton == null)
                    {
                        singleton = new SingleObject3();
                    }
                }
            }
            return singleton;
        }
    }

    //public class SingleObject4 {
    //    private static class SingleObject4Holder { 
    //        private static  SingleObject4 instance=new SingleObject4();
    //    }
    //    private SingleObject4() { }
    //    public static  SingleObject4 getInstance()
    //    {
    //        return SingleObject4Holder.instance;
    //    }
    //}



}
