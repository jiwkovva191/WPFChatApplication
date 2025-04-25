using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ChatApplication.Core;
using ChatApplication.MVVM.Model;

namespace ChatApplication.MVVM.ViewModel
{
    public class MainViewModel:ObservableObject
    {
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }

        /*Commands*/
        public ContactModel SelectedContact { get; set; }
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
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

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
    }
}
