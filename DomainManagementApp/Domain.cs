using System;

namespace DomainManagementApp
{
    public class Domain
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Owner { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}