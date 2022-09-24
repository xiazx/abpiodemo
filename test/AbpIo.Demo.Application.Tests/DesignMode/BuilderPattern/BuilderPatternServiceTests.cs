﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpIo.Demo.DesignMode.BuilderPattern
{
    #region
    //  建造者模式——将一个复杂对象的构建与它的表示分离，使得同样的构建过程可以创建不同的表示。
    //　　　　　　 建造者模式使得建造代码与表示代码的分离，可以使客户端不必知道产品内部组成的细节，从而降低了客户端与具体产品之间的耦合度
    //  例如：一个采购系统中，如果需要采购员去采购一批电脑时，在这个实际需求中，电脑就是一个复杂的对象，它是由CPU、主板、硬盘、显卡、机箱等组装而成的，如果此时让采购员一台一台电脑去组装的话真是要累死采购员了，这里就可以采用建造者模式来解决这个问题，我们可以把电脑的各个组件的组装过程封装到一个建造者类对象里，建造者只要负责返还给客户端全部组件都建造完毕的产品对象就可以了。然而现实生活中也是如此的，如果公司要采购一批电脑，此时采购员不可能自己去买各个组件并把它们组织起来，此时采购员只需要像电脑城的老板说自己要采购什么样的电脑就可以了，电脑城老板自然会把组装好的电脑送到公司

    #endregion
   
    public class BuilderPatternServiceTests
    {

    }
}
