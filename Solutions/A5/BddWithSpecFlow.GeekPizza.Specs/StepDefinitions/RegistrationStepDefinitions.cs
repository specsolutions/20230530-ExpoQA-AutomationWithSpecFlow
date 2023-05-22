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

        // Step definitions for the alternative approach

        private Table _registerInputTable;

        [Given("the client provides registration details as")]
        public void GivenTheClientProvidesRegistrationDetailsAs(Table registerInputTable)
        {
            // we only "remember" the input details here
            _registerInputTable = registerInputTable;
        }

        [Given("the field {string} is set to {string}")]
        public void GivenTheFieldIsSetTo(string field, string value)
        {
            if (field == "passwords") // special case to set both password fields
            {
                _registerInputTable.Rows[0]["password"] = value;
                _registerInputTable.Rows[0]["password re-enter"] = value;
            }
            else
            {
                _registerInputTable.Rows[0][field] = value;
            }
        }

        [When("the client attempts to register")]
        public void WhenTheClientAttemptsToRegister()
        {
            // we just need to perform the registration with the fields we built up
            WhenTheClientAttemptsToRegisterWith(_registerInputTable);
        }
    }
}
