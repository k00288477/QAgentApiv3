using QAgentApi.Model;

namespace QAgentApi.Repository.Interfaces
{
    public interface IOrganisationRepository
    {
        Task<Organisation> InsertNewOrg(Organisation org);
        Task<Organisation> UpdateOrg(Organisation org);
        Task<Organisation> GetOrgById(int orgId);
        Task DeleteOrgById(int orgId);
    }
}
