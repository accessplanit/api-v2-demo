using Blazored.LocalStorage;
using Models;

namespace Services
{
    public class StateManagementService
    {
        private User _loggedInUser;
        private bool _isLoggedIn;
        private Basket _basket;
        private string _latestErrorMessage = string.Empty;

        private readonly ILocalStorageService _localStorage;

        public StateManagementService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public string LatestErrorMessage
        {
            get => _latestErrorMessage;
            set
            {
                _latestErrorMessage = value;
                NotifyStateChanged();
            }
        }

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            private set
            {
                if (_isLoggedIn != value)
                {
                    _isLoggedIn = value;
                    NotifyStateChanged();
                }
            }
        }

        public Token? Token { get; set; } = null;

        public User LoggedInUser
        {
            get => _loggedInUser;
            set
            {
                _loggedInUser = value;
                IsLoggedIn = _loggedInUser != null && _loggedInUser.ID != null;
            }
        }

        public Basket Basket
        {
            get => _basket;
            set
            {
                _basket = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged()
            => OnChange?.Invoke();

        public void SetLoggedInUser(User? user)
        {
            if (user is not null)
            {
                LoggedInUser = user;
                NotifyStateChanged();
            }
        }

        public void SetToken(Token token)
        {
            Token = token;
            NotifyStateChanged();
        }

        /// <summary>
        /// Restores the token saved at login from local storage when none is held in memory
        /// (e.g. after a browser reload creates a new circuit). Does nothing if storage is
        /// unavailable, such as during prerendering.
        /// </summary>
        public async Task EnsureTokenAsync()
        {
            if (Token is not null)
                return;

            try
            {
                var stored = await _localStorage.GetItemAsync<Token>("authToken");

                if (stored is not null && !string.IsNullOrEmpty(stored.AccessToken))
                    Token = stored;
            }
            catch (InvalidOperationException)
            {
                // JS interop isn't available yet (prerender); a later request will retry.
            }
            catch (Exception ex)
            {
                SetLatestErrorMessage($"Could not restore the saved login token. {ex.Message}");
            }
        }

        public void Logout()
        {
            LoggedInUser = null;
            Token = null;
            IsLoggedIn = false;
            NotifyStateChanged();
        }

        public void SetLatestErrorMessage(string message)
        {
            LatestErrorMessage = message;
            NotifyStateChanged();
        }

        public void ClearLatestErrorMessage()
        {
            LatestErrorMessage = string.Empty;
            NotifyStateChanged();
        }
    }
}
