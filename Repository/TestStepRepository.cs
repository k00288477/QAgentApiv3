using QAgentApi.Model;
using QAgentApi.Repository.Interfaces;

namespace QAgentApi.Repository
{
    public class TestStepRepository : ITestStepRepository
    {
        public Task DeleteTestStepById(int testStepId)
        {
            throw new NotImplementedException();
        }

        public Task<TestStep?> GetTestStepById(int testStepId)
        {
            throw new NotImplementedException();
        }

        public Task<TestStep> InsertNewTestStep(TestStep testStep)
        {
            throw new NotImplementedException();
        }

        public Task<TestStep> UpdateTestStep(TestStep testStep)
        {
            throw new NotImplementedException();
        }
    }
}
