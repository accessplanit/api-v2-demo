using System.ComponentModel;
using Constants;
using Services.Models;

namespace CommercialSite.WebApp.App.Users.Pages
{
	public partial class Account
	{
		private User? LoggedInUserDetails { get; set; }

		protected override void OnParametersSet()
		{
			//Have to create new User to decouple the instances of the User object from the one we want to
			//update vs. the one we have in the statemanagement
			LoggedInUserDetails = new User(StateManagementService.LoggedInUser);
			StateHasChanged();
		}

		protected override async Task OnInitializedAsync()
		{
			try
			{
				if (LoggedInUserDetails == null)
					LoggedInUserDetails = new User(StateManagementService.LoggedInUser);

			}
			catch (Exception ex)
			{
				StateManagementService.SetLatestErrorMessage($"Something went wrong: {ex.Message}");
				throw;
			}
		}

		private async Task UpdateUser()
		{
			var updatedUserDetails = User.GetUpdatedProperties(StateManagementService.LoggedInUser, LoggedInUserDetails);

			try
			{
				var result = await AccessPlanitUserClient.UpdateUser(LoggedInUserDetails.ID, updatedUserDetails);

				if (result)
					StateManagementService.LoggedInUser = LoggedInUserDetails;
			}
			catch (Exception ex)
			{
                StateManagementService.SetLatestErrorMessage($"Error logging in: {ex.Message}");
			}
		}

	}
}