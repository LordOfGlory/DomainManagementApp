//Author: Dan Kabagambe
//Version: 1.0.0.0
//Copyright © 2024 Dan Kabagambe

using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DomainManagementApp
{
    public partial class MainPage : ContentPage
    {
        private readonly DomainManager domainManager = new DomainManager();
        public ObservableCollection<DomainWithStatus> Domains { get; } = new ObservableCollection<DomainWithStatus>();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            InitializeData();
        }

        private void InitializeData()
        {
            DateTime now = DateTime.Now;
            domainManager.AddDomain(1, "active.com", "Alex Active", now.AddYears(-1)); // Active
            domainManager.AddDomain(2, "thirty.com", "Tim Thirty", now.AddDays(-360)); // 30 Days Left
            domainManager.AddDomain(3, "seven.net", "Sam Seven", now.AddDays(-363)); // 7 Days Left
            domainManager.AddDomain(4, "justexpired.org", "James Just", now.AddDays(-366)); // Just Expired
            domainManager.AddDomain(5, "inredemption.io", "Rachel Rays", now.AddDays(-368)); // In Redemption
            domainManager.AddDomain(6, "newactive.site", "Ned New", now.AddDays(-300)); // Another Active
            domainManager.AddDomain(7, "another30.com", "Alice Another", now.AddDays(-330)); // Another 30 Days Left
            domainManager.AddDomain(8, "anazaone.net", "Dan Set", now.AddDays(-353)); // Another 7 Days Left
            domainManager.AddDomain(9, "longexpired.org", "Larry Long", now.AddDays(-400)); // Long Expired
            domainManager.AddDomain(10, "longredemption.io", "Lila Why", now.AddDays(-370)); // Long Redemption

            RefreshDomains();
        }

        private void RefreshDomains()
        {
            Domains.Clear();
            foreach (var domain in domainManager.GetAllDomains())
            {
                var daysUntilExpiry = (domain.ExpiryDate - DateTime.Now).TotalDays;
                var daysSinceExpiry = (DateTime.Now - domain.ExpiryDate).TotalDays;

                Domains.Add(new DomainWithStatus
                {
                    Id = domain.Id,
                    Name = domain.Name,
                    Owner = domain.Owner,
                    StartDate = domain.StartDate,
                    ExpiryDate = domain.ExpiryDate,
                    ThirtyDays = daysUntilExpiry <= 30 && daysUntilExpiry > 7 ? "Yes" : "No",
                    SevenDays = daysUntilExpiry <= 7 && daysUntilExpiry > 0 ? "Yes" : "No",
                    InRedemption = daysSinceExpiry > 0 && daysSinceExpiry <= 2 ? "Yes" : "No",
                  
                });
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                domainsCollectionView.ItemsSource = Domains;
            }
            else
            {
                var filteredDomains = Domains.Where(d =>
                    d.Name.Contains(e.NewTextValue, StringComparison.OrdinalIgnoreCase) ||
                    d.Owner.Contains(e.NewTextValue, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                domainsCollectionView.ItemsSource = filteredDomains;
            }
        }

        private async void OnRefreshClicked(object sender, EventArgs e)
        {
            RefreshDomains();
            await DisplayAlert("Success", "Domains refreshed.", "OK");
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Exit", "Do you want to exit the application?", "Yes", "No");
            if (answer)
            {
                Environment.Exit(0);
            }
        }
    }

    public class DomainWithStatus
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Owner { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string ThirtyDays { get; set; } = string.Empty;

        public string SevenDays { get; set; } = string.Empty;

        public string InRedemption { get; set; } = string.Empty;
    }

    
}
