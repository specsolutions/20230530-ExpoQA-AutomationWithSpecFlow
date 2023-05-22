using System;
using BddWithSpecFlow.GeekPizza.Web.Controllers;
using BddWithSpecFlow.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class RegistrationStepDefinitions
    {
        private Exception _registerError;

        private void AttemptRegister(RegisterInputModel inputModel)
        {
            try
            {
                _registerError = null;
                var controller = new UserController();
                controller.Register(inputModel);
            }
            catch (Exception ex)
            {
                _registerError = ex;
            }
        }

        [When("the client attempts to register with")]
        public void WhenTheClientAttemptsToRegisterWith(Table registerInputTable)
        {
            var inputModel = registerInputTable.CreateInstance(() => new RegisterInputModel
            {
                UserName = "Trillian",
                Password = "139139",
                PasswordReEnter = "139139"
            });

            AttemptRegister(inputModel);
        }

        [When("the client attempts to register with user name {string} and password {string}")]
        public void WhenTheClientAttemptsToRegisterWithUserNameAndPassword(string userName, string password)
        {
            AttemptRegister(new RegisterInputModel
            {
                UserName = userName,
                Password = password,
                PasswordReEnter = password
            });
        }

        [Then("the registration should be successful")]
        public void ThenTheRegistrationShouldBeSuccessful()
        {
            Assert.IsNull(_registerError, "The registration should be successful");
        }

        [Then("the registration should fail with {string}")]
        public void ThenTheRegistrationShouldFailWith(string expectedErrorMessage)
        {
            Assert.IsNotNull(_registerError, "The registration should fail");
            StringAssert.Contains(_registerError.Message, expectedErrorMessage);
        }
    }
}
