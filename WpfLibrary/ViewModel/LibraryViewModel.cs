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
        public ObservableCollection<Author> authors { get; set; } = new ObservableCollection<Author>();
        public ObservableCollection<Fee> fee { get; set; } = new ObservableCollection<Fee>();
        public ObservableCollection<Models.Staff> StaffMembers { get; set; } = new ObservableCollection<Models.Staff>();
        public ObservableCollection<Supplier> suppliers { get; set; } = new ObservableCollection<Supplier>();

        public ObservableCollection<BookGroup> bookGroups { get; set; } = new ObservableCollection<BookGroup>();

        public ObservableCollection<Bookshelf> bookShelfs { get; set; } = new ObservableCollection<Bookshelf>();


        public ObservableCollection<Loan> loans { get; set; } = new ObservableCollection<Loan>();


        public ObservableCollection<Book> book { get; set; } = new ObservableCollection<Book>();
        public ObservableCollection<Member> members { get; set; } = new ObservableCollection<Member>();


        public Member SelectedMember { get; set; }
        public Book SelectedBook { get; set; }

        public Models.Staff SelectedStaff { get; set; } = new Models.Staff();
        public Supplier SelectedSupplier { get; set; } = new Supplier();
        public Category SelectedCate { get; set; }
        public Publisher SelectedPub { get; set; }
        public Fee SelectedFee { get; set; }

        public BookGroup SelectedBookGroup { get; set; }

        public Bookshelf SelectedBookshelf { get; set; }

        public Loan SelectedLoan { get; set; }
        public Author SelectedAuthor { get; set; }

        public ICommand AddStaffCommand { get; }
        public ICommand UpdateStaffCommand { get; }
        public ICommand DeleteStaffCommand { get; }

        public ICommand AddSupplierCommand { get; }
        public ICommand UpdateSupplierCommand { get; }


        public LibraryViewModel()
        {
            _httpClient = new HttpClient(); // Chỉ khởi tạo một lần
            _httpClient.BaseAddress = new Uri("http://localhost:5139/api/"); // Đặt URL cơ sở cho tất cả các API
            LoadCategoriesAsync();
            LoadFeeAsync();
            LoadPublisherAsync();
            LoadStaffAsync();
            LoadAuthorAsync();
            LoadSupplierAsync();
            LoadBookGroupsAsync();
            LoadBookshelfAsync();
            LoadLoanAsync();
            LoadBookAsync();
            LoadMember();
            //AttachJwtTokenToClient();

            AddStaffCommand = new RelayCommand(async (staff) => await AddStaffAsync((Models.Staff)staff));
            UpdateStaffCommand = new RelayCommand(async (staff) => await UpdateStaffAsync((Models.Staff)staff), CanUpdate);
            DeleteStaffCommand = new RelayCommand(async (staff) => await DeleteStaffAsync(((Models.Staff)staff).StaffId), CanDelete);

            AddSupplierCommand = new RelayCommand(async (supplier) => await AddSupplierAsync((Supplier)supplier));
            UpdateSupplierCommand = new RelayCommand(async (supplier) => await UpdateSupplierAsync((Supplier)supplier));
        }

        public async Task LoadMember()
        {
            var memberList = await _httpClient.GetFromJsonAsync<List<Member>>("Member");
            if(memberList != null)
            {
                members.Clear();
                foreach (var member in memberList)
                {
                    members.Add(member);
                }    
            }    
        }

        public async Task LoadLoanAsync()
        {
            var loanList = await _httpClient.GetFromJsonAsync<List<Loan>>("Loan");
            if (loanList != null)
            {
                loans.Clear();
                foreach (var loan in loanList)
                {
                    loans.Add(loan);
                }
            }
        }
        public async Task LoadAuthorAsync()
        {
            var authorList = await _httpClient.GetFromJsonAsync<List<Author>>("Authors");
            if (authorList != null)
            {
                authors.Clear();
                foreach (var author in authorList)
                {
                    authors.Add(author);
                }
            }
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

        public async Task LoadBookGroupsAsync()
        {
            var books = await _httpClient.GetFromJsonAsync<List<BookGroup>>("BookGroup");
            if (books != null)
            {
                bookGroups.Clear();
                foreach (var item in books)
                {
                    bookGroups.Add(item);
                }
            }
        }

        public async Task LoadBookshelfAsync()
        {
            var loadbookshelf = await _httpClient.GetFromJsonAsync<List<Bookshelf>>("Bookshelf");
            if (loadbookshelf != null)
            {
                bookShelfs.Clear();
                foreach (var item in loadbookshelf)
                {
                    bookShelfs.Add(item);
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

        public async Task LoadBookAsync()
        {
            var viewbook = await _httpClient.GetFromJsonAsync<List<Book>>("Book");
            if (viewbook != null)
            {
                book.Clear();
                foreach (var item in viewbook)
                {
                    book.Add(item);
                }
            }
        }

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



        //--------------------------------------------------Book-------------------------------------------------



        public async Task AddBookAsync(Book newBook)
        {
            var response = await _httpClient.PostAsJsonAsync("Book", newBook);
            if (response.IsSuccessStatusCode)
            {
                var addedBook = await response.Content.ReadFromJsonAsync<Book>();
                if (addedBook != null) book.Add(addedBook); // Add to local collection
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm sách mới.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public async Task UpdateBookAsync(Book updatedBook)
        {
            var response = await _httpClient.PutAsJsonAsync($"Book/{updatedBook.BookId}", updatedBook);
            if (response.IsSuccessStatusCode)
            {
                var index = book.IndexOf(book.FirstOrDefault(b => b.BookId == updatedBook.BookId));
                if (index >= 0) book[index] = updatedBook; // Update local collection
            }
            else
            {
                MessageBox.Show("Lỗi khi cập nhật sách.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var respone = await _httpClient.DeleteAsync($"Book/{bookId}");
            if (respone.IsSuccessStatusCode)
            {
                var dlbook = book.FirstOrDefault(c => c.BookId == bookId);
                if (dlbook != null) book.Remove(dlbook);
            }
        }

        public bool checkname(string name)
        {
            
            return Cate.Any(c => c.CategoryCode.Equals(name, StringComparison.OrdinalIgnoreCase));
        }



        //--------------------------------------------------Cate-------------------------------------------------


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



        //--------------------------------------------------Fee-------------------------------------------------



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
        



        //--------------------------------------------------Pub-------------------------------------------------


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



        //--------------------------------------------------Staff-------------------------------------------------


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



        //--------------------------------------------------Supplier-------------------------------------------------


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



        //--------------------------------------------------BookGroup-------------------------------------------------


        public async Task DeleteBookgroupAsync(int bookGroupId)
        {
            var respone = await _httpClient.DeleteAsync($"BookGroup/{bookGroupId}");
            if (respone.IsSuccessStatusCode)
            {
                var bookgroup = bookGroups.FirstOrDefault(c => c.GroupId == bookGroupId);
                if (bookgroup != null) bookGroups.Remove(bookgroup);
            }
        }
        public async Task UpdateBookgroupAsync(BookGroup bookgroup)
        {
            var respone = await _httpClient.PutAsJsonAsync($"BookGroup/{bookgroup.GroupId}", bookgroup);
            if (respone.IsSuccessStatusCode)
            {
                var index = bookGroups.IndexOf(bookGroups.First(c => c.GroupId == bookgroup.GroupId));
                if (index >= 0) bookGroups[index] = bookgroup;

                bookGroups.RemoveAt(index);
                bookGroups.Insert(index, bookgroup);

            }
        }
        public async Task AddBookGroupAsync(BookGroup bookGroup)
        {
            var response = await _httpClient.PostAsJsonAsync("BookGroup", bookGroup);
            if (response.IsSuccessStatusCode)
            {
                bookGroups.Add(bookGroup);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Lỗi khi thêm nhóm sách: {errorMessage}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        //--------------------------------------------------Bookshelf-------------------------------------------------



        public async Task AddBookshelfAsync(Bookshelf bookshelf)
        {
            var response = await _httpClient.PostAsJsonAsync("Bookshelf", bookshelf);
            if (response.IsSuccessStatusCode)
            {
                bookShelfs.Add(bookshelf);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Lỗi khi thêm bookshelf: {errorMessage}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task UpdateBookshelfAsync(Bookshelf bookshelf)
        {
            var respone = await _httpClient.PutAsJsonAsync($"Bookshelf/{bookshelf.ShelfId}", bookshelf);
            if (respone.IsSuccessStatusCode)
            {
                var index = bookShelfs.IndexOf(bookShelfs.First(c => c.ShelfId == bookshelf.ShelfId));
                if (index >= 0) bookShelfs[index] = bookshelf;

                bookShelfs.RemoveAt(index);
                bookShelfs.Insert(index, bookshelf);

            }
        }

        public async Task DeleteBookshelfAsync(int bookshelfId)
        {
            var respone = await _httpClient.DeleteAsync($"Bookshelf/{bookshelfId}");
            if (respone.IsSuccessStatusCode)
            {
                var bookshelf = bookShelfs.FirstOrDefault(c => c.ShelfId == bookshelfId);
                if (bookshelf != null) bookShelfs.Remove(bookshelf);
            }
        }


        //--------------------------------------------------Loan-------------------------------------------------


        public async Task AddLoanAsync(Loan loan)
        {
            var response = await _httpClient.PostAsJsonAsync("Loan", loan);
            if (response.IsSuccessStatusCode)
            {
                loans.Add(loan);
                SelectedLoan = new Loan();
            }
        }

        public async Task UpdateLoanAsync(Loan loan)
        {
            if (loan != null)
            {
                var response = await _httpClient.PutAsJsonAsync($"Loan/{loan.LoanId}", loan);
                if (response.IsSuccessStatusCode)
                {
                    var index = loans.IndexOf(loans.First(s => s.LoanId == loan.LoanId));
                    if (index >= 0)
                    {
                        loans[index] = loan;
                    }
                }
            }
        }


        //--------------------------------------------------Author-------------------------------------------------


        public async Task AddAuthorAsync(Author author)
        {
            var response = await _httpClient.PostAsJsonAsync("Authors", author);
            if (response.IsSuccessStatusCode)
            {
                authors.Add(author);
                SelectedAuthor = new Author();
            }
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            if (author != null)
            {
                var response = await _httpClient.PutAsJsonAsync($"Authors/{author.AuthorId}", author);
                if (response.IsSuccessStatusCode)
                {
                    var index = authors.IndexOf(authors.First(s => s.AuthorId == author.AuthorId));
                    if (index >= 0)
                    {
                        authors[index] = author;
                    }
                }
            }
        }

        public async Task DeleteAuthorAsync(int authorid)
        {
            var response = await _httpClient.DeleteAsync($"Authors/{authorid}");
            if (response.IsSuccessStatusCode)
            {
                var author = authors.FirstOrDefault(s => s.AuthorId == authorid);
                if (author != null)
                {
                    authors.Remove(author);
                    SelectedAuthor = new Author();
                }
            }
        }


        //--------------------------------------------------Member-------------------------------------------------


        public async Task AddMemberAsync(Member member)
        {
            var response = await _httpClient.PostAsJsonAsync("Member", member);
            if (response.IsSuccessStatusCode)
            {
                members.Add(member);
                SelectedMember = new Member();
            }
        }

        public async Task UpdateMemberAsync(Member member)
        {
            if (member != null)
            {
                var response = await _httpClient.PutAsJsonAsync($"Member/{member.MemberId}", member);
                if (response.IsSuccessStatusCode)
                {
                    var index = members.IndexOf(members.First(s => s.MemberId == member.MemberId));
                    if (index >= 0)
                    {
                        members[index] = member;
                    }
                }
            }
        }

        public async Task DeleteMemberAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Member/{id}");
            if (response.IsSuccessStatusCode)
            {
                var member = members.FirstOrDefault(s => s.MemberId == id);
                if (member != null)
                {
                    members.Remove(member);
                    SelectedMember = new Member();
                }
            }
        }

    }
}
