﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DbMaintenanceWPF.Items
{
    class User : Utilities.ViewModelBase
    {
        ImageSource image;
        int id;
        string name, surname, login, role, salt, passwordHash, dateLock;
        bool isLock, isSelected;

        public ImageSource Image
        {
            get { return image; }
            set { image = value; OnPropertyChanged(nameof(Image));}
        }
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; OnPropertyChanged(nameof(IsSelected)); }
        }
        public bool IsLock
        {
            get { return isLock; }
            set { isLock = value; OnPropertyChanged(nameof(IsLock)); }
        }
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }
        public string PasswordHash
        {
            get { return passwordHash; }
            set { passwordHash = value; OnPropertyChanged(nameof(PasswordHash)); }
        }

        public string Salt
        {
            get { return salt; }
            set { salt = value; OnPropertyChanged(nameof(Salt)); }
        }
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }
        public string Surname
        {
            get { return surname; }
            set { surname = value; OnPropertyChanged(nameof(Surname)); }
        }
        public string Login
        {
            get { return login; }
            set { login = value; OnPropertyChanged(nameof(Login)); }
        }
        public string Role
        {
            get { return role; }
            set { role = value; OnPropertyChanged(nameof(Role)); }
        }
        public string DateLock
        {
            get { return dateLock; }
            set { dateLock = value; OnPropertyChanged(nameof(DateLock)); }
        }
    }
}
