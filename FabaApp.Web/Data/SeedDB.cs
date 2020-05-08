using FabaApp.Common.Enums;
using FabaApp.Web.Data.Entities;
using FabaApp.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FabaApp.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {



            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckLabsAsync();


            LabEntity lab1 = await _context.Labs.FirstOrDefaultAsync(l => l.Id == 1);
            LabEntity lab2 = await _context.Labs.FirstOrDefaultAsync(l => l.Id == 2);
            LabEntity lab3 = await _context.Labs.FirstOrDefaultAsync(l => l.Id == 3);
            LabEntity lab4 = await _context.Labs.FirstOrDefaultAsync(l => l.Id == 4);
            LabEntity lab5 = await _context.Labs.FirstOrDefaultAsync(l => l.Id == 5);
            LabEntity lab6 = await _context.Labs.FirstOrDefaultAsync(l => l.Id == 6);

            await CheckSocialWorksAsync();
            await CheckUserAsync("17157729", "Luis", "Núñez", UserType.Admin, lab1, DateTime.Now, true, "luisalbertonu@gmail.com", "156 814 963");
            await CheckUserAsync("22222222", "Luis", "Núñez", UserType.User, lab1, DateTime.Now, true, "luis@yopmail.com", "156 814 963");
            await CheckUserAsync("33333333", "Pablo", "Lacuadri", UserType.User, lab2, DateTime.Now, true, "lacuadri@yopmail.com", "156 202 020");
            await CheckUserAsync("44444444", "Diego", "Maradona", UserType.User, lab3, DateTime.Now, true, "maradona@yopmail.com", "156 303 030");
            await CheckUserAsync("55555555", "Lionel", "Messi", UserType.User, lab4, DateTime.Now, true, "messi@yopmail.com", "156 404 040");
            await CheckUserAsync("55555555", "Mario", "Kempes", UserType.User, lab5, DateTime.Now, true, "kempes@yopmail.com", "156 404 040");
            await CheckUserAsync("55555555", "Gabriel", "Batistuta", UserType.User, lab6, DateTime.Now, true, "batistuta@yopmail.com", "156 404 040");

        }


        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<UserEntity> CheckUserAsync(
                string document,
                string firstName,
                string lastName,
                UserType userType,
                LabEntity lab,
                DateTime dischargeDate,
                bool active,
                string email,
                string phone
                )
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    Document = document,
                    FirstName = firstName,
                    LastName = lastName,
                    UserType = userType,
                    Lab = lab,
                    DischargeDate = dischargeDate,
                    Active = active,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    EmailConfirmed = true,

                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }



        private async Task CheckLabsAsync()
        {
            if (!_context.Labs.Any())
            {
                AddLab("Laboratorio 1", "Calle 1 N°1", "11-11111111-1", "1-111-111", "Juan 1111", "1111@gmail.com", DateTime.Now, true);
                AddLab("Laboratorio 2", "Calle 2 N°2", "22-222222-2", "2-222-222", "Juan 2222", "2222@gmail.com", DateTime.Now, true);
                AddLab("Laboratorio 3", "Calle 3 N°3", "33-333333-3", "3-333-333", "Juan 3333", "3333@gmail.com", DateTime.Now, true);
                AddLab("Laboratorio 4", "Calle 4 N°4", "44-444444-4", "4-444-444", "Juan 4444", "4444@gmail.com", DateTime.Now, true);
                AddLab("Laboratorio 5", "Calle 5 N°5", "55-555555-5", "5-555-555", "Juan 5555", "5555@gmail.com", DateTime.Now, true);
                AddLab("Laboratorio 6", "Calle 6 N°6", "66-666666-6", "6-666-666", "Juan 6666", "6666@gmail.com", DateTime.Now, true);
                await _context.SaveChangesAsync();
            }
        }

        private void AddLab(string name, string address, string cuit, string phone, string contact, string email, DateTime dischargeDate, bool active)
        {
            _context.Labs.Add(new LabEntity { Name = name, Address = address, CUIT = cuit, Phone = phone, Contact = contact, Email = email, DischargeDate = dischargeDate, Active = active });
        }

        private async Task CheckSocialWorksAsync()
        {
            if (!_context.SocialWorks.Any())
            {
                AddSocialWork("Osde", DateTime.Now, true);
                AddSocialWork("Apross", DateTime.Now, true);
                await _context.SaveChangesAsync();
            }
        }

        private void AddSocialWork(string name, DateTime dischargeDate, bool active)
        {
            _context.SocialWorks.Add(new SocialWorkEntity { Name = name, DischargeDate = dischargeDate, Active = active });
        }
    }
}