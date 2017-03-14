using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeddingServices.Services;
using WeddingServices.Utilities.Enums;

namespace WeddingServicesTest
{
    [TestClass]
    public class MailServicesTest
    {
        [TestMethod]
        public void TestSendMail()
        {
            var target = GetTarget();
            string dest = "pesentimatteo@gmail.com";
            string subject = "test";
            string message = "<h2>Test email</h2>";
            var result = target.SendMail(subject, message, new List<string>() { dest });
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result == ResultEnum.SUCCESS);
            Assert.IsTrue(result.Esito);
        }

        private MailServices GetTarget()
        {
            return new MailServices();
        }
    }
}
