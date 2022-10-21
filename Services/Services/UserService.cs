using Application.EntityDTO;
using Application.Intrefaces.Repository.Base;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Services.DTO;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    internal class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private TResponse _response = new();
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        async Task<TResponse> IUserService.GetUserById(int id)
        {
            _response.ResponseCode = StatusCodes.Status200OK;
            _response.ResponseMessage = ResponseMessage.Success;
            //_response.ResponsePacket = await _context.Users.FindAsync(id);
            var user = await _unitOfWork.UserRepo.GetUserByIdAsync(id);
            _response.ResponsePacket = _mapper.Map<MemberUserDto>(user);
            _response.ResponseStatus = true;
            return _response;
        }

        async Task<TResponse> IUserService.GetUsers()
        {
            _response.ResponseCode = StatusCodes.Status200OK;
            _response.ResponseMessage = ResponseMessage.Success;
            //_response.ResponsePacket = await _context.Users.ToListAsync();
            _response.ResponsePacket = await _unitOfWork.UserRepo.GetAllMembersAsync();
            //_response.ResponsePacket =  _mapper.Map<IEnumerable<MemberUserDto>>(await _unitOfWork.UserRepo.GetAllUsersAsync());
            _response.ResponseStatus = true;
            return _response;
        }

        async Task<TResponse> IUserService.GetUserByName(string name)
        {
            _response.ResponseCode = StatusCodes.Status200OK;
            _response.ResponseMessage = ResponseMessage.Success;
            //_response.ResponsePacket = await _context.Users.ToListAsync();
            //_response.ResponsePacket = _mapper.Map<MemberUserDto>(await _unitOfWork.UserRepo.FindUserAsync(x => x.UserName == name));
            _response.ResponsePacket = await _unitOfWork.UserRepo.GetMembersAsync(name);
            _response.ResponseStatus = true;
            return _response;
        }

        async Task<TResponse> IUserService.GetMembersAsync()
        {
            _response.ResponseCode = StatusCodes.Status200OK;
            _response.ResponseMessage = ResponseMessage.Success;
            //_response.ResponsePacket = await _context.Users.ToListAsync();
            _response.ResponsePacket = await _unitOfWork.UserRepo.GetAllMembersAsync();
            _response.ResponseStatus = true;
            return _response;
        }
    }
}
