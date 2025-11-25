using System.Security.Cryptography;
using System.Text;
using MovieDLL.Models;

namespace MovieUI.Services
{
	public class UserService : IUserService
	{
		private readonly HttpClient _httpClient;

		public UserService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<UserModel> GetUser(string username)
		{
			HttpResponseMessage response = await _httpClient.GetAsync($"/api/User/search/{username}");

			if (response.IsSuccessStatusCode)
			{
				UserModel user = await _httpClient.GetFromJsonAsync<UserModel>($"/api/User/search/{username}");
				return user;
			}

			return null;
		}

		private string HashPasswordWithSalt(string pwd, string salt)
		{
			using (var sha256 = SHA256.Create())
			{
				byte[] combinedBytes = Encoding.UTF8.GetBytes(pwd + salt);
				byte[] hashBytes = sha256.ComputeHash(combinedBytes);

				StringBuilder sb = new StringBuilder();
				foreach (byte b in hashBytes)
				{
					sb.Append(b.ToString("x2"));
				}

				return sb.ToString();
			}
		}

		private string GenerateSalt(int size = 16)
		{
			byte[] saltBytes = new byte[size];
			RandomNumberGenerator.Fill(saltBytes);
			return Convert.ToBase64String(saltBytes);
		}

		public async Task<bool> ValidateUser(string username, string pwd)
		{
			try
			{
				UserModel user = await GetUser(username);

				string pwdHash = HashPasswordWithSalt(pwd, user.Salt);

				return user.PwdHash.Equals(pwdHash);
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
                return false;
			}
		}

		public async Task<bool> CreateUser(string username, string pwd)
		{
			bool isAvailable = await _httpClient.GetFromJsonAsync<bool>($"/api/User/available/{username}");
			if (!isAvailable) return false;

			string salt = GenerateSalt();

			UserModel user = new UserModel()
			{
				Username = username,
				PwdHash = HashPasswordWithSalt(pwd, salt),
				Salt = salt
			};

			try
			{
				await _httpClient.PostAsJsonAsync($"/api/User/create/", user);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}

		}

	}
}
