using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;

namespace ProductsCRUD.ViewModels
{
    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
    }

    public class Vendor
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }
        public string StartContractDate { get; set; }
        public int VendorCategoryId { get; set; }
    }

    public class VendorCategory
    {
        public int VendorCategoryId { get; set; }
        public string Name { get; set; }
    }

    public class VendorsMainPageViewModel : ViewModelBase
    {
        public List<City> Cities { get; set; }
        public List<Vendor> Vendors { get; set; }
        public List<VendorCategory> VendorCategories { get; set; }

        public VendorsMainPageViewModel()
        {
            LoadDataGridItems();
        }

        public void LoadDataGridItems()
        {
            Cities = new List<City>
            {
                new City {CityId = 1, Name = "Recife"},
                new City {CityId = 2, Name = "Jaboatão dos Guararapes"},
                new City {CityId = 3, Name = "Caruaru"},
                new City {CityId = 4, Name = "Petrolina"},
                new City {CityId = 5, Name = "Olinda"},
                new City {CityId = 6, Name = "Garanhuns"},
                new City {CityId = 7, Name = "Cabo de Sto Agostinho"},
            };

            VendorCategories = new List<VendorCategory>
            {
                new VendorCategory {VendorCategoryId = 1, Name = "Alimentos não perecíveis"},
                new VendorCategory {VendorCategoryId = 2, Name = "Hortifruti"},
                new VendorCategory {VendorCategoryId = 3, Name = "Frigorífico"},
            };

            Vendors = new List<Vendor>
            {
                new Vendor
                {
                    Id = 1,
                    CityId = 3,
                    VendorCategoryId = 1,
                    Name = "ABC alimentos",
                    StartContractDate = new DateTime(2023, 9, 15, 10, 30, 0).ToString()
                },
                new Vendor
                {
                    Id = 2,
                    CityId = 4,
                    VendorCategoryId = 3,
                    Name = "DEF alimentos",
                    StartContractDate = new DateTime(2023, 9, 14, 10, 45, 0).ToString()
                },
                new Vendor
                {
                    Id = 3,
                    CityId = 5,
                    VendorCategoryId = 1,
                    Name = "GHI alimentos",
                    StartContractDate = new DateTime(2023, 9, 10, 15, 30, 0).ToString()
                },
                new Vendor
                {
                    Id = 4,
                    CityId = 1,
                    VendorCategoryId = 3,
                    Name = "JKL alimentos",
                    StartContractDate = new DateTime(2022, 3, 05, 08, 30, 0).ToString()
                },
                new Vendor
                {
                    Id = 5,
                    CityId = 1,
                    VendorCategoryId = 2,
                    Name = "MNO alimentos",
                    StartContractDate = new DateTime(2022, 4, 24, 10, 40, 0).ToString()
                },
                new Vendor
                {
                    Id = 6,
                    CityId = 2,
                    VendorCategoryId = 1,
                    Name = "PQR alimentos",
                    StartContractDate = new DateTime(2023, 9, 15, 10, 11, 0).ToString()
                },
            };
        }
    }
}
