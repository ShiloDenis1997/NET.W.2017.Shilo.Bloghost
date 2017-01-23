using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Concrete.Mappers;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;

namespace BLL.Concrete
{
    public class UserService : IUserService
    {
        private IUnitOfWork unitOfWork;
        private IRepository<DalUser> repository;

        public UserService(IUnitOfWork uow, IRepository<DalUser> repo)
        {
            unitOfWork = uow;
            repository = repo;
        }

        public void CreateUser(UserEntity user)
        {
            repository.Create(user.ToDalUser());
            unitOfWork.Commit();
        }

        public void DeleteUser(UserEntity user)
        {
            repository.Delete(user.ToDalUser());
            unitOfWork.Commit();
        }

        public IEnumerable<UserEntity> GetAllUserEntities()
            => repository.GetAll().Select(user => user.ToUserEntity());

        public UserEntity GetUserEntity(int id)
            => repository.GetById(id).ToUserEntity();

        public void UpdateUser(UserEntity user)
        {
            try
            {
                repository.Update(user.ToDalUser());
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Cannot update user", ex);
            }
            
            unitOfWork.Commit();
        }
    }
}
