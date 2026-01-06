using QAgentApi.Model;
using QAgentApi.Repository;

namespace QAgentApi.Service
{
    public class OrganisationService
    {
        // Dependency Injection
        private OrganisationRepository _orgRepo;

        // CONSTRUCTOR
        public OrganisationService(OrganisationRepository orgRepo)
        {
            _orgRepo = orgRepo;
        }

        // METHODS

        // Get Organisation by Id
        public async Task<Organisation?> GetOrganisationById(int orgId)
        {
            return await _orgRepo.GetOrgById(orgId);
        }

        // Add Organisation
        public async Task<Organisation> AddOrganisation(Organisation org)
        {
            return await _orgRepo.InsertNewOrg(org);
        }

        // Update Organisation
        public async Task<Organisation> UpdateOrganisation(Organisation org)
        {
            return await _orgRepo.UpdateOrg(org);
        }

        // Delete Organisation by Id
        public async Task DeleteOrganisationById(int orgId)
        {
            await _orgRepo.DeleteOrgById(orgId);
        }
    }
}
