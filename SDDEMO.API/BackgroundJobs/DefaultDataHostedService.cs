﻿
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Extensions.DependencyInjection;
using NPOI.OpenXml4Net.OPC.Internal;
using NPOI.POIFS.Crypt;
using SDDEMO.Application.DefaultDataGenerator.cs;
using SDDEMO.Application.Interfaces.UnitOfWork;
using SDDEMO.Domain.Entity;
using SDDEMO.Persistance.UnitOfWork;
using System.Drawing.Imaging;

namespace SDDEMO.API.BackgroundJobs
{
    public class DefaultDataHostedService : IHostedService
    {
        private IServiceScopeFactory serviceScopeFactory;
        private IUnitOfWork unitOfWork;
        public DefaultDataHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await SeedDefaultData();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {

        }

        public async Task SeedDefaultData()
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

                User user = DefaultDataGenerator.DefaultUser();

                var existingUser = unitOfWork.userRepository.GetById(user.id);

                if (existingUser == null)
                {
                    unitOfWork.userRepository.Add(user);
                }
                else
                {
                    existingUser.username = user.username;
                    existingUser.password = user.password;
                    existingUser.mailAddress = user.mailAddress;
                    existingUser.name = user.name;
                    existingUser.surname = user.surname;
                    existingUser.creationDate = user.creationDate;
                    existingUser.isActive = user.isActive;
                    existingUser.isDeleted = user.isDeleted;
                    existingUser.updatedDate = user.updatedDate;
                    existingUser.createdBy = user.createdBy;

                    unitOfWork.userRepository.Update(existingUser);
                }
                unitOfWork.CommitChanges();
            }
        }

    }
}
