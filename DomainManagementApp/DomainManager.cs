//Author: Dan Kabagambe
//Version: 1.0.0.0
//Copyright © 2024 Dan Kabagambe

using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainManagementApp
{
    public class DomainManager
    {
        private readonly List<Domain> domains = new List<Domain>();

        public void AddDomain(int id, string name, string owner, DateTime startDate)
        {
            domains.Add(new Domain
            {
                Id = id,
                Name = name,
                Owner = owner,
                StartDate = startDate,
                ExpiryDate = startDate.AddYears(1)
            });
        }

        public IEnumerable<Domain> GetAllDomains() => domains.AsReadOnly();

        public List<string> Get30DayDomains()
        {
            var sevenDayDomains = Get7DayDomains();
            return domains.Where(d => d.ExpiryDate > DateTime.Now.AddDays(7) && d.ExpiryDate <= DateTime.Now.AddDays(30))
                          .Select(d => d.Name)
                          .Except(sevenDayDomains)
                          .ToList();
        }

        public List<string> Get7DayDomains()
        {
            return domains.Where(d => d.ExpiryDate > DateTime.Now && d.ExpiryDate <= DateTime.Now.AddDays(7))
                          .Select(d => d.Name)
                          .ToList();
        }

        public List<string> GetExpiredDomains()
        {
            return domains.Where(d => d.ExpiryDate <= DateTime.Now)
                          .Select(d => d.Name)
                          .ToList();
        }

        public List<string> GetRedemptionDomains()
        {
            return domains.Where(d => d.ExpiryDate <= DateTime.Now.AddDays(-2) && d.ExpiryDate > DateTime.Now.AddDays(-4))
                          .Select(d => d.Name)
                          .ToList();
        }
    }
}
