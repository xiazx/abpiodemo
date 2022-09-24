using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AbpIo.Demo.DesignMode.FactoryMode
{
    #region
    // 工厂方法模式——在工厂方法模式中，工厂类与具体产品类具有平行的等级结构，它们之间是一一对应的
    //      工厂方法模式之所以可以解决简单工厂的模式，是因为它的实现把具体产品的创建推迟到子类中，此时工厂类不再负责所有产品的创建，而只是给出具体工厂必须实现的接口，这样工厂方法模式就可以允许系统不修改工厂类逻辑的情况下来添加新产品，这样也就克服了简单工厂模式中缺点
    //      用工厂方法实现的系统，如果系统需要添加新产品时，我们可以利用多态性来完成系统的扩展，对于抽象工厂类和具体工厂中的代码都不需要做任何改动。
    //例子：例如，我们我们还想点一个“肉末茄子”，此时我们只需要定义一个肉末茄子具体工厂类和肉末茄子类就可以。而不用像简单工厂模式中那样去修改工厂类中的实现
    #endregion
    /// <summary>
    /// 
    /// </summary>
    //[Fact]
    public class FactoryModeServiceTests
    {

    }


    public interface Ishape {
        void draw();
    }
    public class shape : Ishape
    {
        public void draw()
        { 
            
        }
    }
}
