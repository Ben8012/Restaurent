﻿using DAL.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Tools;
using DAL.Models.Forms;
using DAL.Interfaces;
using Org.BouncyCastle.Crypto.Generators;
using DAL.Models.DTO;

namespace DAL.Services
{
    public class UserDalService : IUserDal
    {
        private readonly Connection _connection;
        private readonly ILogger _logger;

        public UserDalService(ILogger<UserDalService> logger, Connection connection)
        {
            _connection = connection;
            _logger = logger;
        }

        public IEnumerable<UserDal> GetAll()
        {
            Command command = new Command("SELECT Id, lastname, firstname, email, phone, role, birthDate, createdAt, updatedAt,isActive, adress_id FROM [User];", false);
            try
            {
                return _connection.ExecuteReader(command, dr => dr.ToUserDal());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

      
        public UserDal? GetById(int id)
        {
            try
            {
                UserDal? user = GetUserById(id);
                if (user == null) throw new Exception("Id invalide");
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }


        public UserDal? Insert(AddUserFormDal form)
        {
            Command command = new Command("INSERT INTO [User](firstname, lastname, email, passwd, phone, role, birthDate, createdAt, isActive,adress_Id) OUTPUT inserted.id VALUES(@firstname, @lastname, @email,@passwd , @phone , @role, @birthDate, @createdAt, @isActive,@adress_Id)", false);
            command.AddParameter("lastname", form.LastName);
            command.AddParameter("firstname", form.FirstName);
            command.AddParameter("email", form.Email);
            command.AddParameter("passwd", form.Password);
            command.AddParameter("phone", form.Phone == null ? DBNull.Value : form.Phone);
            command.AddParameter("role", "user");
            command.AddParameter("birthDate", form.Birthdate);
            command.AddParameter("createdAt", DateTime.Now);
            command.AddParameter("isActive",1);
            command.AddParameter("adress_Id", form.AdressId);


            try
            {
               int? id = (int?)_connection.ExecuteScalar(command); // recuperer l'id du contact inseré
                if (id.HasValue)
                {
                    UserDal? user = GetUserById(id.Value);
                    return user;
                }
                else
                {
                    throw new Exception("probleme de recuperation de l'id lors de l'insertion");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
  
        }



        public UserDal? Update(UpdateUserFormDal form)
        {
            Command command = new Command("UPDATE [User] SET firstname = @firstname, lastname=@lastname, email=@email, phone=@phone, birthDate=@birthDate, updatedAt=@updatedAt, adress_Id=@adress_Id  OUTPUT inserted.id WHERE Id=@Id ; ", false);
            command.AddParameter("Id", form.Id);
            command.AddParameter("lastname", form.LastName);
            command.AddParameter("firstname", form.FirstName);
            command.AddParameter("email", form.Email);
            command.AddParameter("phone", form.Phone == null ? DBNull.Value : form.Phone);
            command.AddParameter("birthDate", form.Birthdate);
            command.AddParameter("updatedAt", DateTime.Now);
            command.AddParameter("adress_Id", form.AdressId);

            try
            {
                int? resultid = (int?)_connection.ExecuteScalar(command);
                if (!resultid.HasValue) throw new Exception("probleme de recuperation de l'id lors de la mise a jour");
                UserDal? newuser = GetUserById(resultid.Value);
                if (newuser is null) throw new Exception("probleme de recuperation de l'utilisateur lors de la mise a jour");
                return newuser;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

   
        public int? Delete(int id)
        {
            Command command = new Command("DELETE FROM [User] WHERE Id=@Id", false);
            command.AddParameter("Id", id);
            try
            {
                int? nbLigne = (int?)_connection.ExecuteNonQuery(command);
                if (nbLigne != 1) throw new Exception("erreur lors de la suppression");
                return nbLigne;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }


        private UserDal? GetUserById(int id)
        {
            Command command = new Command("SELECT Id, lastname, firstname, email, phone, role, birthDate, createdAt, updatedAt,isActive, adress_id FROM [User] WHERE Id = @Id;", false);
            command.AddParameter("Id", id);
            try
            {
                return _connection.ExecuteReader(command, dr => dr.ToUserDal()).SingleOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }


        public UserDal Login(LoginFormDal form)
        {
            string? passwordHash;
            Command command = new Command("SELECT passwd FROM [User] WHERE email = @Email ", false);
            command.AddParameter("Email", form.Email);
            try
            {
                passwordHash = (string?)_connection.ExecuteScalar(command);
                if (passwordHash is null) throw new Exception("l'email est invalide");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                bool verified = BCrypt.Net.BCrypt.Verify(form.Password, passwordHash);
                if (!verified) throw new Exception("Le mot de passe est invalide");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            Command command2 = new Command("SELECT Id, lastname, firstname, email, phone, role, birthDate, createdAt, updatedAt,isActive, adress_id FROM [User] WHERE Email = @Email", false);
            command2.AddParameter("Email", form.Email);

            try
            {
                UserDal? user = _connection.ExecuteReader(command2, dr => dr.ToUserDal()).SingleOrDefault();

                if (user is null)
                {
                    throw new Exception("L'utilisateur n'a pas été trouvé ");
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int? Disable(int id)
        {
            Command command = new Command("UPDATE [User] SET isActive = @isActive WHERE Id=@Id ; ", false);
            command.AddParameter("Id", id);
            command.AddParameter("isActive", 0);

            try
            {
                int? nbLigne = (int?)_connection.ExecuteNonQuery(command);
                if (nbLigne != 1) throw new Exception("erreur lors de la suppression");
                return nbLigne;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? Enable(int id)
        {
            Command command = new Command("UPDATE [User] SET isActive = @isActive WHERE Id=@Id ; ", false);
            command.AddParameter("Id", id);
            command.AddParameter("isActive", 1);

            try
            {
                int? nbLigne = (int?)_connection.ExecuteNonQuery(command);
                if (nbLigne != 1) throw new Exception("erreur lors de la suppression");
                return nbLigne;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
