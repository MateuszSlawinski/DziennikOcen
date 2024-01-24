using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DziennikOcen.Pages.Roles.Admin
{
	public class IndexModel : PageModel
	{
		public List<UserInfo> listUsers = new List<UserInfo>();
		public void OnGet()
		{
			try
			{
				String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=dziennik;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

				using (SqlConnection connection = new SqlConnection(connectionString)) 
				{
					connection.Open();
					String sql = "SELECT * FROM users";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								UserInfo userInfo = new UserInfo();
								userInfo.user_id = "" + reader.GetInt32(0);
								userInfo.role_id = "" + reader.GetInt32(1);
								userInfo.firstName = reader.GetString(2);
								userInfo.lastName = reader.GetString(3);
								userInfo.email = reader.GetString(4);
								userInfo.password = reader.GetString(5);

								listUsers.Add(userInfo);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
			}
		}
		public class UserInfo
		{
			public String user_id;
			public String role_id;
			public String firstName;
			public String lastName;
			public String email;
			public String password;
		}
	}
}
