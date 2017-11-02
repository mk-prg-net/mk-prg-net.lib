using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace mko.Test
{
    [TestClass]
    public class RCTest
    {
        Logging.RC Function(bool failed)
        {
            var ret = (failed ? subFunction(6, 0) : subFunction(6, 2));
            return ret.Succeeded ? Logging.RC.Ok(Message: ret.Value.ToString()) : Logging.RC.Failed(inner: ret);
        }

        Logging.RC<int> subFunction(int a, int b)
        {
            try
            {
                return Logging.RC<int>.Ok(a / b);

            }catch(Exception ex)
            {
                return Logging.RC<int>.Failed(value: 0, ErrorDescription: ex.Message);
            }
        }

        public void TestInit()
        {
            var start = Logging.StartTimeSingleton.Instance.StartTime;
        }

        [TestMethod]
        public void TestMethod1()
        {

            var ret = Function(false);
            Assert.IsTrue(ret.Succeeded);
            Debug.WriteLine(ret.ToString());
            
            ret = Function(true);
            Assert.IsFalse(ret.Succeeded);
            Debug.WriteLine(ret.ToString());
            Debug.WriteLine(ret.InnerRCV2.ToString());


        }
    }
}
