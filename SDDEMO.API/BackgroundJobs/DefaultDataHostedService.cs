
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Identity;
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

        /// <summary>
        /// Admin's datas seed Method.
        /// </summary>
        /// <returns></returns>
        public async Task SeedDefaultData()
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
                var passwordHasher = new PasswordHasher<User>();

                User user = DefaultDataGenerator.DefaultUser();

                user.SetPassword("serkan");
                var existingUser = unitOfWork.userRepository.GetById(user.id);

                if (existingUser == null)
                {
                    unitOfWork.userRepository.Add(user);
                }
                else
                {
                    existingUser.username = user.username;
                    existingUser.passwordHash = user.passwordHash;
                    existingUser.mailAddress = user.mailAddress;
                    existingUser.name = user.name;
                    existingUser.surname = user.surname;
                    existingUser.creationDate = user.creationDate;
                    existingUser.isActive = user.isActive;
                    existingUser.isDeleted = user.isDeleted;
                    existingUser.updatedDate = user.updatedDate;

                    unitOfWork.userRepository.Update(existingUser);
                }
                unitOfWork.CommitChanges();
            }
        }
    }
}
