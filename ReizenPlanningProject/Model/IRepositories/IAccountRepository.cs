using ReizenPlanningProject.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model.IRepositories
{
    public interface IAccountRepository
    {
        Task<bool> LoginAsync(LoginRequest request);
    }
}
