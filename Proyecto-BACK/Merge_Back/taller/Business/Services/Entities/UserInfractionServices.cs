using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces.IBusinessImplements.Entities;
using Business.Repository;
using Data.Interfaces.IDataImplement.Entities;
using Entity.Domain.Models.Implements.Entities;
using Entity.DTOs.Interface.Entities;
using Microsoft.Extensions.Logging;

namespace Business.Services.Entities
{
    public class UserInfractionServices : BusinessBasic<UserInfractionDto, UserInfractionSelectDto, UserInfraction>, IUserInfractionServices
    {
        private readonly ILogger<UserInfractionServices> _logger;
        private readonly IUserInfractionRepository _userInfractionRepository;

        public UserInfractionServices(IUserInfractionRepository data , IMapper mapper , ILogger<UserInfractionServices> logger) : base(data, mapper)
        {
            _userInfractionRepository = data;
            _logger = logger;
        }
    }
}
