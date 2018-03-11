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
            return ret.Succeeded ? Logging.RC.Ok(Message: ret.Value.ToString(), inner: ret) : Logging.RC.Failed(inner: ret);
        }

        Logging.RC<int> subFunction(int a, int b)
        {
            try
            {
                return Logging.RC<int>.Ok(a / b);

            }
            catch (Exception ex)
            {
                return Logging.RC<int>.Failed(value: 0, ErrorDescription: ex.Message);
            }
        }

        public void TestInit()
        {
            var start = Logging.StartTimeSingleton.Instance.StartTime;
        }

        [TestMethod]
        public void RCTest_RCExpression()
        {

            var ret = Function(false);
            Assert.IsTrue(ret.Succeeded);
            Debug.WriteLine(ret.ToString());

            ret = Function(true);
            Assert.IsFalse(ret.Succeeded);
            Debug.WriteLine(ret.ToString());
            Debug.WriteLine(ret.InnerRCV2.ToString());
        }

        [TestMethod]
        public void RCTest_RCBuilderForDeserializationPurpose()
        {
            {
                var ret = Function(false);

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(ret, Newtonsoft.Json.Formatting.Indented);
                Assert.IsFalse(string.IsNullOrWhiteSpace(json));

                var retDeser = Newtonsoft.Json.JsonConvert.DeserializeObject<Logging.RCBuilder>(json).CreateRC();

                Assert.AreEqual(ret.AssemblyName, retDeser.AssemblyName);
                Assert.AreEqual(ret.FunctionName, retDeser.FunctionName);
                Assert.IsNotNull(retDeser.InnerRCV2);
                Assert.AreEqual(ret.InnerRCV2.AssemblyName, retDeser.InnerRCV2.AssemblyName);
                Assert.AreEqual(ret.InnerRCV2.FunctionName, retDeser.InnerRCV2.FunctionName);
            }

            {
                var ret = subFunction(6, 2);
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(ret, Newtonsoft.Json.Formatting.Indented);
                var retDeser = Newtonsoft.Json.JsonConvert.DeserializeObject<Logging.RCBuilder<int>>(json).CreateRC();
                Assert.AreEqual(ret.Value, retDeser.Value);
            }
        }
    }
}
