using System;

namespace FabaApp.Common.Models
{
    public class LabResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string CUIT { get; set; }

        public string Phone { get; set; }

        public string Contact { get; set; }

        public string Email { get; set; }

        public DateTime DischargeDate { get; set; }

        public bool Active { get; set; }

    }
}