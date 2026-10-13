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
			if (!StateManagementService.IsLoggedIn)
			{
				Navigation.NavigateTo("/login");
				return;
			}

			LoggedInUserDetails = new User(StateManagementService.LoggedInUser);
			StateHasChanged();
		}

		protected override async Task OnInitializedAsync()
		{
			try
			{
				if (LoggedInUserDetails == null && StateManagementService.IsLoggedIn)
					LoggedInUserDetails = new User(StateManagementService.LoggedInUser);

			}
			catch (Exception ex)
			{
				StateManagementService.SetLatestErrorMessage($"Something went wrong: {ex.Message}");
				throw;
			}
		}

		private bool isDirty;
		private bool isSaved;

		private string Initials =>
			$"{LoggedInUserDetails?.Forenames?.FirstOrDefault()}{LoggedInUserDetails?.Surname?.FirstOrDefault()}".ToUpperInvariant();

		private void OnFormChanged()
		{
			isDirty = true;
			isSaved = false;
		}

		private void DiscardChanges()
		{
			LoggedInUserDetails = new User(StateManagementService.LoggedInUser);
			isDirty = false;
			isSaved = false;
		}

		private async Task UpdateUser()
		{
			var updatedUserDetails = User.GetUpdatedProperties(StateManagementService.LoggedInUser, LoggedInUserDetails);

			try
			{
				var result = await AccessPlanitUserClient.UpdateUser(LoggedInUserDetails.ID, updatedUserDetails);

				if (result)
				{
					StateManagementService.LoggedInUser = LoggedInUserDetails;
					isDirty = false;
					isSaved = true;
				}
			}
			catch (Exception ex)
			{
                StateManagementService.SetLatestErrorMessage($"Error logging in: {ex.Message}");
			}
		}

	}
}