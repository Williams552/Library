//using Models;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using WpfLibrary;

//namespace WpfLibrary.ViewModel
//{
//    public class StaffCRUDViewModel : BaseViewModel
//    {
//        private readonly HttpClient _httpClient;

//        public ObservableCollection<Staff> StaffMembers { get; set; } = new ObservableCollection<Staff>();
//        public Staff SelectedStaff { get; set; } = new Staff();

//        public ICommand AddStaffCommand { get; }
//        public ICommand UpdateStaffCommand { get; }
//        public ICommand DeleteStaffCommand { get; }

//        public StaffCRUDViewModel()
//        {
//            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7143/api/Staff/") };
//            LoadStaffAsync();
//        }

//        public async Task LoadStaffAsync()
//        {
//            var staffList = await _httpClient.GetFromJsonAsync<List<Staff>>("");
//            if (staffList != null)
//            {
//                StaffMembers.Clear();
//                foreach (var staff in staffList)
//                {
//                    StaffMembers.Add(staff);
//                }
//            }
//        }

//        public async Task AddStaffAsync()
//        {
//            var response = await _httpClient.PostAsJsonAsync("", SelectedStaff);
//            if (response.IsSuccessStatusCode)
//            {
//                StaffMembers.Add(SelectedStaff);
//                SelectedStaff = new Staff();
//            }
//        }

//        public async Task UpdateStaffAsync()
//        {
//            if (SelectedStaff != null)
//            {
//                var response = await _httpClient.PutAsJsonAsync($"{SelectedStaff.StaffId}", SelectedStaff);
//                if (response.IsSuccessStatusCode)
//                {
//                    var index = StaffMembers.IndexOf(StaffMembers.First(s => s.StaffId == SelectedStaff.StaffId));
//                    if (index >= 0)
//                    {
//                        StaffMembers[index] = SelectedStaff;
//                    }
//                }
//            }
//        }

//        public async Task DeleteStaffAsync()
//        {
//            if (SelectedStaff != null)
//            {
//                var response = await _httpClient.DeleteAsync($"{SelectedStaff.StaffId}");
//                if (response.IsSuccessStatusCode)
//                {
//                    StaffMembers.Remove(SelectedStaff);
//                    SelectedStaff = new Staff();
//                }
//            }
//        }

//        private bool CanExecuteUpdateOrDelete()
//        {
//            return SelectedStaff != null && SelectedStaff.StaffId != 0;
//        }
//    }
//}
