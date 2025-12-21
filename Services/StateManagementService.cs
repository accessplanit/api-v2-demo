using Models;

namespace Services
{
    public class StateManagementService
    {
        private User _loggedInUser;
        private bool _isLoggedIn;
        private Basket _basket;
        private string _latestErrorMessage = string.Empty;

        public StateManagementService() {}

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
