using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ChatApplication.Core;
using ChatApplication.MVVM.Model;
using ChatApplication.Net;

namespace ChatApplication.MVVM.ViewModel
{
    public class MainViewModel:ObservableObject
    { 
        private Server _server;
        public string Username { get; set; }
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }

        /*Commands*/
        public RelayCommand SendCommand { get; set; }
        public RelayCommand ConnectToServerCommand { get; set; }
       
        
        private ContactModel _selectedContact;

        public ContactModel SelectedContact
        {
            get { return _selectedContact; }
            set { _selectedContact = value; OnPropertyChanged(); }
           
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set {
                _message = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            /*server*/
            _server = new Server();
            _server.connectedEvent += UserConnected;
            ConnectToServerCommand = new RelayCommand(o =>_server.ConnectToServer(Username),o => !string.IsNullOrEmpty(Username));
            
            /*ui*/
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();
            SendCommand = new RelayCommand(o =>
            {
                Messages.Add(new MessageModel
                {
                    Message = Message,
                    FirstMessage = false
                });
                Message = "";
            });

            Messages.Add(new MessageModel
            {
                Username = "Allison",
                UsernameColor = "#FF0000",
                ImageSource = "https://cdn.pixabay.com/photo/2018/04/18/18/56/user-3331256_1280.png",
                Message = "Hello, how are you?",
                Time = DateTime.Now,
                IsNativeOrigin = false,
                FirstMessage = true
            });

            for (int i = 0; i < 3; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "Allison",
                    UsernameColor = "#FF0000",
                    ImageSource = "https://cdn.pixabay.com/photo/2018/04/18/18/56/user-3331256_1280.png",
                    Message = "Hello, how are you?",
                    Time = DateTime.Now,
                    IsNativeOrigin = false,
                    FirstMessage = false
                });
            }

            for (int i = 0; i < 4; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "Meggie",
                    UsernameColor = "#FF0000",
                    ImageSource = "https://cdn.pixabay.com/photo/2018/04/18/18/56/user-3331256_1280.png",
                    Message = "Hello, how are you?",
                    Time = DateTime.Now,
                    IsNativeOrigin = true
                });
            }

            Messages.Add(new MessageModel
            {
                Username = "Meggie",
                UsernameColor = "#FF0000",
                ImageSource = "https://cdn.pixabay.com/photo/2018/04/18/18/56/user-3331256_1280.png",
                Message = "Last",
                Time = DateTime.Now,
                IsNativeOrigin = true
            });

            for (int i = 0; i < 5; i++)
            {
                Contacts.Add(new ContactModel
                {
                    Username = $"Allison {i}",
                    ImageSource = "https://cdn.pixabay.com/photo/2018/04/18/18/56/user-3331256_1280.png",
                    Messages = Messages
                });
            }
        }

        private void UserConnected()
        {
            var user = new ContactModel
            {
                Username = _server.PacketReader.ReadMessage(),
                UID = _server.PacketReader.ReadMessage()
            };

            if(!Contacts.Any(c=>c.UID == user.UID))
            {
                Application.Current.Dispatcher.Invoke(() => Contacts.Add(user));

            }

        }
    }
}
