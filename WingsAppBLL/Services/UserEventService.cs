using System;
using System.Collections.Generic;
using System.Text;
using WingsAppDAL;
using System.Linq;
using WingsAppDAL.Entities;
using WingsAppBLL.BusinessObjects;
using WingsAppBLL.Converters;

namespace WingsAppBLL.Services
{
    class UserEventService : IUserEventService
    {
        UserEventConverter conv = new UserEventConverter();
        DALFacade facade;
        public UserEventService(DALFacade facade)
        {
            this.facade = facade;
        }
        //Create
        public UserEventBO Create(UserEventBO userEvent)
        {
            using(var uow = facade.UnitOfWork)
            {
                var newEvent = uow.UserEventRepository.Create(conv.Convert(userEvent));
                uow.Complete();
                return conv.Convert(newEvent);
            }
        }

        //Read
        public List<UserEventBO> GetAll()
        {
            using(var uow = facade.UnitOfWork)
            {
                return uow.UserEventRepository.GetAll().Select(ue => conv.Convert(ue)).ToList();
            }       
        }


        public UserEventBO Get(int Id)
        {
            using(var uow = facade.UnitOfWork)
            {
                var userEvent = uow.UserEventRepository.Get(Id);
                userEvent.Reporter = uow.UserProfileRepository.Get(userEvent.ReporterId);
                return conv.Convert(userEvent);
            }
        }

        //Update
        public UserEventBO Update(UserEventBO userEvent)        
        {
            using(var uow = facade.UnitOfWork)
            {
                var userEventFromDb = uow.UserEventRepository.Get(userEvent.Id);
                if (userEventFromDb == null)
                {
                    throw new InvalidOperationException("User Event not found");
                }
                var userEventUpdated = conv.Convert(userEvent);
                userEventFromDb.Title = userEventUpdated.Title;
                userEventFromDb.Description = userEventUpdated.Description;
                userEventFromDb.ReporterId = userEventUpdated.ReporterId;
                userEventFromDb.Types = userEventUpdated.Types;
                uow.Complete();
                userEventFromDb.Reporter = uow.UserProfileRepository.Get(userEventUpdated.ReporterId);
                return conv.Convert(userEventFromDb);
            }
        }

        //Delete
        public UserEventBO Delete(int Id)
        {
            using(var uow = facade.UnitOfWork)
            {
                var deletedEvent = uow.UserEventRepository.Delete(Id);
                uow.Complete();
                return conv.Convert(deletedEvent);
            }
        }
    }
}
