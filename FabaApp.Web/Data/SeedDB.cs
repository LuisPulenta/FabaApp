using FabaApp.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FabaApp.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {



            await _context.Database.EnsureCreatedAsync();
            
            await CheckLabsAsync();


            

            await CheckSocialWorksAsync();
            

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