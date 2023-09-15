﻿using BLL.Models.DTO;
using BLL.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserBll
    {
         IEnumerable<UserBll> GetAll();

         UserBll? GetById(int id);

         UserBll? Insert(AddUserFormBll form);

         UserBll? Update(UpdateUserFormBll form);

         int? Delete(int id);

        UserBll Login(LoginFormBll form);
        int? Enable(int id);

        int? Disable(int id);


    }
}
