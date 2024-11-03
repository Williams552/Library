﻿using Microsoft.AspNetCore.SignalR;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfLibrary.ViewModel
{
    public class LibraryViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;
        public ObservableCollection<Category> Cate { get; set; } = new ObservableCollection<Category>();

        public ObservableCollection<Publisher> Pub { get; set; } = new ObservableCollection<Publisher>();

        public ObservableCollection<Fee> fee { get; set; } = new ObservableCollection<Fee>();

        public ObservableCollection<Models.Staff> StaffMembers { get; set; } = new ObservableCollection<Models.Staff>();
        public ObservableCollection<Supplier> suppliers { get; set; } = new ObservableCollection<Supplier>();


        public Models.Staff SelectedStaff { get; set; } = new Models.Staff();
        public Supplier SelectedSupplier { get; set; } = new Supplier();
        public Category SelectedCate { get; set; }
        public Publisher SelectedPub { get; set; }
        public Fee SelectedFee { get; set; }

        public ICommand AddStaffCommand { get; }
        public ICommand UpdateStaffCommand { get; }
        public ICommand DeleteStaffCommand { get; }

        public ICommand AddSupplierCommand { get; }
        public ICommand UpdateSupplierCommand { get; }


        public LibraryViewModel()
        {
            _httpClient = new HttpClient(); // Chỉ khởi tạo một lần
            _httpClient.BaseAddress = new Uri("https://localhost:7143/api/"); // Đặt URL cơ sở cho tất cả các API
            LoadCategoriesAsync();
            LoadFeeAsync();
            LoadPublisherAsync();
            LoadStaffAsync();
            LoadSupplierAsync();

            //AttachJwtTokenToClient();

            AddStaffCommand = new RelayCommand(async (staff) => await AddStaffAsync((Models.Staff)staff));
            UpdateStaffCommand = new RelayCommand(async (staff) => await UpdateStaffAsync((Models.Staff)staff), CanUpdate);
            DeleteStaffCommand = new RelayCommand(async (staff) => await DeleteStaffAsync(((Models.Staff)staff).StaffId), CanDelete);

            AddSupplierCommand = new RelayCommand(async (supplier) => await AddSupplierAsync((Supplier)supplier));
            UpdateSupplierCommand = new RelayCommand(async (supplier) => await UpdateSupplierAsync((Supplier)supplier));
        }

        private bool CanDelete(object? arg)
        {
            return SelectedStaff != null && SelectedStaff.StaffId > 0;
        }

        private bool CanUpdate(object? arg)
        {
            return SelectedStaff != null && SelectedStaff.StaffId > 0;
        }

        public async Task LoadSupplierAsync()
        {
            var supplierList = await _httpClient.GetFromJsonAsync<List<Supplier>>("Supplier");
            if (supplierList != null)
            {
                suppliers.Clear();
                foreach (var supplier in supplierList)
                {
                    suppliers.Add(supplier);
                }
            }
        }

        public async Task LoadStaffAsync()
        {
            var staffList = await _httpClient.GetFromJsonAsync<List<Models.Staff>>("Staff");
            if (staffList != null)
            {
                StaffMembers.Clear();
                foreach (var staff in staffList)
                {
                    StaffMembers.Add(staff);
                }
            }
        }
        public async Task LoadCategoriesAsync()
        {
            var categories = await _httpClient.GetFromJsonAsync<List<Category>>("Category");
            if (categories != null)
            {
                Cate.Clear();
                foreach (var category in categories)
                {
                    Cate.Add(category);
                }
            }
        }



        public async Task LoadFeeAsync()
        {
            var fees = await _httpClient.GetFromJsonAsync<List<Fee>>("Fee");
            if (fees != null)
            {
                fee.Clear();
                foreach (var item in fees)
                {
                    fee.Add(item);
                }
            }
        }
        
        public bool checkname(string name)
        {
            
            return Cate.Any(c => c.CategoryCode.Equals(name, StringComparison.OrdinalIgnoreCase));
        }


        public async Task AddCategoryAsync(Category category)
        {
            
            if (checkname(category.CategoryCode))
            {
                
                MessageBox.Show("Tên danh mục đã tồn tại. Vui lòng nhập tên khác.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; 
            }

            var response = await _httpClient.PostAsJsonAsync("Category", category);
            if (response.IsSuccessStatusCode)
            {
                Cate.Add(category); 
            }
            else
            {
                
                var errorMessage = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Lỗi khi thêm danh mục: {errorMessage}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public async Task UpdateCategoryAsync(Category category)
        {
            var response = await _httpClient.PutAsJsonAsync($"Category/{category.CategoryId}", category);
            if (response.IsSuccessStatusCode)
            {
                var index = Cate.IndexOf(Cate.First(c => c.CategoryId == category.CategoryId));
                if (index >= 0) Cate[index] = category;

                Cate.RemoveAt(index);
                Cate.Insert(index, category);
            }
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var response = await _httpClient.DeleteAsync($"Category/{categoryId}");
            if (response.IsSuccessStatusCode)
            {
                var category = Cate.FirstOrDefault(c => c.CategoryId == categoryId);
                if (category != null) Cate.Remove(category);
            }
        }

        //fee
        public async Task AddFeeAsync(Fee fe)
        {
            var response = await _httpClient.PostAsJsonAsync("Fee", fe);
            
            if (response.IsSuccessStatusCode)
            {
                fee.Add(fe);
            }
        }

        public async Task UpdateFeeAsync(Fee fe)
        {
            var response = await _httpClient.PutAsJsonAsync($"Fee/{fe.FeeId}", fe);
            if (response.IsSuccessStatusCode)
            {
                var index = fee.IndexOf(fee.First(c => c.FeeId == fe.FeeId));
                if (index >= 0) fee[index] = fe;

                fee.RemoveAt(index);
                fee.Insert(index, fe);
            }
        }

        public async Task DeleteFeeAsync(int feeid)
        {
            var response = await _httpClient.DeleteAsync($"Fee/{feeid}");
            if (response.IsSuccessStatusCode)
            {
                var category = fee.FirstOrDefault(c => c.FeeId == feeid);
                if (category != null) fee.Remove(category);
            }
        }

        //load Publisher
        public async Task LoadPublisherAsync()
        {
            var publisher = await _httpClient.GetFromJsonAsync<List<Publisher>>("Publisher");
            if (publisher != null)
            {
                Pub.Clear();
                foreach (var pub in publisher)
                {
                    Pub.Add(pub);
                }
            }
        }

        public async Task AddPub(Publisher _pub)
        {


            var response = await _httpClient.PostAsJsonAsync("Publisher", _pub);
            if (response.IsSuccessStatusCode)
            {
                Pub.Add(_pub);
            }
            else
            {

                var errorMessage = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Lỗi khi thêm danh mục: {errorMessage}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public async Task UpdatePub(Publisher _pub)
        {
            var response = await _httpClient.PutAsJsonAsync($"Publisher/{_pub.PublisherId}", _pub);
            if (response.IsSuccessStatusCode)
            {
                var index = Pub.IndexOf(Pub.First(c => c.PublisherId == _pub.PublisherId));
                if (index >= 0) Pub[index] = _pub;

                Pub.RemoveAt(index);
                Pub.Insert(index, _pub);
            }
        }

        public async Task DeletePub(int publisherId)
        {
            var response = await _httpClient.DeleteAsync($"Publisher/{publisherId}");
            if (response.IsSuccessStatusCode)
            {
                var pub = Pub.FirstOrDefault(c => c.PublisherId == publisherId);
                if (pub != null) Pub.Remove(pub);
            }
        }


        public async Task AddStaffAsync(Models.Staff staff)
        {
            var response = await _httpClient.PostAsJsonAsync("Staff", staff);
            if (response.IsSuccessStatusCode)
            {
                StaffMembers.Add(staff);
                SelectedStaff = new Models.Staff();
            }
        }

        public async Task UpdateStaffAsync(Models.Staff staff)
        {
            if (staff != null)
            {
                var response = await _httpClient.PutAsJsonAsync($"Staff/{staff.StaffId}", staff);
                if (response.IsSuccessStatusCode)
                {
                    var index = StaffMembers.IndexOf(StaffMembers.First(s => s.StaffId == staff.StaffId));
                    if (index >= 0)
                    {
                        StaffMembers[index] = staff;
                    }
                }
            }
        }

        public async Task DeleteStaffAsync(int staffId)
        {
            var response = await _httpClient.DeleteAsync($"Staff/{staffId}");
            if (response.IsSuccessStatusCode)
            {
                var staff = StaffMembers.FirstOrDefault(s => s.StaffId == staffId);
                if (staff != null)
                {
                    StaffMembers.Remove(staff);
                    SelectedStaff = new Models.Staff();
                }
            }
        }



        public async Task AddSupplierAsync(Supplier supplier)
        {
            var response = await _httpClient.PostAsJsonAsync("Supplier", supplier);
            if (response.IsSuccessStatusCode)
            {
                suppliers.Add(supplier);
                SelectedSupplier = new Supplier();
            }
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            if (supplier != null)
            {
                var response = await _httpClient.PutAsJsonAsync($"Supplier/{supplier.SupplierId}", supplier);
                if (response.IsSuccessStatusCode)
                {
                    var index = suppliers.IndexOf(suppliers.First(s => s.SupplierId == supplier.SupplierId));
                    if (index >= 0)
                    {
                        suppliers[index] = supplier;
                    }
                }
            }
        }

        public async Task DeleteSupplierAsync(int supplierid)
        {
            var response = await _httpClient.DeleteAsync($"Supplier/{supplierid}");
            if (response.IsSuccessStatusCode)
            {
                var supplier = suppliers.FirstOrDefault(s => s.SupplierId == supplierid);
                if (supplier != null)
                {
                    suppliers.Remove(supplier);
                    SelectedSupplier = new Supplier();
                }
            }
        }



    }
}