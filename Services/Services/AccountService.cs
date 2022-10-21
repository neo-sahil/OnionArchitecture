using Application.Intrefaces.Repository.Base;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Services.DTO;
using Services.IServices;
using Services.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private TResponse _response = new();
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        async Task<TResponse> IAccountService.logIn(LogInDTO obj)
        {
            var user = (await _unitOfWork.UserRepo.FindAsync(x => x.UserName == obj.userName)).FirstOrDefault<AppUser>();
            if(user == null)
            {
                _response.ResponseCode = StatusCodes.Status401Unauthorized;
                _response.ResponseMessage = "Invalid Users!!";
                _response.ResponsePacket = null;
                _response.ResponseStatus = false;
            }
            else
            {
                using var hmac = new HMACSHA512(user.PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(obj.Password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.PasswordHash[i])
                    {
                        _response.ResponseCode = StatusCodes.Status401Unauthorized;
                        _response.ResponseMessage = "UserName Or password doesnot match!!";
                        _response.ResponsePacket = null;
                        _response.ResponseStatus = false;
                        return _response;
                    }
                }
                _response.ResponseCode = StatusCodes.Status200OK;
                _response.ResponseMessage = ResponseMessage.LoginSuccess;
                _response.ResponsePacket = new UserRtnDTO
                {
                    UserName = user.UserName,
                    Token = await _tokenService.CreateToken(user)
                };
                _response.ResponseStatus = true;
            }
            return _response;
        }
    }
}
