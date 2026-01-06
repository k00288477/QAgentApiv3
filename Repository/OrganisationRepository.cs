using QAgentApi.Data;
using QAgentApi.Model;
using QAgentApi.Repository.Interfaces;

namespace QAgentApi.Repository
{
    public class OrganisationRepository : IOrganisationRepository
    {
        private readonly AppDBContext _context;
        public OrganisationRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task DeleteOrgById(int orgId)
        {
            var org = await _context.Organisations.FindAsync(orgId);
            if (org != null)
            {
                _context.Organisations.Remove(org);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Organisation?> GetOrgById(int orgId)
        {
            return await _context.Organisations.FindAsync(orgId);
        }

        public async Task<Organisation> InsertNewOrg(Organisation org)
        {
            _context.Organisations.Add(org);
            await  _context.SaveChangesAsync();
            return org;
        }

        public async Task<Organisation> UpdateOrg(Organisation org)
        {
            _context.Organisations.Update(org);
            await _context.SaveChangesAsync();
            return org;
        }
    }
}
