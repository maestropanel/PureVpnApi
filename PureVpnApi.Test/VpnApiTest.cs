namespace PureVpnApi.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class VpnApiTest
    {
        private readonly string apiUserName = "test";
        private readonly string apiPassword = "test";

        [TestMethod]
        public void CreateUser()
        {
            var result = new VpnRequest()
                            .Authentication(apiUserName, apiPassword)
                            .Package("STANDARD")
                            .Period(90)
                            .Create()
                            .Send();
            
            Assert.AreEqual(1, result.Result);
            Assert.AreNotEqual(String.Empty, result.User);
            Assert.AreNotEqual(String.Empty, result.Password);
        }

        [TestMethod]
        public void RenewUser()
        {
            var result = new VpnRequest()
                            .Authentication(apiUserName, apiPassword)
                            .Username("vpnuser123")                            
                            .Period(90)
                            .Renew()
                            .Send();

            Assert.AreEqual(1, result.Result);
            Assert.AreNotEqual(String.Empty, result.User);
            Assert.AreNotEqual(String.Empty, result.Password);
        }

        [TestMethod]
        public void FindStatus()
        {
            var result = new VpnRequest()
                            .Authentication(apiUserName, apiPassword)
                            .Username("vpnuser123")                            
                            .FindStatus()
                            .Send();

            Assert.AreEqual(1, result.Result);
            Assert.AreNotEqual(String.Empty, result.User);
            Assert.AreNotEqual(String.Empty, result.Status);
        }

        [TestMethod]
        public void Delete()
        {
            var result = new VpnRequest()
                            .Authentication(apiUserName, apiPassword)
                            .Username("vpnuser123")
                            .Delete()
                            .Send();

            Assert.AreEqual(1, result.Result);            
            Assert.AreNotEqual(String.Empty, result.Status);
        }

        [TestMethod]
        public void DisableUser()
        {
            var result = new VpnRequest()
                            .Authentication(apiUserName, apiPassword)
                            .Username("vpnuser123")
                            .Update("disable")
                            .Send();

            Assert.AreEqual(1, result.Result);            
        }

        [TestMethod]
        public void ChangePassword()
        {
            var result = new VpnRequest()
                            .Authentication(apiUserName, apiPassword)
                            .Username("vpnuser123")
                            .ChangePassword("p@ssw0rd")
                            .Send();

            Assert.AreEqual(1, result.Result);
        }

    }
}
