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
        Task<string> Login(LoginRequest request);
        Task<bool> CheckAvailableUserName(string email);
        Task<bool> Register(RegisterRequest request); 
    }
}
