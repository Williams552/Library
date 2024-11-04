using System;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DAOs
{
    public class MemberDAO : SingletonBase<MemberDAO>
    {
        public MemberDAO() { }

        private readonly string _key;
        private readonly JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Constructor có tham số JwtTokenService
        public MemberDAO(JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _jwtTokenService = jwtTokenService;
            _httpContextAccessor = httpContextAccessor;
            _key = configuration["Jwt:Key"];
        }

        public string GenerateJwtTokenMember(Member member)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("userID", member.MemberId.ToString()),
                        new Claim(ClaimTypes.Role, member.Role),
                        new Claim("fullName", member.FullName)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Lay id cua tai khoan dang dang nhap dua tren jwt token lay duoc
        private int GetUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("userID");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return 0;
        }

        // Lay ten cua tai khoan dang dang nhap dua tren jwt token lay duoc
        private string GetUserName()
        {
            var fullName = _httpContextAccessor.HttpContext.User.Identity.Name;
            return fullName;
        }

        // Xem tat ca thanh vien
        public async Task<IEnumerable<Member>> getAllMembers()
        {
            return await _context.Members.Where(a => !a.IsDeleted).ToListAsync();
        }

        // Lay thanh vien theo id
        public async Task<Member> getMemberById(int id)
        {
            return await _context.Members.FirstOrDefaultAsync(a => a.MemberId == id && !a.IsDeleted);
        }

        // Them thanh vien moi
        public async Task<(bool success, string mess, Member)> createMember(Member member)
        {
            var existingMember = await _context.Members.FirstOrDefaultAsync(a => a.IdCardNumber == member.IdCardNumber || a.Username == member.Username);

            if (existingMember != null)
            {
                if (existingMember.IdCardNumber == member.IdCardNumber)
                {
                    return (false, "Thẻ ID này đã tồn tại trong hệ thống.", null);
                }
                else if (existingMember.Username == member.Username)
                {
                    return (false, "Username đã tồn tại", null);
                }
            }
            member.IsDeleted = false;
            member.CreatedBy = GetUserId();
            member.CreatedAt = DateTime.Now;
            member.Password = BCrypt.Net.BCrypt.HashPassword(member.Password);
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
            return (true, "Thêm thành công.", member);
        }

        // Cap nhat thanh vien 
        public async Task<(bool success, string mess, Member)> updateMember(Member member)
        {
            // Kiểm tra xem member có tồn tại trong cơ sở dữ liệu không
            var existingMember = await getMemberById(member.MemberId);
            if (existingMember == null)
            {
                return (false, "Không tìm thấy người này", null);
            }

            // Cập nhật thông tin của member
            existingMember.IsDeleted = false;
            existingMember.UpdatedBy = GetUserId();
            existingMember.UpdatedAt = DateTime.Now;
            existingMember.FullName = member.FullName;
            existingMember.DateOfBirth = member.DateOfBirth;
            existingMember.Gender = member.Gender;
            existingMember.Email = member.Email;
            existingMember.PhoneNumber = member.PhoneNumber;
            existingMember.Address = member.Address;
            existingMember.Username = member.Username;
            if (!string.IsNullOrEmpty(member.Password) && member.Password != existingMember.Password)
            {
                existingMember.Password = BCrypt.Net.BCrypt.HashPassword(member.Password);
            }
            existingMember.IdCardNumber = member.IdCardNumber;
            existingMember.ProfilePicture = member.ProfilePicture;
            existingMember.GroupId = member.GroupId;
            existingMember.MembershipFee = member.MembershipFee;
            existingMember.ResetPin = member.ResetPin;
            existingMember.Role = member.Role;
            existingMember.Balance = member.Balance;
            existingMember.MembershipFeeDueDate = member.MembershipFeeDueDate;
            await _context.SaveChangesAsync();
            return (true, "Member updated successfully.", existingMember);
        }

        // Xoa thanh vien
        public async Task<(bool success, string message)> DeleteMember(int id)
        {
            var member = await getMemberById(id);
            if (member == null)
            {
                return (false, "Không tìm thấy thành viên");
            }
            member.IsDeleted = true;
            member.DeletedBy = GetUserId();
            member.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return (true, "Xóa thành công.");
        }

        public async Task<(bool success, string token, string message)> Login(string username, string password)
        {
            try
            {
                var member = await _context.Members.FirstOrDefaultAsync(a => a.Username == username && !a.IsDeleted);
                if (member == null)
                {
                    return (false, null, "Tài khoản hoặc mật khẩu không chính xác.");
                }
                Console.WriteLine("Password Entered: " + password);
                Console.WriteLine("Stored Password Hash: " + member.Password);
                if (!BCrypt.Net.BCrypt.Verify(password, member.Password))
                {
                    return (false, null, "Tài khoản hoặc mật khẩu không chính xác.");
                }
                var token = GenerateJwtTokenMember(member);
                return (true, token, "Đăng nhập thành công.");
            }
            catch (Exception ex)
            {
                return (false, null, "Có lỗi xảy ra trong quá trình đăng nhập");
            }
        }
    }
}