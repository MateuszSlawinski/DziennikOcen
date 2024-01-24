using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static DziennikOcen.Pages.Roles.Admin.IndexModel;

namespace DziennikOcen.Pages.Roles.Admin
{
    public class CreateModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            userInfo.role_id = Request.Form["role_id"];
            userInfo.firstName = Request.Form["firstName"];
            userInfo.lastName = Request.Form["lastName"];
            userInfo.email = Request.Form["email"];
            userInfo.password = Request.Form["password"];

            if (userInfo.role_id.Length == 0 || userInfo.firstName.Length == 0 || userInfo.lastName.Length == 0 || 
                userInfo.email.Length == 0 || userInfo.password.Length == 0)
            {
                errorMessage = "Wszystkie pola musza byc wypelnione";
                return;
            }

            //save the new user into the database
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=dziennik;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO users " + "(role_id, firstName, lastName, email, password) VALUES " + "(@role_id, @firstName, @lastName, @email, @password);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@role_id", userInfo.role_id);
                        command.Parameters.AddWithValue("@firstName", userInfo.firstName);
                        command.Parameters.AddWithValue("@lastName", userInfo.lastName);
                        command.Parameters.AddWithValue("@email", userInfo.email);
                        command.Parameters.AddWithValue("@password", userInfo.password);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            userInfo.role_id = ""; userInfo.firstName = ""; userInfo.lastName = ""; userInfo.email = ""; userInfo.password = "";
            successMessage = "Nowy uzytkownik zostal dodany";

            Response.Redirect("/Roles/Admin/Index");
        }
    }
}
