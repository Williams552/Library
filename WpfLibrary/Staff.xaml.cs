using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using WpfLibrary.ViewModel;
using static MaterialDesignThemes.Wpf.Theme;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace WpfLibrary
{
    /// <summary>
    /// Interaction logic for Staff.xaml
    /// </summary>
    public partial class Staff : Window
    {
        private LibraryViewModel _LibraryViewModel;

        public Staff()
        {
            InitializeComponent();
            _LibraryViewModel = new LibraryViewModel();

            DataContext = _LibraryViewModel;

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void myDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myDataGrid.SelectedItem is Models.Staff selectedStaff)
            {
                // Cập nhật các trường nhập liệu với thông tin của danh mục đã chọn
                txtStaffid.Text = selectedStaff.StaffId.ToString();
                txtUsername.Text = selectedStaff.Username;
                txtPassword.Text = selectedStaff.Password;
                txtFullname.Text = selectedStaff.FullName;
                txtEmail.Text = selectedStaff.Email;
                txtRole.Text = selectedStaff.Role;
            }
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStaffid.Text) && !string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtFullname.Text) && !string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtRole.Text))
            {


                var newStaff = new Models.Staff
                {

                    StaffId = Int32.Parse(txtStaffid.Text),
                    Username = txtUsername.Text,
                    Password = txtPassword.Text,
                    FullName = txtFullname.Text,
                    Email = txtEmail.Text,
                    Role = txtRole.Text
                };

                _LibraryViewModel.AddStaffAsync(newStaff);



                ClearFieldsStaff();

            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin danh mục.");
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (myDataGrid.SelectedItem is Models.Staff selectedStaff)
            {
                //selectedStaff.StaffId = Int32.Parse(txtStaffid.Text);
                selectedStaff.Username = txtUsername.Text;
                selectedStaff.Password = txtPassword.Text;
                selectedStaff.FullName = txtFullname.Text;
                selectedStaff.Email = txtEmail.Text;
                selectedStaff.Role = txtRole.Text;



                _LibraryViewModel.UpdateStaffAsync(selectedStaff);
                ClearFieldsStaff();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn danh mục cần cập nhật.");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (myDataGrid.SelectedItem is Models.Staff selectedStaff)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa Staff này không?",
                                              "Xác nhận xóa",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _LibraryViewModel.DeleteStaffAsync(selectedStaff.StaffId);



                    ClearFieldsStaff();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa.");
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            txtStaffid.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtFullname.Clear();
            txtEmail.Clear();
            txtRole.Clear();
        }

        private void ClearFieldsStaff()
        {
            txtStaffid.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtFullname.Clear();
            txtEmail.Clear();
            txtRole.Clear();
        }

        private void ClearFieldsSupplier()
        {
            txtSupplierName.Clear();
            txtContactInfo.Clear();

        }

        private void ClearFieldsLoan()
        {
            txtUserId.Clear();
            txtCopyId.Clear();
            dpLoanDate.SelectedDate = null;
            dpReturnDate.SelectedDate = null;
            dpDueDate.SelectedDate = null;
            txtFine.Clear();
            txtBorrowFee.Clear();
            txtStatus.Clear();

        }

        private void myDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (myDataGridSupplier.SelectedItem is Supplier selectedSupplier)
            {
                // Cập nhật các trường nhập liệu với thông tin của danh mục đã chọn
                txtSupplierName.Text = selectedSupplier.Name;
                txtContactInfo.Text = selectedSupplier.ContactInfo;
            }
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSupplierName.Text) && !string.IsNullOrEmpty(txtContactInfo.Text))
            {


                var newSupplier = new Supplier
                {

                    Name = txtSupplierName.Text,
                    ContactInfo = txtContactInfo.Text
                };

                _LibraryViewModel.AddSupplierAsync(newSupplier);



                ClearFieldsSupplier();

            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin danh mục.");
            }
        }

        private void UpdateSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (myDataGridSupplier.SelectedItem is Supplier selectedSupplier)
            {
                //selectedStaff.StaffId = Int32.Parse(txtStaffid.Text);
                selectedSupplier.Name = txtSupplierName.Text;
                selectedSupplier.ContactInfo = txtContactInfo.Text;




                _LibraryViewModel.UpdateSupplierAsync(selectedSupplier);
                ClearFieldsSupplier();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn danh mục cần cập nhật.");
            }
        }

        private void DeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (myDataGridSupplier.SelectedItem is Supplier selectedSupplier)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa Supplier này không?",
                                              "Xác nhận xóa",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _LibraryViewModel.DeleteSupplierAsync(selectedSupplier.SupplierId);



                    ClearFieldsSupplier();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa.");
            }
        }

        private void ClearSupplier_Click(object sender, RoutedEventArgs e)
        {
            txtSupplierName.Clear();
            txtContactInfo.Clear();
        }

        private void mydata_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mydata.SelectedItem is Category selectedCategory)
            {
                // Cập nhật các trường nhập liệu với thông tin của danh mục đã chọn
                txtCode.Text = selectedCategory.CategoryCode;
                txtName.Text = selectedCategory.Name;
                txtDiscription.Text = selectedCategory.Description;
            }
        }

        private void Add_button(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text) && !string.IsNullOrEmpty(txtName.Text))
            {


                var newCategory = new Category
                {

                    CategoryCode = txtCode.Text,
                    Name = txtName.Text,
                    Description = txtDiscription.Text
                };

                _LibraryViewModel.AddCategoryAsync(newCategory);



                ClearFields();

            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin danh mục.");
            }
        }

        private void Update_Button(object sender, RoutedEventArgs e)
        {
            if (mydata.SelectedItem is Category selectedCategory)
            {
                selectedCategory.CategoryCode = txtCode.Text;
                selectedCategory.Name = txtName.Text;
                selectedCategory.Description = txtDiscription.Text;



                _LibraryViewModel.UpdateCategoryAsync(selectedCategory);
                ClearFields();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn danh mục cần cập nhật.");
            }
        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            if (mydata.SelectedItem is Category selectedCategory)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa danh mục này không?",
                                              "Xác nhận xóa",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _LibraryViewModel.DeleteCategoryAsync(selectedCategory.CategoryId);



                    ClearFields();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa.");
            }
        }


        private void mydata1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mydata1.SelectedItem is Fee select)
            {
                txtName1.Text = select.Name;
                txtAmount1.Text = select.Amount.ToString();
                txtFeeType1.Text = select.FeeType.ToString();
                txtDescription1.Text = select.Description;
            }
        }

        private void AddFee_button(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName1.Text) && !string.IsNullOrEmpty(txtFeeType1.Text))
            {


                var newFee = new Fee
                {

                    Name = txtName1.Text,
                    Amount = Int32.Parse(txtAmount1.Text),
                    Description = txtDescription1.Text,
                    FeeType = txtFeeType1.Text
                };

                _LibraryViewModel.AddFeeAsync(newFee);



                ClearFields();



            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin fee.");
            }

        }

        private void UpdateFee_Button(object sender, RoutedEventArgs e)
        {
            if (mydata1.SelectedItem is Fee selectedFee)
            {
                selectedFee.Name = txtName1.Text;
                selectedFee.FeeType = txtFeeType1.Text;
                selectedFee.Description = txtDescription1.Text;
                selectedFee.Amount = Int32.Parse(txtAmount1.Text);


                _LibraryViewModel.UpdateFeeAsync(selectedFee);
                ClearFields();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn Publisher cần cập nhật.");
            }
        }

        private void DeleteFee_Button(object sender, RoutedEventArgs e)
        {
            if (mydata1.SelectedItem is Fee selectedFee)
            {

                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa fee này không?",
                                               "Xác nhận xóa",
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Warning);


                if (result == MessageBoxResult.Yes)
                {
                    _LibraryViewModel.DeleteFeeAsync(selectedFee.FeeId);



                    ClearFields();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn fee cần xóa.");
            }
        }

        private void ClearFields()
        {
            txtCode.Clear();
            txtName.Clear();
            txtDiscription.Clear();
        }

        private void mydata2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mydata2.SelectedItem is Publisher select)
            {
                PubName.Text = select.Name;
                PubAddress.Text = select.Address.ToString();
            }

        }

        private void Add_Pub(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PubName.Text) && !string.IsNullOrEmpty(PubAddress.Text))
            {


                var newPub = new Publisher
                {

                    Name = PubName.Text,
                    Address = PubAddress.Text

                };

                _LibraryViewModel.AddPub(newPub);



                ClearFields();



            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin publisher.");
            }

        }

        private void Update_Pub(object sender, RoutedEventArgs e)
        {
            if (mydata2.SelectedItem is Publisher selectedPub)
            {
                selectedPub.Name = PubName.Text;
                selectedPub.Address = PubAddress.Text;



                _LibraryViewModel.UpdatePub(selectedPub);
                ClearFields();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn fee cần cập nhật.");
            }

        }

        private void Delete_Pub(object sender, RoutedEventArgs e)
        {
            if (mydata2.SelectedItem is Publisher selectedPub)
            {

                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa fee này không?",
                                               "Xác nhận xóa",
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Warning);


                if (result == MessageBoxResult.Yes)
                {
                    _LibraryViewModel.DeletePub(selectedPub.PublisherId);



                    ClearFields();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn fee cần xóa.");
            }

        }

        private void BookGroupData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookGroupData.SelectedItem is BookGroup select)
            {
                BookName.Text = select.Name;
                BookDes.Text = select.Description;
            }
        }

        private void Update_BookGroup(object sender, RoutedEventArgs e)
        {
            if (BookGroupData.SelectedItem is BookGroup select)
            {
                select.Name = BookName.Text;
                select.Description = BookDes.Text;

                _LibraryViewModel.UpdateBookgroupAsync(select);
                ClearFields();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn danh mục cần cập nhật.");
            }
        }

        private void Delete_BookGroup(object sender, RoutedEventArgs e)
        {
            if (BookGroupData.SelectedItem is BookGroup select)
            {

                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhóm sách này không?",
                                               "Xác nhận xóa",
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Warning);


                if (result == MessageBoxResult.Yes)
                {
                    _LibraryViewModel.DeleteBookgroupAsync(select.GroupId);



                    ClearFields();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhóm sách cần xóa.");
            }
        }

        private void Add_BookGroup(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(BookName.Text) && !string.IsNullOrEmpty(BookDes.Text))
            {


                var newBook = new BookGroup
                {

                    Name = BookName.Text,
                    Description = BookDes.Text

                };

                _LibraryViewModel.AddBookGroupAsync(newBook);



                ClearFields();



            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin book group.");
            }
        }

        private void BookShelfData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookShelfData.SelectedItem is Bookshelf select)
            {
                ColumnNumberBookshelf.Text = select.ColumnNumber.ToString();
                RowNumberBookshelf.Text = select.RowNumber.ToString();
                ShelfNumberBookshelf.Text = select.ShelfNumber.ToString();
            }
        }

        private void Add_Bookshelf(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ColumnNumberBookshelf.Text) && !string.IsNullOrEmpty(RowNumberBookshelf.Text) && !string.IsNullOrEmpty(ShelfNumberBookshelf.Text))
            {


                var newBookshelf = new Bookshelf
                {

                    ColumnNumber = Int32.Parse(ColumnNumberBookshelf.Text),
                    RowNumber = Int32.Parse(RowNumberBookshelf.Text),
                    ShelfNumber = Int32.Parse(ShelfNumberBookshelf.Text)

                };

                _LibraryViewModel.AddBookshelfAsync(newBookshelf);



                ClearFields();



            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bookshelf.");
            }
        }

        private void Update_Bookshelf(object sender, RoutedEventArgs e)
        {
            if (BookShelfData.SelectedItem is Bookshelf select)
            {
                select.ColumnNumber = Int32.Parse(ColumnNumberBookshelf.Text);
                select.RowNumber = Int32.Parse(RowNumberBookshelf.Text);
                select.ShelfNumber = Int32.Parse(ShelfNumberBookshelf.Text);

                _LibraryViewModel.UpdateBookshelfAsync(select);
                ClearFields();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn danh mục cần cập nhật.");
            }
        }

        private void Delete_Bookshelf(object sender, RoutedEventArgs e)
        {
            if (BookShelfData.SelectedItem is Bookshelf select)
            {

                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa bookshelf này không?",
                                               "Xác nhận xóa",
                                               MessageBoxButton.YesNo,
                                               MessageBoxImage.Warning);


                if (result == MessageBoxResult.Yes)
                {
                    _LibraryViewModel.DeleteBookshelfAsync(select.ShelfId);



                    ClearFields();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn bookshelf cần xóa.");
            }
        }

        private void myDataGrid_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            if (myLoan.SelectedItem is Loan selectedLoan)
            {
                // Cập nhật các trường nhập liệu với thông tin của danh mục đã chọn
                txtUserId.Text = selectedLoan.UserId.ToString();
                txtCopyId.Text = selectedLoan.CopyId.ToString();
                dpLoanDate.SelectedDate = selectedLoan.LoanDate;
                dpReturnDate.SelectedDate = selectedLoan.ReturnDate;
                dpDueDate.SelectedDate = selectedLoan.DueDate;
                txtFine.Text = selectedLoan.Fine.ToString();
                txtBorrowFee.Text = selectedLoan.BorrowFee.ToString();
                txtStatus.Text = selectedLoan.Status;
            }
        }


        private void AddLoan_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserId.Text) &&
        !string.IsNullOrEmpty(txtCopyId.Text) &&
        dpLoanDate.SelectedDate.HasValue &&
        dpReturnDate.SelectedDate.HasValue &&
        dpDueDate.SelectedDate.HasValue &&
        !string.IsNullOrEmpty(txtFine.Text) &&
        !string.IsNullOrEmpty(txtBorrowFee.Text) &&
        !string.IsNullOrEmpty(txtStatus.Text))
            {


                var newLoan = new Loan
                {

                    UserId = Int32.Parse(txtUserId.Text),
                    CopyId = Int32.Parse(txtCopyId.Text),
                    LoanDate = dpLoanDate.SelectedDate.Value,
                    ReturnDate = dpReturnDate.SelectedDate.Value,
                    DueDate = dpDueDate.SelectedDate.Value,
                    Fine = decimal.Parse(txtFine.Text),
                    BorrowFee = decimal.Parse(txtBorrowFee.Text),
                    Status = txtStatus.Text
                };

                _LibraryViewModel.AddLoanAsync(newLoan);



                ClearFieldsLoan();

            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin danh mục.");
            }
        }

        private void UpdateLoan_Click(object sender, RoutedEventArgs e)
        {
            if (myLoan.SelectedItem is Loan selectedLoan)
            {
                selectedLoan.UserId = Int32.Parse(txtUserId.Text);
                selectedLoan.CopyId = Int32.Parse(txtCopyId.Text);
                selectedLoan.LoanDate = dpLoanDate.SelectedDate.Value;
                selectedLoan.ReturnDate = dpReturnDate.SelectedDate.Value;
                selectedLoan.DueDate = dpDueDate.SelectedDate.Value;
                selectedLoan.Fine = decimal.Parse(txtFine.Text);
                selectedLoan.BorrowFee = decimal.Parse(txtBorrowFee.Text);
                selectedLoan.Status = txtStatus.Text;




                _LibraryViewModel.UpdateLoanAsync(selectedLoan);
                ClearFieldsLoan();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn danh mục cần cập nhật.");
            }
        }

        private void ClearLoan_Click(object sender, RoutedEventArgs e)
        {
            txtUserId.Clear();
            txtCopyId.Clear();
            dpLoanDate.SelectedDate = null;
            dpReturnDate.SelectedDate = null;
            dpDueDate.SelectedDate = null;
            txtFine.Clear();
            txtBorrowFee.Clear();
            txtStatus.Clear();
        }
    }
}
