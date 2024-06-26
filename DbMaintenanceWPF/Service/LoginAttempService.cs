﻿using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Repositories;
using System;

namespace DbMaintenanceWPF.Service
{
    class LoginAttempService(UserRepository userR) : ILoginAttemp
    {
        UserRepository UserR = userR;

        public void DownAttempUser(User user)
        {
            user.CountAttemp--;
            UserR.Update(user.Id, user);
        }

        public void LockUser(User user)
        {
            user.DateLock = DateTime.Now;
            UserR.Update(user.Id, user);
        }

        public void UnlockUser(User user)
        {
            user.DateLock = null;
            user.CountAttemp = 3;
            UserR.Update(user.Id, user);
        }
    }
}
